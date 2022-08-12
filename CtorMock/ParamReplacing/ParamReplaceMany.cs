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

        public bool CanReplace(ParameterInfo parameterInfo, Type parent) 
            => _valid(parent, parameterInfo)
               && _paramReplaces.Any(a => IsInterchangeable(parameterInfo, a.paramName, a.replacedWith));

        public object GetReplacement(ParameterInfo parameterInfo, Type parent) 
            => _paramReplaces.First(f => IsInterchangeable(parameterInfo, f.paramName, f.replacedWith)).replacedWith;

        bool IsInterchangeable(ParameterInfo paramToBeReplaced, string replaceParameterName, object? replaceParameter)
        {
            var typeToBeReplaced = paramToBeReplaced.ParameterType;
            var sameParameter = replaceParameterName == paramToBeReplaced.Name;

            if (!sameParameter)
                return false;
            
            if (replaceParameter is null)
                return true;
            
            var replaceParameterType = replaceParameter.GetType();
            var interChangeableType = typeToBeReplaced.IsInterface
                ? replaceParameterType.GetInterfaces().Any(i => i == typeToBeReplaced)
                : typeToBeReplaced.IsAssignableFrom(replaceParameterType);

            return sameParameter && interChangeableType;
        }
    }
}