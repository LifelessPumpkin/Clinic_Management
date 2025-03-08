using Api.Clinic.Database;
using Api.ToDoApplication.Persistence;
using Library.Clinic.DTO;
using Library.Clinic.Models;

namespace Api.Clinic.Enterprise
{
    public class PhysicianEC
    {
        public PhysicianEC() { }
        public IEnumerable<PhysicianDTO> Physicians
        {
            get => Filebase.Current.Physicians.Select(x => new PhysicianDTO(x));
        }
        //public IEnumerable<PhysicianDTO>? Search(string query)
        //{
        //    return Filebase.Current.Physicians
        //        .Where(p => p.LName.ToUpper()
        //            .Contains(query?.ToUpper() ?? string.Empty))
        //        .Select(p => new PhysicianDTO(p));
        //}
        public PhysicianDTO? GetById(int id)
        {
            var physician = Filebase.Current.Physicians.FirstOrDefault(p => p.EmployeeId == id);
            if (physician != null) return new PhysicianDTO(physician);
            return null;
        }
        public void Delete(int id)
        {
            Filebase.Current.DeletePhysician(id);
        }
        public PhysicianDTO? AddOrUpdate(PhysicianDTO? physician)
        {
            if (physician == null) return null;
            Filebase.Current.CreateOrUpdatePhysician(new Physician(physician));
            return physician;
        }
    }
}
