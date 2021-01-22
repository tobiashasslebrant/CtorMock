using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;
using CtorMock.CtorSelect;
using CtorMock.ParamReplacing;

namespace CtorMock
{
    public abstract class CtorMockerBase
    {
        private const int DEFAULT_CTOR = 0;
        
        public abstract object CreateMock(Type type);

        public T New<T>() where T : class
            => New<T>(new CtorSelecter(0));
        
        public T New<T>(int ctorIndex) where T : class
            => New<T>(new CtorSelecter(ctorIndex));
        
        public T New<T>(params (string paramName, object replacedWith)[] paramReplaces) where T : class
            => New<T>(new CtorSelecter(0), new ParamReplaceMany(paramReplaces, (parent, param) => parent == typeof(T)));

        public T New<T>(int ctorIndex, params (string paramName, object replacedWith)[] paramReplaces) where T : class
            => New<T>(new CtorSelecter(ctorIndex), new ParamReplaceMany(paramReplaces, (parent, param) => parent == typeof(T)));

        public T New<T>(ExpandoObject paramReplaces, int ctorIndex = 0) where T : class 
            => New<T>(ctorIndex, paramReplaces.Select(s => (s.Key, s.Value)).ToArray());

        public T New<T>(ICtorSelecter ctorSelecter) where T : class
            =>  (T)Factory(typeof(T), ctorSelecter, param => Create(param.ParameterType, ctorSelecter));

        public T New<T>(ICtorSelecter ctorSelecter, IParamReplace paramReplace) where T : class
            =>  (T)Factory(typeof(T), ctorSelecter, param => Replace(param, typeof(T), paramReplace));
        
        object Factory(Type type, ICtorSelecter ctorSelecter, Func<ParameterInfo, object> ctorHandler)
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
            if (type.GetConstructors().Length == 0)
                return DefaultValue.Of(type);

            var ctorIndex = ctorSelecter?.Index(type) ?? DEFAULT_CTOR;

            var ctors = type.GetConstructors();
            var ctorParams = ctors[ctorIndex].GetParameters()
                .Select(ctorHandler)
                .ToArray();

            return ctors[ctorIndex].Invoke(BindingFlags.CreateInstance,null, ctorParams, Thread.CurrentThread.CurrentCulture);
        }
        
        object Create(Type type, ICtorSelecter ctorSelecter)
            => Factory(type, ctorSelecter, param => Create(param.ParameterType, ctorSelecter));
        
        object Replace(ParameterInfo parameterInfo, Type parent, IParamReplace paramReplace)
        {
            var result = paramReplace.Replace(parameterInfo, parent);
            if (result.isReplaced)
            {
                if (result.replaceWith?.GetType() != parameterInfo.ParameterType)
                    throw new ArgumentException(
                        $"Replaced type {result.replaceWith?.GetType().Name} must be same as parameter type {parameterInfo.ParameterType.Name}");

                return result.replaceWith;
            }

            return Create(parameterInfo.ParameterType, new CtorSelecter(0));
        }
    }
}


  