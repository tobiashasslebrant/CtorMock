using System;
using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public interface IParamReplace
    {
        //bool CanReplace(ParameterInfo parameterInfo, Type parent);
        //object Replace(ParameterInfo parameterInfo, Type parent);
        (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, Type parent);
        
    }
}