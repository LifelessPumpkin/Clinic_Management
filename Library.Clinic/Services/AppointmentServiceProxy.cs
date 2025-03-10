using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        public List<Appointment> Appointments { get; private set; } = [];

        // Available hours for appointment
        public List<int> HourRange { get; private set; } = [9, 10, 11, 12, 1, 2, 3, 4, 5];

        // Help with multithreading
        private static object _lock = new object();

        // Singleton
        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;

            }
        }

        private static AppointmentServiceProxy? instance;

        // TODO: Add persistence and connect to API
        private AppointmentServiceProxy()
        {
            instance = null;
            Appointments = new List<Appointment>();
        }

        // Function to increment last appointment ID
        public int LastAID
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.AppointmentId).Max();
                }
                return 0;
            }
        }




        //--------------------Appointment--------------------\\


        public void CreateOrUpdateAppointment(Appointment appointment)
        {
            bool isAdd = false;
            // Create a new appointment
            if (appointment.AppointmentId <= 0)
            {
                appointment.AppointmentId = LastAID + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Appointments.Add(appointment);
            }
        }

        // Validate Appointment (Deprecated)
        public bool ValidateAppointment(Appointment validappointment)
        {
            var appointmenttovalidate = Appointments.FirstOrDefault
            // Check that new appointment doesn't conflict with existing physician appointment and that it is within valid hours
            (p => p.Hour == validappointment.Hour
            && p.physician.LName == validappointment.physician.LName
            && p.StartTime.Date == validappointment.StartTime.Date);
            if (appointmenttovalidate == null) return true;
            return false;
        }

        // Delete Appointment
        public void DeleteAppointment(int appointmentid)
        {
            // Find appointment to remove
            var appointmenttoremove = Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
            
            if (appointmenttoremove != null)
            {
                Appointments.Remove(appointmenttoremove);
            }
        }

        // Add treatment performed to appointment
        public void AddTreatment(int treatmentid, int appointmentid)
        {
            // Find the treatment from the treatment list
            var TreatmentToAdd = TreatmentServiceProxy.Current.Treatments.FirstOrDefault(t => t.TreatmentId == treatmentid);
            
            // Find the appointment from appointment list
            var appointment = Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
            
            if (TreatmentToAdd != null && appointment != null)
            {
                // Add the treatment into the appointment
                appointment.TreatmentsPerformed.Add(TreatmentToAdd);

                // Add the price of the treatment to the appointment
                appointment.AppointmentPrice += (TreatmentToAdd.TreatmentPrice) * (1-appointment.patient.InsurancePlan.Coverage);
            }

        }

        

    }
}
