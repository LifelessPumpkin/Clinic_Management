using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class TreatmentServiceProxy
    {
        public List<Treatment> Treatments { get; private set; } = [];
        // Not connected to persistence or DTO yet

        // Help with multithreading, not a perfect implementation but its functioning
        private static object _lock = new object();

        public static TreatmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    // If theres already an instance we don't need a new one
                    if (instance == null)
                    {
                        instance = new TreatmentServiceProxy();
                    }
                }
                return instance;

            }
        }

        private static TreatmentServiceProxy? instance;

        private TreatmentServiceProxy()
        {
            instance = null;
            // Initialize a list of example treatments
            Treatments = new List<Treatment>
            {
                new Treatment
                {
                    TreatmentName = "MRI",
                    TreatmentPrice = 900,
                    TreatmentId=1
                },
                new Treatment
                {
                    TreatmentName = "CT Scan",
                    TreatmentPrice = 700,
                    TreatmentId=2
                },
                new Treatment
                {
                    TreatmentName = "X-Ray",
                    TreatmentPrice = 150,
                    TreatmentId=3
                },
                new Treatment
                {
                    TreatmentName = "Blood Test",
                    TreatmentPrice = 50,
                    TreatmentId=4
                },
                new Treatment
                {
                    TreatmentName = "Physical Therapy Session",
                    TreatmentPrice = 100,
                    TreatmentId=5
                },
                new Treatment
                {
                    TreatmentName = "Ultrasound",
                    TreatmentPrice = 300,
                    TreatmentId=5
                },
                new Treatment
                {
                    TreatmentName = "ECG (Electrocardiogram)",
                    TreatmentPrice = 200,
                    TreatmentId=6
                },
                new Treatment
                {
                    TreatmentName = "Dental Cleaning",
                    TreatmentPrice = 120,
                    TreatmentId=7
                },
                new Treatment
                {
                    TreatmentName = "Allergy Testing",
                    TreatmentPrice = 250,
                    TreatmentId=8
                },
                new Treatment
                {
                    TreatmentName = "Flu Shot",
                    TreatmentPrice = 40,
                    TreatmentId=9
                }
            
            };
        }

        public int LastTKey
        {
            // Basic function to increment keys
            get
            {
                if (Treatments.Any())
                {
                    return Treatments.Select(x => x.TreatmentId).Max();
                }
                return 0;
            }
        }

        public void CreateOrUpdateTreatment(Treatment T)
        {
            bool isAdd = false;
            // If there is not an ID, create a new treatment
            if (T.TreatmentId <= 0)
            {
                T.TreatmentId = LastTKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Treatments.Add(T);
            }
        }

        public void DeleteTreatment(int Id)
        {
            // Delete treatment
            var treatmenttoremove = Treatments.FirstOrDefault(t => t.TreatmentId == Id);
            if(treatmenttoremove!=null)Treatments.Remove(treatmenttoremove);
        }
    }
}
