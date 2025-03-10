using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        public List<PhysicianDTO> Physicians { get; private set; } = [];

        // Help with multithreading, not a perfect implementation but its functioning
        private static object _lock = new object();

        // Singleton
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    // If theres already an instance we don't need a new one
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;

            }
        }

        private static PhysicianServiceProxy? instance;

        private PhysicianServiceProxy()
        {
            instance = null;
            
            // New HTTP GET 
            var physiciansdata = new WebRequestHandler().Get("/Physician").Result;

            // Deserialize objects
            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansdata) ?? new List<PhysicianDTO>();
        }

        
        public int LastEID
        {
            // Get the last used physician ID
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.EmployeeId).Max();
                }
                return 0;
            }
        }



        //--------------------Physician--------------------\\



        public async Task<PhysicianDTO?> AddOrUpdatePhysician(PhysicianDTO physician)
        {
            // New HTTP POST
            var payload = await new WebRequestHandler().Post("/Physician", physician);

            // Deserialize
            var newPhysician = JsonConvert.DeserializeObject<PhysicianDTO>(payload);

            // Give the new physician a new ID
            newPhysician.EmployeeId = LastEID + 1;
            
            // Add new Physician
            if(newPhysician!=null && newPhysician.EmployeeId > 0 && physician.EmployeeId == 0)
            {
                Physicians.Add(newPhysician);
            }
            // Update existing Physician
            else if (newPhysician!=null && physician != null && physician.EmployeeId > 0 && physician.EmployeeId == newPhysician.EmployeeId)
            {
                // Find the correct physician
                var currentPhysician = Physicians.FirstOrDefault(p => p.EmployeeId == newPhysician.EmployeeId);
                var index = Physicians.Count;
                if (currentPhysician != null)
                {
                    // Remove from list
                    index = Physicians.IndexOf(currentPhysician);
                    Physicians.RemoveAt(index);
                }
                // Insert back into list
                Physicians.Insert(index, newPhysician);
            }
            return newPhysician;
        }

        // Delete Physician
        public async void DeletePhysician(int eid)
        {
            // Find the physician to delete
            var physiciantoremove = Physicians.FirstOrDefault(p => p.EmployeeId == eid);

            if (physiciantoremove != null)
            {
                // Remove from cache
                Physicians.Remove(physiciantoremove);

                // Send request to API to delete
                await new WebRequestHandler().Delete($"/Physician/{eid}");
            }
        }

        //public async Task<List<PatientDTO>> Search(string query)
        //{
        //    var patientsPayload = await new WebRequestHandler()
        //            .Post("/Patient/Search", new Query(query));

        //    Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
        //        ?? new List<PatientDTO>();

        //    return Patients;
        //}

        //public void AddSpecialization(int Eid)
        //{
        //    var physicianspecialization = Physicians.FirstOrDefault(p => p.EmployeeId == Eid);
        //    if (physicianspecialization != null)
        //    {
        //        Console.WriteLine("Please enter the Specialization - ");
        //        var specializations = Console.ReadLine() ?? string.Empty;
        //        physicianspecialization.Specializations.Add(specializations);
        //    }
        //    else Console.WriteLine("Physician was not found");
        //}

        //public void PrintSpecializations(int Eid)
        //{
        //    var physicianprint = Physicians.FirstOrDefault(p => p.EmployeeId == Eid);
        //    if (physicianprint != null)
        //    {
        //        Console.WriteLine("\nSpecializations: ");
        //        Console.WriteLine(string.Join("\n", physicianprint.Specializations));
        //        Console.WriteLine("\n");
        //    }
        //}
    }
}
