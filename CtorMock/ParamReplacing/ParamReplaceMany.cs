using System;
using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public class ParamReplaceMany : IParamReplace
    {
        private readonly (string paramName, object replacedWith)[] _paramReplaces;
        private readonly Func<int, bool> _validForDepth;

        public ParamReplaceMany((string paramName, object replacedWith)[] paramReplaces, Func<int,bool> validForDepth)
        {
            _paramReplaces = paramReplaces;
            _validForDepth = validForDepth;
        }

        public (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, int depth)
        {
            if(_validForDepth(depth))
                foreach (var paramReplace in _paramReplaces)
                {
                    if (parameterInfo.ParameterType == paramReplace.replacedWith.GetType() &&
                        parameterInfo.Name == paramReplace.paramName)
                        return (paramReplace.replacedWith, true);
                }

            return (null, false);
        }
    }
}