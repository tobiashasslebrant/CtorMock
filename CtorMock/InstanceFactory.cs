using System;
using System.Linq;
using System.Reflection;
using CtorMock.CtorSelect;
using CtorMock.ParamReplacing;

namespace CtorMock
{
    public abstract class InstanceFactory
    {
        private const int DEFAULT_CTOR = 0;
        
        public abstract object CreateMock(Type type);
        
        public T New<T>(ICtorSelecter ctorSelecter) where T : class
            => New<T>(ctorSelecter,null);
        
        public T New<T>(IParamReplace paramReplace) where T : class
            => New<T>(null, paramReplace);
        
        public T New<T>(ICtorSelecter ctorSelecter = null, params IParamReplace[] paramReplaces) where T : class
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
                    return CreateMock(type);
                if (type.IsAbstract)
                    return DefaultValue.Of(type);
                
                var ctors = type.GetConstructors();
                if (ctors.Length == 0)
                    return DefaultValue.Of(type);

                var ctorIndex = ctorSelecter?.Index(type, depth++) ?? DEFAULT_CTOR;

                var ctorParams = ctors[ctorIndex].GetParameters()
                    .Select(param =>
                        Replace(param) ??
                        Create(param.ParameterType))
                    .ToArray();
                
                return ctors[ctorIndex].Invoke(BindingFlags.CreateInstance, null, ctorParams, null);
            }
            
            object Replace(ParameterInfo parameterInfo)
            {
                if (paramReplaces != null)
                {
                    foreach (var paramReplace in paramReplaces)
                    {
                        var result = paramReplace.Replace(parameterInfo, depth);
                        if (result.isReplaced)
                        {
                            if (result.replaceWith?.GetType() != parameterInfo.ParameterType)
                                throw new ArgumentException(
                                    $"Replaced type {result.replaceWith?.GetType().Name} must be same as parameter type {parameterInfo.ParameterType.Name}");

                            return result.replaceWith;
                        }
                    }
                }
                return null;
            }
        }
    }
}


  