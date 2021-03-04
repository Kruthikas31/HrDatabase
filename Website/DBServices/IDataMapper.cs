using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.DBServices
{
    interface IDataMapper<T, U>
    {
        int InsertInfo(T item);
        int UpdateInfo(T item, U reference);
        List<T> SelectInfo(int id);
        // T SelectInfo(string empID);

    }
}
