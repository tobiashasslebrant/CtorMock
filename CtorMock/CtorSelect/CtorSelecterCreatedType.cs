using System;

namespace CtorMock.CtorSelect
{
    public class CtorSelecterCreatedType : ICtorSelecter
    {
        private readonly int _ctorIndex;

        public CtorSelecterCreatedType(int ctorIndex) 
            => _ctorIndex = ctorIndex;

        public int Index(Type type, int depth) 
            => depth == 0 ? _ctorIndex : 0;
    }
}