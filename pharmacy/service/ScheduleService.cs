using pharmacy.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy
{
    internal class ScheduleService: IService<string>
    {
        public ScheduleService()
        {
        }

        public void Add(string opening, string ending, string openingOnWeekands, string endingOnWeekands)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
