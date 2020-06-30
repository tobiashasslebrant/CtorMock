using System;

namespace CtorMock.CtorSelect
{
    public class CtorSelecterForCreatedType : ICtorSelecter
    {
        private readonly int _ctorIndex;

        public CtorSelecterForCreatedType(int ctorIndex) 
            => _ctorIndex = ctorIndex;

        public int Index(Type type, int depth) 
            => depth == 0 ? _ctorIndex : 0;
    }
}