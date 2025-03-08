using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Clinic.DTO;

namespace Library.Clinic.Models;

public class Appointment
{
    public PhysicianDTO physician { get; set; }

    public PatientDTO patient { get; set; }

    public int AppointmentId { get; set; }

    public int Hour { get; set; }

    public string AppointmentType { get; set; }

    public DateTime StartTime { get; set; }

    public double AppointmentPrice {get;set;}

    public double AppointmentPriceAfterInsurance {get;set;}

    public List<Treatment> TreatmentsPerformed { get; set; } = [];

    // public DateTime EndTime{get;set;}
    public Appointment()
    {
        patient = new PatientDTO();
        physician = new PhysicianDTO();
        AppointmentType = string.Empty;
        AppointmentId = 0;
        Hour = 0;
        StartTime = DateTime.Today;
        AppointmentPrice = 0;
        AppointmentPriceAfterInsurance = 0;
        // EndTime = DateTime.Today;
    }

    public override string ToString()
    {
        return $"\nAppointment Date - {StartTime}\nPatient - {patient}\nPhysician - {physician}\n";
    }

}