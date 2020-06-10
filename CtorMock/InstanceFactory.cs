using System;
using System.Linq;
using System.Reflection;

namespace CtorMock
{
    
    public class InstanceFactory
    {
        private readonly Func<Type, object> _createMock;

        public InstanceFactory(Func<Type,object> createMock) => _createMock = createMock;

        public T New<T>(Func<ParameterInfo, object> replace = null)
        {
            return (T)Create(typeof(T));

            object Create(Type type)
            {
                if (type.IsInterface)
                    return _createMock(type);
                if (type.IsArray)
                    return null;
                if (type == typeof(string))
                    return null;
                if (type.IsAbstract)
                    return null;
                if (type.IsValueType) 
                    return GetDefault(type);

                var ctorParams = type.GetConstructors()[0].GetParameters();

                return ctorParams.Length == 0
                    ? Activator.CreateInstance(type)
                    : Activator.CreateInstance(type, ctorParams
                        .Select(param =>
                            replace?.Invoke(param) ??
                            Create(param.ParameterType)).ToArray());
            }
            
        }
        object GetDefault(Type t) 
            => GetType()
                .GetMethod(nameof(GetDefaultGeneric),BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(t).Invoke(this, null);
        
        TDefault GetDefaultGeneric<TDefault>() 
            => default(TDefault);


    }
}

  