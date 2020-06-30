using System;

namespace CtorMock.CtorSelect
{
    public class CtorSelecterForSubType : ICtorSelecter
    {
        private readonly Type _type;
        private readonly int _ctorIndex;

        public CtorSelecterForSubType(Type type, int ctorIndex)
        {
            _type = type;
            _ctorIndex = ctorIndex;
        }

        public int Index(Type type, int depth) 
            => type == _type && depth > 0 ? _ctorIndex : 0;
    }
}