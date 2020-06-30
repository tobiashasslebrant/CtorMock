using System;

namespace CtorMock.CtorSelect
{
    public class CtorsSelecterWithLeastParams : ICtorSelecter
    {
        public int Index(Type type, int depth)
        {
            var chosenCtor = (0,0);
            var ctors = type.GetConstructors();
            for (var index = 0; index < ctors.Length; ++index)
            {
                var length = ctors[index].GetParameters().Length;
                if (length <= chosenCtor.Item1)
                    chosenCtor = (length, index);
            }
            return chosenCtor.Item2;
        }
    }
}