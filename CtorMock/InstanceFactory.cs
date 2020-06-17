using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CtorMock
{
    public class InstanceFactory
    {
        readonly Dictionary<Type, object> _mocks = new Dictionary<Type, object>();

        private readonly Func<Type, object> _createMock;

        public InstanceFactory(Func<Type,object> createMock) => _createMock = createMock;
        
        public T MockOf<T>() 
            => (T) _mocks[typeof(T)];

        public T New<T>(Func<Type, int, int> chooseCtor) where T : class
            => New<T>(null, chooseCtor);
        
        public T New<T>(Func<ParameterInfo, (object replaceWith, bool isReplaced)> paramReplace = null, Func<Type, int, int> chooseCtor = null) where T : class
        {
            var depth = 0;
            return (T)Create(typeof(T));
            
            object Create(Type type)
            {
                if (type.IsArray)
                    return DefaultValue.Of(type);
                if (type == typeof(string))
                    return DefaultValue.Of(type);
                if (type.IsValueType) 
                    return DefaultValue.Of(type);
                if (type.IsInterface)
                {
                    if (!_mocks.ContainsKey(type))
                        _mocks.Add(type, _createMock(type));
                    return _mocks[type];
                }
                if (type.IsAbstract)
                    return DefaultValue.Of(type);
                
                var ctors = type.GetConstructors();
                if (ctors.Length == 0)
                    return DefaultValue.Of(type);

                var ctorIndex = chooseCtor?.Invoke(type, depth++) ?? 0;
                var ctorParams = ctors[ctorIndex].GetParameters();
                
                return ctorParams.Length == 0
                    ? Activator.CreateInstance(type)
                    : Activator.CreateInstance(type, ctorParams
                        .Select(param => 
                            Replace(param) ?? 
                            Create(param.ParameterType)).ToArray());
            }
            
            object Replace(ParameterInfo parameterInfo)
            {
                if (paramReplace != null)
                {
                    var result = paramReplace.Invoke(parameterInfo);
                    if (result.isReplaced)
                    {
                        if(result.replaceWith?.GetType() != parameterInfo.ParameterType)
                            throw new ArgumentException($"Replaced type {result.replaceWith?.GetType().Name} must be same as parameter type {parameterInfo.ParameterType.Name}");

                        return result.replaceWith;
                    }
                }
                return null;
            }

        }
    }
}


  