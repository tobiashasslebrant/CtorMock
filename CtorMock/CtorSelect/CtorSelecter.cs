using System;

namespace CtorMock.CtorSelect
{
    public class CtorSelecter : ICtorSelecter
    {
        private readonly int _ctorIndex;
        public CtorSelecter(int ctorIndex) => _ctorIndex = ctorIndex;
        public int Index(Type type) => _ctorIndex;
    }
}