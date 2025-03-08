using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.DTO
{
    public class PhysicianDTO
    {
        public override string ToString()
        {
            return $"[{LName}] - {EmployeeId}";
        }
        public string Display
        {
            get
            {
                return $"{LName} - {EmployeeId}";
            }
        }
        public int EmployeeId { get; set; }
        public string? LName { get; set; }
        public string? FName { get; set; }
        public DateOnly GradDate { get; set; }
        public string? LicenseNumber { get; set; }
        public PhysicianDTO() { }
        public PhysicianDTO(Physician p)
        {
            EmployeeId = p.EmployeeId;
            LName = p.LName;
            FName = p.FName;
            GradDate = p.GradDate;
            LicenseNumber = p.LicenseNumber;

        }
    }
}
