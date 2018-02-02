using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace CtorMock
{
    public abstract class CtorMockerBase
    {
        protected abstract object CreateMock(Type type);

        public T New<T>(ExpandoObject ctorParams = null, int ctorIndex = 0)
        {
            var ctorParamsDictionary = ((IDictionary<string, object>)ctorParams ?? new Dictionary<string, object>());
            var classCtor = typeof(T).GetConstructors()[ctorIndex].GetParameters();

            foreach (var param in ctorParamsDictionary)
                if (!classCtor.Select(c => c.Name).Contains(param.Key))
                    throw new ArgumentException($"{ param.Key } is not an argument in constructor of class { typeof(T).Name }");

            var ctor = new List<object>();
            foreach (var param in classCtor)
                ctor.Add(ctorParamsDictionary.ContainsKey(param.Name)
                    ? ctorParamsDictionary[param.Name]
                    : Create(param.ParameterType));

            return (T)Activator.CreateInstance(typeof(T), ctor.ToArray());
        }

        object Create(Type type) => type.IsValueType
            ? Activator.CreateInstance(type)
            : (type.IsInterface
                ? CreateMock(type)
                : null);
    }
}
