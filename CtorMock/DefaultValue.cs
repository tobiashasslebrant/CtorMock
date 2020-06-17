using System;
using System.Reflection;

namespace CtorMock
{
    public static class DefaultValue
    {
        static readonly DefaultValueService _defaultValueService;

        static DefaultValue() => _defaultValueService = new DefaultValueService();

        public static object Of(Type t)
            => _defaultValueService.Of(t);
        
        class DefaultValueService
        {
            public object Of(Type t) 
                => GetType()
                    .GetMethod(nameof(OfGeneric),BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(t).Invoke(this, null);
        
            TDefault OfGeneric<TDefault>() 
                => default(TDefault);
    
        }
        
    }
}