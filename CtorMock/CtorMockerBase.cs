using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CtorMock
{
    public abstract class CtorMockerBase
    {
        protected abstract object CreateMock(Type type);

        public T New<T>(ExpandoObject ctorParams = null, int ctorIndex = 0)
        {
            var ctorParamsDictionary = (IDictionary<string, object>)ctorParams ?? new Dictionary<string, object>();

            var classCtor = typeof(T).GetConstructors()[ctorIndex].GetParameters();

            foreach (var param in ctorParamsDictionary)
                if (!classCtor.Select(c => c.Name).Contains(param.Key))
                    throw new ArgumentException($"{ param.Key } is not an argument in constructor of class { typeof(T).Name }");

            var ctorArguments = classCtor.Select(param => ctorParamsDictionary.ContainsKey(param.Name)
                ? ctorParamsDictionary[param.Name]
                : InstanceFromType(param.ParameterType)).ToArray();

            return (T)Activator.CreateInstance(typeof(T), ctorArguments);
        }

        object InstanceFromType(Type type) => type.IsValueType
            ? Activator.CreateInstance(type)
            : (type.IsInterface
                ? CreateMock(type)
                : null);
    }
}
