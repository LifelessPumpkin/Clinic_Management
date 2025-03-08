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

        public List<int> HourRange { get; private set; } = [9, 10, 11, 12, 1, 2, 3, 4, 5];
        private static object _lock = new object();

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

        private AppointmentServiceProxy()
        {
            instance = null;
            Appointments = new List<Appointment>();
        }


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

        public bool ValidateAppointment(Appointment validappointment)
        {
            var appointmenttovalidate = Appointments.FirstOrDefault
            (p => p.Hour == validappointment.Hour
            && p.physician.LName == validappointment.physician.LName
            && p.StartTime.Date == validappointment.StartTime.Date);
            if (appointmenttovalidate == null) return true;
            return false;
        }

        public void DeleteAppointment(int appointmentid)
        {
            var appointmenttoremove = Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
            if (appointmenttoremove != null)
            {
                Appointments.Remove(appointmenttoremove);
            }
        }

        public void AddTreatment(int treatmentid, int appointmentid)
        {
            var TreatmentToAdd = TreatmentServiceProxy.Current.Treatments.FirstOrDefault(t => t.TreatmentId == treatmentid);
            var appointment = Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
            if (TreatmentToAdd != null && appointment != null)
            {
                appointment.TreatmentsPerformed.Add(TreatmentToAdd);
                appointment.AppointmentPrice += (TreatmentToAdd.TreatmentPrice) * (1-appointment.patient.InsurancePlan.Coverage);
            }

        }

        

    }
}
