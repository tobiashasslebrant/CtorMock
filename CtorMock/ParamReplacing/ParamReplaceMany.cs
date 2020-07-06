using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public class ParamReplaceMany : IParamReplace
    {
        private readonly (string paramName, object replacedWith)[] _paramReplaces;
        private readonly int _depth;

        public ParamReplaceMany((string paramName, object replacedWith)[] paramReplaces, int depth)
        {
            _paramReplaces = paramReplaces;
            _depth = depth;
        }

        public (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, int depth)
        {
            if (depth != _depth)
                return (null, false);

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