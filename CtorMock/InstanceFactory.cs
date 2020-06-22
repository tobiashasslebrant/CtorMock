using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CtorMock
{
    public class InstanceFactory
    {
        private const int DEFAULT_CTOR = 0;
        
        readonly Dictionary<Type, object> _mocks = new Dictionary<Type, object>();

        private readonly Func<Type, object> _createMock;

        public InstanceFactory(Func<Type,object> createMock) => _createMock = createMock;
        
        /// <summary>
        /// Get the created mock instance from memory that was used as constructor arguments when creating type
        /// </summary>
        /// <typeparam name="T">Type of interface used</typeparam>
        /// <returns>a mock</returns>
        public T MockOf<T>() 
            => (T) _mocks[typeof(T)];
        
        /// <summary>
        /// Creates a new instance of T
        /// </summary>
        /// <param name="chooseCtor">Choose another constructor for object, first is default.
        /// (in) type: the type with the constructors
        /// (in) int: is the nested depth
        /// (out) int: the index of constructor to use 
        /// </param>
        /// <typeparam name="T">The type to create</typeparam>
        /// <returns>Instance of T with created constructor parameters</returns>
        public T New<T>(Func<Type, int, int> chooseCtor) where T : class
            => New<T>(null, chooseCtor);
        

        /// <summary>
        /// Creates a new instance of T
        /// </summary>
        /// <param name="paramReplace">Replace a constructor parameter.
        /// (in) ParameterInfo: is the supplied parameter.
        /// (in) int: is the nested depth
        /// (out) (object,bool): new object and true if replaced or null and false if not replaced </param>
        /// <param name="chooseCtor">Choose another constructor for object, first is default.
        /// (in) type: the type with the constructors
        /// (in) int: is the nested depth
        /// (out) int: the index of constructor to use 
        /// </param>
        /// <typeparam name="T">The type to create</typeparam>
        /// <returns>Instance of T with created constructor parameters</returns>
        /// <exception cref="ArgumentException"></exception>
        public T New<T>(Func<ParameterInfo, int, (object replaceWith, bool isReplaced)> paramReplace = null, Func<Type, int, int> chooseCtor = null) where T : class
        {
            var depth = 0;
            return (T)Create(typeof(T));p
            
            object Create(Type type)
            {
                if (type.IsArray)
                    return DefaultValue.Of(type);
                if (type == typeof(string))
                    return DefaultValue.Of(type);
                if (type.IsValueType) 
                    return DefaultValue.Of(type);
                if (type.IsInterface)
                {
                    if (!_mocks.ContainsKey(type))
                        _mocks.Add(type, _createMock(type));
                    return _mocks[type];
                }
                if (type.IsAbstract)
                    return DefaultValue.Of(type);
                
                var ctors = type.GetConstructors();
                if (ctors.Length == 0)
                    return DefaultValue.Of(type);

                var ctorIndex = chooseCtor?.Invoke(type, depth++) ?? DEFAULT_CTOR;

                var ctorParams = ctors[ctorIndex].GetParameters()
                    .Select(param =>
                        Replace(param) ??
                        Create(param.ParameterType))
                    .ToArray();

                return ctors[ctorIndex].Invoke(BindingFlags.CreateInstance, null, ctorParams, null);
            }
            
            object Replace(ParameterInfo parameterInfo)
            {
                if (paramReplace != null)
                {
                    var result = paramReplace.Invoke(parameterInfo, depth);
                    if (result.isReplaced)
                    {
                        if(result.replaceWith?.GetType() != parameterInfo.ParameterType)
                            throw new ArgumentException($"Replaced type {result.replaceWith?.GetType().Name} must be same as parameter type {parameterInfo.ParameterType.Name}");

                        return result.replaceWith;
                    }
                }
                return null;
            }
        }
    }
}


  