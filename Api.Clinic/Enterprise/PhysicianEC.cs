﻿using Api.Clinic.Database;
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

        // This should work now just need to do some testing
        //public IEnumerable<PhysicianDTO>? Search(string query)
        //{
        //    return Filebase.Current.Physicians
        //        .Where(p => p.LName.ToUpper()
        //            .Contains(query?.ToUpper() ?? string.Empty))
        //        .Select(p => new PhysicianDTO(p));
        //}

        public PhysicianDTO? GetById(int id)
        {
            // Find the correct physician
            var physician = Filebase.Current.Physicians.FirstOrDefault(p => p.EmployeeId == id);
            if (physician != null) return new PhysicianDTO(physician);
            return null;
        }

        public void Delete(int id)
        {
            // Calls filebase to delete physician
            Filebase.Current.DeletePhysician(id);
        }
        public PhysicianDTO? AddOrUpdate(PhysicianDTO? physician)
        {
            if (physician == null) return null;
            
            // calls filebaes to create/update physician
            Filebase.Current.CreateOrUpdatePhysician(new Physician(physician));
            return physician;
        }
    }
}
