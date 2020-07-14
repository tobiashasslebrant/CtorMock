using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CtorMock.CtorSelect;
using CtorMock.ParamReplacing;

namespace CtorMock
{
    public abstract class CtorMockerBase
    {
        private const int DEFAULT_CTOR = 0;
        
        public abstract object CreateMock(Type type);

        public T New<T>() where T : class
            => New<T>(null,null);
        
        public T New<T>(int ctorIndex) where T : class
            => New<T>(new CtorSelecter(ctorIndex),null);
        
        public T New<T>(params (string paramName, object replacedWith)[] paramReplaces) where T : class
            => New<T>(null, new ParamReplaceMany(paramReplaces, (parent, param) => parent == typeof(T)));

        public T New<T>(int ctorIndex, params (string paramName, object replacedWith)[] paramReplaces) where T : class
            => New<T>(new CtorSelecter(ctorIndex), new ParamReplaceMany(paramReplaces, (parent, param) => parent == typeof(T)));

        public T New<T>(ExpandoObject paramReplaces, int ctorIndex = 0) where T : class 
            => New<T>(ctorIndex, paramReplaces.Select(s => (s.Key, s.Value)).ToArray());

        public T New<T>(ICtorSelecter ctorSelecter, IParamReplace paramReplace) where T : class
        {
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

                var ctorIndex = ctorSelecter?.Index(type) ?? DEFAULT_CTOR;

                var ctorParams = ctors[ctorIndex].GetParameters()
                    .Select(param =>
                        Replace(param, type) ??
                        Create(param.ParameterType))
                    .ToArray();

                 return ctors[ctorIndex].Invoke(BindingFlags.CreateInstance, null, ctorParams, null);
            }

            object Replace(ParameterInfo parameterInfo, Type parent)
            {
                if (paramReplace != null)
                {
                    var result = paramReplace.Replace(parameterInfo, parent);
                    if (result.isReplaced)
                    {
                        if (result.replaceWith?.GetType() != parameterInfo.ParameterType)
                            throw new ArgumentException(
                                $"Replaced type {result.replaceWith?.GetType().Name} must be same as parameter type {parameterInfo.ParameterType.Name}");

                        return result.replaceWith;
                    }
                }
                return null;
            }
        }
    }
}


  