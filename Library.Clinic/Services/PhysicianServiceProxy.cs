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

        private static object _lock = new object();

        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
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
            var physiciansdata = new WebRequestHandler().Get("/Physician").Result;
            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansdata) ?? new List<PhysicianDTO>();
        }

        
        public int LastEID
        {
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
            var payload = await new WebRequestHandler().Post("/Physician", physician);
            var newPhysician = JsonConvert.DeserializeObject<PhysicianDTO>(payload);
            newPhysician.EmployeeId = LastEID + 1;
            if(newPhysician!=null && newPhysician.EmployeeId > 0 && physician.EmployeeId == 0)
            {
                Physicians.Add(newPhysician);
            }
            else if (newPhysician!=null && physician != null && physician.EmployeeId > 0 && physician.EmployeeId == newPhysician.EmployeeId)
            {
                var currentPhysician = Physicians.FirstOrDefault(p => p.EmployeeId == newPhysician.EmployeeId);
                var index = Physicians.Count;
                if (currentPhysician != null)
                {
                    index = Physicians.IndexOf(currentPhysician);
                    Physicians.RemoveAt(index);
                }
                Physicians.Insert(index, newPhysician);
            }
            return newPhysician;
        }


        //public async Task<List<PatientDTO>> Search(string query)
        //{
        //    var patientsPayload = await new WebRequestHandler()
        //            .Post("/Patient/Search", new Query(query));

        //    Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
        //        ?? new List<PatientDTO>();

        //    return Patients;
        //}
        public async void DeletePhysician(int eid)
        {
            var physiciantoremove = Physicians.FirstOrDefault(p => p.EmployeeId == eid);

            if (physiciantoremove != null)
            {
                Physicians.Remove(physiciantoremove);

                await new WebRequestHandler().Delete($"/Physician/{eid}");
            }
        }

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
