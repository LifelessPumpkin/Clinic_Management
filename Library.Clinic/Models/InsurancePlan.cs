using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Library.Clinic.Models
{
    public class InsurancePlan
    {
        
        public string InsurancePlanName { get; set; }
        public double Coverage { get; set; }
        public double premium { get; set; }

        public InsurancePlan()
        {
            InsurancePlanName = string.Empty;
            Coverage = 0;
            premium = 0;
        }

        public override string ToString()
        {
            return $"{InsurancePlanName}";
        }
    }
}
