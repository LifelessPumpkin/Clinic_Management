using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Clinic.DTO;

namespace Library.Clinic.Models;
public class Patient
{
    private string? name;

    public string Name
    {
        get
        {
            return name ?? string.Empty;
        }

        set
        {
            name = value ?? string.Empty;
        }
    }

    public DateOnly Birthday{get;set;}

    public string? Gender{get;set;}

    public string? Race{get;set;}

    public string? Address{get;set;}

    public string? SSN{get;set;}

    public int Id {get;set;}

    public List<string> Diagnoses{get;set;}
        
    public List<string> Prescriptions{get;set;}
        
    public InsurancePlan? InsurancePlan { get;set;}

    public string Display 
    {
        get
        {
            return Name;
        }
    }

    public Patient()
    {
        Name = string.Empty;
        Address = string.Empty;
        Race = string.Empty;
        Gender = string.Empty;
        Birthday = DateOnly.MinValue;
        SSN = string.Empty;
        Diagnoses = [];
        Prescriptions = [];
        Id = 0;
        InsurancePlan = new InsurancePlan();
    }
    public Patient(PatientDTO p)
    {
        Id = p.Id;
        name = p.Name;
        Birthday = p.Birthday;
        Address = p.Address;
        SSN = p.SSN;
        Gender = p.Gender;
        Race = p.Race;
        InsurancePlan = p.InsurancePlan;
    }

    public override string ToString()
    {
        return $"{Name} - {Id}";
    }

}