using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="IDType">The type of the identifier for the <see cref="IMyRepositoryEntity{IDType, SaveType}"/></typeparam>
    /// <typeparam name="SaveType">The type the <see cref="IMyRepositoryEntity{IDType, SaveType}"/> will save its state as or load a previous state from</typeparam>
    public interface IMyRepositoryEntity<IDType, SaveType>
    {
        /// <summary>
        /// The ID to identify the Entity in the <see cref="IMyRepository{EntityType, SaveType}"/>
        /// </summary>
        IDType ID { get; }

        /// <summary>
        /// Save the current state
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        SaveType SaveEntity ();

        /// <summary>
        /// Restore a previous state based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        void BuildEntity (SaveType _data);
    }
}
