using System;
using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public interface IParamReplace
    {
        bool CanReplace(ParameterInfo parameterInfo, Type parent);
        object GetReplacement(ParameterInfo parameterInfo, Type parent);
        //(bool canBeReplaced, Func<object> getReplacement)  Replace(ParameterInfo parameterInfo, Type parent);
        //(object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, Type parent);
        
    }
}