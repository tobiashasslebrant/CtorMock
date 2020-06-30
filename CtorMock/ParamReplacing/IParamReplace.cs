using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public interface IParamReplace
    {
        (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, int depth);
    }
}