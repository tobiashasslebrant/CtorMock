using System;

namespace CtorMock.CtorSelect
{
    public interface ICtorSelecter
    {
        int Index(Type type, int depth);
    }
}