using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

namespace Library.Clinic.Services;

public class PatientServiceProxy
{

    public List<PatientDTO> Patients { get; private set; } = [];
    public List<InsurancePlan> InsurancePlans { get; private set; } = [];
    // Available hours to set an appointment
    public List<int> HourRange { get; private set; } = [9, 10, 11, 12, 1, 2, 3, 4, 5];
    
    // Help with multithreading
    private static object _lock = new object();

    //Singleton
    public static PatientServiceProxy Current
    {
        get
        {
            lock (_lock)
            {
                // If theres already an instance we don't need a new one
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
            }
            return instance;

        }
    }

    private static PatientServiceProxy? instance;

    private PatientServiceProxy()
    {
        instance = null;
        // Insurance might deserve its own model, DTO, and service proxy
        InsurancePlans = new List<InsurancePlan>
        {
            //Economy Plan – 15% Coverage - $95
            //Basic Plan – 40% Coverage - $200
            //Standard Plan – 60% Coverage - $375
            //Premium Plan – 80% Coverage - $600
            //Executive Plan - 90% Coverage - $1,000
            new InsurancePlan
            {
                InsurancePlanName = "Economy",
                Coverage = .15,
                premium = 95
            },
            new InsurancePlan
            {
                InsurancePlanName = "Basic",
                Coverage = .40,
                premium = 200
            },
            new InsurancePlan
            {
                InsurancePlanName = "Standard",
                Coverage = .60,
                premium = 375
            },
            new InsurancePlan
            {
                InsurancePlanName = "Premium",
                Coverage = .80,
                premium = 600
            },
            new InsurancePlan
            {
                InsurancePlanName = "Executive",
                Coverage = .90,
                premium = 1000
            }
        };
        // Get Web Request to fetch all the patients from API
        var patientsData = new WebRequestHandler().Get("/Patient").Result;
        // Deserialize the list
        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
    }

    public int LastKey
    {
        // Basic function to increment keys
        get
        {
            if (Patients.Any())
            {
                return Patients.Select(x => x.Id).Max();
            }
            return 0;
        }
    }



    //--------------------Patient--------------------\\
    public async Task<PatientDTO?> AddOrUpdatePatient(PatientDTO patient)
    {
        // New HTTP POST
        var payload = await new WebRequestHandler().Post("/Patient", patient); 

        // Deserialize it
        var newPatient = JsonConvert.DeserializeObject<PatientDTO>(payload);

        // Fetch and increment last used ID 
        // DONT DELETE THIS , IT WILL CAUSE BREAKS
        newPatient.Id = LastKey + 1;
        
        // New patient to be added to the list
        if (newPatient != null && newPatient.Id > 0 && patient.Id == 0) 
        {
            Patients.Add(newPatient);
        }
        
        // Edit, exchange the object in the list
        else if (newPatient != null && patient != null && patient.Id > 0 && patient.Id == newPatient.Id)
        {
            // Find the patient in the list
            var currentPatient = Patients.FirstOrDefault(p => p.Id == newPatient.Id);
            var index = Patients.Count;
            if (currentPatient != null)
            {
                // Remove the patient
                index = Patients.IndexOf(currentPatient);
                Patients.RemoveAt(index);
            }
            // Insert again
            Patients.Insert(index, newPatient);
        }

        return newPatient;
    }

    
    public async Task<List<PatientDTO>> RetrievePatients()
    {
        // New HTTP GET
        var patientsPayload = await new WebRequestHandler().Get("/Patient");

        // Deserialize objects
        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
            ?? new List<PatientDTO>();

        return Patients;
    }

    public async Task<List<PatientDTO>> Search(string query)
    {
        // New HTTP Post with search query
        var patientsPayload = await new WebRequestHandler()
                .Post("/Patient/Search", new Query(query));

        // Deserialize object
        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
            ?? new List<PatientDTO>();

        return Patients;
    }
    public async void DeletePatient(int id)
    {
        // Find the patient to be deleted
        var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);

        if (patientToRemove != null)
        {
            // Remove from cache
            Patients.Remove(patientToRemove);

            // New HTTP DELETE
            await new WebRequestHandler().Delete($"/Patient/{id}");
        }
    }

    // NEED TO ADD API FUNCTIONALITY
    /*public void AddDiagnosis(int id)
    {
       var patientdiagnosis = Patients.FirstOrDefault(p => p.Id == id);
       if (patientdiagnosis != null)
       {
           Console.WriteLine("Please enter the diagnosis - ");
           var ailment = Console.ReadLine() ?? string.Empty;
           patientdiagnosis.Diagnoses.Add(ailment);
       }
    }*/

    // NEED TO ADD API FUNCTIONALITY
    /*public void AddPrescription(int id)
    {
       var patientprescription = Patients.FirstOrDefault(p => p.Id == id);
       if (patientprescription != null)
       {
           Console.WriteLine("Please enter the Prescription - ");
           var ailment = Console.ReadLine() ?? string.Empty;
           patientprescription.Prescriptions.Add(ailment);
       }
       else Console.WriteLine("Patient was not found");
    }*/

}