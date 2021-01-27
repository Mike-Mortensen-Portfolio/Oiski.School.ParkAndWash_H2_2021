using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyRepository<T>
    {
        IEnumerable<T> GetEnumerable ();
        T GetDataByIdentifier<IDType> (IDType _identifier);
        bool InsertData (T _data);
        bool DeleteData<IDType> (IDType _identifier);
        bool UpdateData (T _data);
    }
}