using Api.Clinic.Database;
using Api.ToDoApplication.Persistence;
using Library.Clinic.DTO;
using Library.Clinic.Models;

namespace Api.Clinic.Enterprise
{
    public class PatientEC
    {
        public PatientEC() { }
        public IEnumerable<PatientDTO> Patients
        {
            get => new List<PatientDTO>(Filebase.Current.Patients.Select(p=>new PatientDTO(p)));
        }
        public IEnumerable<PatientDTO>? Search(string query)
        {
            return Filebase.Current.Patients
                .Where(p => p.Name.ToUpper()
                    .Contains(query?.ToUpper() ?? string.Empty))
                .Select(p => new PatientDTO(p));
        }
        public PatientDTO? GetById(int id)
        {
            var patient = Filebase.Current.Patients.FirstOrDefault(p => p.Id == id);
            if (patient != null) return new PatientDTO(patient);
            return null;
        }
        public void Delete(int id)
        {
            Filebase.Current.DeletePatient(id);
        }
        public PatientDTO? AddOrUpdate(PatientDTO? patient)
        {
            if (patient == null) return null;
            Filebase.Current.CreateOrUpdatePatient(new Patient(patient));
            return patient;
        }
    }
}
