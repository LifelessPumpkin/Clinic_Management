using Api.Clinic.Enterprise;
using Library.Clinic.Models;
using Library.Clinic.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _patientRoot;
        private string _physicianRoot;
        private static Filebase? _instance;

        private static object _lock = new object();

        public static Filebase Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Filebase();
                    }
                }
                return _instance;

            }
        }

        private Filebase()
        {
            //_instance = null;
            _root = @"C:\Persistence";
            _patientRoot = $"{_root}\\Patients";
            _physicianRoot = $"{_root}\\Physicians";
        }

        public int LastKey
        {
            get
            {
                if (Patients.Any()) return Patients.Select(x => x.Id).Max();
                return 0;
            }
        }
        
        public int LastKeyPhysician
        {
            get
            {
                if (Physicians.Any()) return Physicians.Select(x => x.EmployeeId).Max();
                return 0;
            }
        }

        public Patient CreateOrUpdatePatient(Patient patient)
        {
            //set up a new Id if one doesn't already exist
            if (patient.Id<=0)
            {
                patient.Id = LastKey+1;
            }

            //go to the right place]
            string path = $"{_patientRoot}//{patient.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));

            //return the item, which now has an id
            return patient;
        }

        public Physician CreateOrUpdatePhysician(Physician physician)
        {
            //set up a new Id if one doesn't already exist
            if (physician.EmployeeId <= 0)
            {
                physician.EmployeeId = LastKeyPhysician + 1;
            }

            //go to the right place]
            string path = $"{_physicianRoot}//{physician.EmployeeId}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(physician));

            //return the item, which now has an id
            return physician;
        }

        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert.DeserializeObject<Patient>(File.ReadAllText(patientFile.FullName));
                    if (patient != null) _patients.Add(patient);
                }
                return _patients;
            }
        }

        public List<Physician> Physicians
        {
            get
            {
                var root = new DirectoryInfo(_physicianRoot);
                var _physicians = new List<Physician>();
                foreach (var physfile in root.GetFiles())
                {
                    var physician = JsonConvert.DeserializeObject<Physician>(File.ReadAllText(physfile.FullName));
                    if (physician != null) _physicians.Add(physician);
                }
                return _physicians;
            }
        }

        public bool DeletePatient(int id)
        {
            string path = $"{_patientRoot}//{id}.json";
            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return true;
            }
            return false;
        }

        public bool DeletePhysician (int id)
        {
            string path = $"{_physicianRoot}//{id}.json";
            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
