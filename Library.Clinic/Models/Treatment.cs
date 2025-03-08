using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Treatment
    {
        public string TreatmentName { get; set; }
        public double TreatmentPrice { get; set; }
        public int TreatmentId { get; set; }

        public Treatment()
        {
            TreatmentName = string.Empty;
            TreatmentPrice = 0;
            TreatmentId = 0;
        }

        public override string ToString()
        {
            return $"{TreatmentName} - {TreatmentPrice}";
        }
    }
}

