using System;
using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public interface IParamReplace
    {
        bool CanReplace(ParameterInfo parameterInfo, Type parent);
        object GetReplacement(ParameterInfo parameterInfo, Type parent);
    }
}