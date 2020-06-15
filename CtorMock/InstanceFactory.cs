using System;
using System.Linq;
using System.Reflection;

namespace CtorMock
{
    
    public class InstanceFactory
    {
        private readonly Func<Type, object> _createMock;

        public InstanceFactory(Func<Type,object> createMock) => _createMock = createMock;

        public T New<T>(Func<ParameterInfo, (object replaceWith, bool isReplaced)> paramReplace = null)
            => New<T>(0, paramReplace);

        public T New<T>(int ctorIndex = 0, Func<ParameterInfo, (object replaceWith,bool isReplaced)> paramReplace = null)
        {
            return (T)Create(typeof(T));

            object Replace(ParameterInfo parameterInfo)
            {
                if (paramReplace != null)
                {
                    var result = paramReplace.Invoke(parameterInfo);
                    if (result.isReplaced)
                        return result.replaceWith;
                    
                }
                return null;
            }

            object Create(Type type)
            {
                var ctors = type.GetConstructors();
                if (ctors.Length == 0)
                    return GetDefault(type);
                if (type.IsArray)
                    return null;
                if (type == typeof(string))
                    return null;
                if (type.IsAbstract)
                    return null;
                if (type.IsValueType) 
                    return GetDefault(type);
                if (type.IsInterface)
                    return _createMock(type);

                var ctorParams = ctors[ctorIndex].GetParameters();
                ctorIndex = 0; //only first type will be able to chose contructor
                
                return ctorParams.Length == 0
                    ? Activator.CreateInstance(type)
                    : Activator.CreateInstance(type, ctorParams
                        .Select(param => 
                            Replace(param) ?? 
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

  