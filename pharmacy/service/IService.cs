using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.service
{
    internal interface IService<T>
    {
        T GetAll();
        void Delete(int id);
    }
}
