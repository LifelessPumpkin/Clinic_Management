using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Clinic.DTO;



namespace Library.Clinic.Models;

public class Physician
{
    private string? lname;

    public string LName
    {
        get
        {
            return lname ?? string.Empty;
        }

        set
        {
            lname = value ?? string.Empty;
        }
    }

    private string? fname;

    public string FName
    {
        get
        {
            return fname ?? string.Empty;
        }

        set
        {
            fname = value ?? string.Empty;
        }
    }

    private string? licenseNumber;

    public string LicenseNumber
    {
        get
        {
            return licenseNumber ?? string.Empty;
        }

        set
        {
            licenseNumber = value ?? string.Empty;
        }
    }
    
    public int EmployeeId{get;set;}
    
    public DateOnly GradDate{get;set;}

    public List<string> Specializations{get;set;}


    public Physician()
    {
        LName = string.Empty;
        FName = string.Empty;
        LicenseNumber = string.Empty;
        GradDate = DateOnly.MinValue;
        EmployeeId = 0;
        Specializations = [];
    }

    public Physician(PhysicianDTO p)
    {
        fname = p.FName;
        lname = p.LName;
        licenseNumber = p.LicenseNumber;
        GradDate = p.GradDate;
        EmployeeId = p.EmployeeId;
    }

    public override string ToString()
    {
        return $"{LName} - {EmployeeId}";
    }
}
