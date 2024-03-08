using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.service
{
    public class Medicine 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Costs { get; set; }
        public string OnPrescription { get; set; }
        public int Count { get; set; }
        public string ExpirationDate { get; set; }
        public string Volume { get; set; }
        public string PrimaryPackaging { get; set; }
        public string ActiveSubstance { get; set; }
        public string SpecialProperties { get; set; }
        public string ReleaseForm { get; set; }
    }
}
