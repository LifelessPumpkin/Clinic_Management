using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

namespace Library.Clinic.Services;

public class PatientServiceProxy
{

    public List<PatientDTO> Patients { get; private set; } = [];
    public List<InsurancePlan> InsurancePlans { get; private set; } = [];
    public List<int> HourRange { get; private set; } = [9, 10, 11, 12, 1, 2, 3, 4, 5];

    private static object _lock = new object();

    public static PatientServiceProxy Current
    {
        get
        {
            lock (_lock)
            {
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
        var patientsData = new WebRequestHandler().Get("/Patient").Result;
        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
    }

    public int LastKey
    {
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
        var payload = await new WebRequestHandler().Post("/Patient", patient); // here
        var newPatient = JsonConvert.DeserializeObject<PatientDTO>(payload);
        newPatient.Id = LastKey + 1;
        if (newPatient != null && newPatient.Id > 0 && patient.Id == 0) // should be new patient.id >0, but there is an error so this is temp fix
        {
            //new patient to be added to the list
            Patients.Add(newPatient);
        }
        else if (newPatient != null && patient != null && patient.Id > 0 && patient.Id == newPatient.Id)
        {
            //edit, exchange the object in the list
            var currentPatient = Patients.FirstOrDefault(p => p.Id == newPatient.Id);
            var index = Patients.Count;
            if (currentPatient != null)
            {
                index = Patients.IndexOf(currentPatient);
                Patients.RemoveAt(index);
            }
            Patients.Insert(index, newPatient);
        }

        return newPatient;
    }
    public async Task<List<PatientDTO>> RetrievePatients()
    {
        var patientsPayload = await new WebRequestHandler().Get("/Patient");

        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
            ?? new List<PatientDTO>();

        return Patients;
    }
    public async Task<List<PatientDTO>> Search(string query)
    {
        var patientsPayload = await new WebRequestHandler()
                .Post("/Patient/Search", new Query(query));

        Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
            ?? new List<PatientDTO>();

        return Patients;
    }
    public async void DeletePatient(int id)
    {
        var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);

        if (patientToRemove != null)
        {
            Patients.Remove(patientToRemove);

            await new WebRequestHandler().Delete($"/Patient/{id}");
        }
    }

    //public void AddDiagnosis(int id)
    //{
    //    var patientdiagnosis = Patients.FirstOrDefault(p => p.Id == id);
    //    if (patientdiagnosis != null)
    //    {
    //        Console.WriteLine("Please enter the diagnosis - ");
    //        var ailment = Console.ReadLine() ?? string.Empty;
    //        patientdiagnosis.Diagnoses.Add(ailment);
    //    }
    //}

    //public void AddPrescription(int id)
    //{
    //    var patientprescription = Patients.FirstOrDefault(p => p.Id == id);
    //    if (patientprescription != null)
    //    {
    //        Console.WriteLine("Please enter the Prescription - ");
    //        var ailment = Console.ReadLine() ?? string.Empty;
    //        patientprescription.Prescriptions.Add(ailment);
    //    }
    //    else Console.WriteLine("Patient was not found");
    //}

}