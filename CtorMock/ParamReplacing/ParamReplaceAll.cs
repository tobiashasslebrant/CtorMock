using System.Reflection;

namespace CtorMock.ParamReplacing
{
    public class ParamReplaceAll<T> : IParamReplace
    {
        private readonly T _value;
        private readonly string _name;

        public ParamReplaceAll(T value, string name)
        {
            _value = value;
            _name = name;
        }

        public (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, int depth) 
            => parameterInfo.ParameterType == typeof(T) && parameterInfo.Name == _name 
                ? ((object replaceWith, bool isReplaced)) (_value, true)
                : (null, false);
    }
}