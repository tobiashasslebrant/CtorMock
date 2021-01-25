using System;
using System.Linq;
using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public class ParamReplaceMany : IParamReplace
    {
        private readonly (string paramName, object replacedWith)[] _paramReplaces;
        private readonly Func<Type, ParameterInfo, bool> _valid;

        public ParamReplaceMany((string paramName, object replacedWith)[] paramReplaces, Func<Type, ParameterInfo, bool> valid)
        {
            _paramReplaces = paramReplaces;
            _valid = valid;
        }

        public (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, Type parent)
        {
            if(_valid(parent, parameterInfo))
                foreach (var paramReplace in _paramReplaces)
                {
                    if (IsInterchangeable(parameterInfo.ParameterType, paramReplace.replacedWith.GetType())
                        && parameterInfo.Name == paramReplace.paramName)
                        return (paramReplace.replacedWith, true);
                }

            return (new object(), false);
        }

        public bool IsInterchangeable(Type type, Type exchangeWith) 
            => type.IsInterface
                ? exchangeWith.GetInterfaces().Any(i => i == type)
                : type.IsAssignableFrom(exchangeWith);
    }
}