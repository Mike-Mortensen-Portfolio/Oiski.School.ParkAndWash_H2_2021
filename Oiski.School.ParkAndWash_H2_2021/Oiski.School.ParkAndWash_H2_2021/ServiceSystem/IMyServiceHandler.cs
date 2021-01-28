using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// Defines a container with functionality for handling <see cref="IMyServiceBase"/> <see langword="objects"/>
    /// </summary>
    public interface IMyServiceHandler
    {
        /// <summary>
        /// The collection of services attached to this <see cref="IMyServiceHandler"/>
        /// </summary>
        IReadOnlyList<IMyServiceBase> Services { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_serviceID"></param>
        /// <returns>The <see cref="IMyServiceBase"/> <see langword="object"/> that matches the <paramref name="_serviceID"/></returns>
        IMyServiceBase this[string _serviceID] { get; }

        /// <summary>
        /// Inject an <see cref="IMyServiceBase"/> <see langword="object"/> into the <see cref="IMyServiceHandler"/>
        /// </summary>
        /// <param name="_service"></param>
        void InjectService (IMyServiceBase _service);
        /// <summary>
        /// Remove an <see cref="IMyServiceBase"/> <see langword="object"/> from the <see cref="IMyServiceHandler"/>
        /// </summary>
        /// <param name="_service"></param>
        /// <returns></returns>
        bool RemoveService (IMyServiceBase _service);

        /// <summary>
        /// Find and return an <see cref="IMyServiceBase"/> <see langword="object"/> as <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Must be a derived form of <see cref="IMyServiceBase"/> or an implementation of it</typeparam>
        /// <param name="_serviceID"></param>
        /// <returns>The first occurence that matches the <paramref name="_serviceID"/> as an instance of type <typeparamref name="T"/></returns>
        T GetServiceAs<T> (string _serviceID);
    }
}