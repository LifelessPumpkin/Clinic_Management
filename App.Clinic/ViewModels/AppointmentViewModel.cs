using App.Clinic.Views;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels;

public class AppointmentViewModel : INotifyPropertyChanged
{   
    private Appointment? model { get; set; }
    public ICommand? DeleteCommand {get;set;}
    public ICommand? EditCommand {get;set;}
    public ICommand? AddTreatmentCommand {get;set;}
    public int PatId
    {
        get
        {
            if(model == null)
            {
                return -1;
            }

            return model.patient.Id;
        }

        set
        {
            if(model != null && model.patient.Id != value) {
                model.patient.Id = value;
            }
        }
    }
    public string PhysLName
    {
        get => model?.physician.LName ?? string.Empty;
        set
        {
            if(model != null)
            {
                model.physician.LName = value;
            }
        }
    }
    public int AppId
    {
        get
        {
            if(model == null)
            {
                return -1;
            }

            return model.AppointmentId;
        }

        set
        {
            if(model != null && model.AppointmentId != value) {
                model.AppointmentId = value;
            }
        }
    }
    public double PostInsurancePrice
    {
        get
        {
            if (model != null) return model.AppointmentPriceAfterInsurance;
            return -1;
        }
        set
        {
        }
    }
    public double PreInsurancePrice
    {
        get
        {
            if (model != null) return model.AppointmentPrice;
            return -1;
        }
        set
        {
        }
    }
    public int StartHour
    {
        get => model?.Hour ?? 0;
        set
        {
            if(model != null)
            {
                model.Hour = value;
            }
        }
    }
    private DateTime tempstartdate;
    public DateTime StartDate
{
    get
    {
        return tempstartdate != DateTime.MinValue ? tempstartdate : model?.StartTime.Date ?? DateTime.Today;
    }
    set
    {
        tempstartdate = value;
    }
}
    public string DisplayStartDate
    {
        get
        {
            return model?.StartTime.Date.ToShortDateString() ?? string.Empty;
        }
    }
    public DateTime MinStartDate
    {
        get
        {
            return DateTime.Today;
        }
    }
    public PatientDTO? SelectedPatient 
    { 
        get
        {
            return model?.patient;
        }

        set
        {
            var selectedPatient = value;
            if(model != null && selectedPatient != null)
            {
                model.patient = selectedPatient;
                // model.patientId = selectedPatient?.Id ?? 0;
            }

        }
    }
    private PhysicianDTO? tempphysician;
    public PhysicianDTO? SelectedPhysician 
{ 
    get
    {
        return tempphysician ?? model?.physician;
    }

    set
    {
        tempphysician = value;
    }
}
    private int temphour;
    public int SelectedHour
{
    get
    {
        return temphour != 0 ? temphour : model?.Hour ?? 0;
    }

    set
    {
        temphour = value;
    }
}
    //public string TreatName
    //{
    //    get => model?. ?? string.Empty;
    //    set
    //    {
    //        if (model != null)
    //        {
    //            model.Name = value;
    //        }
    //    }
    //}
    public ObservableCollection<int> AppointmentHourRange { 
        get
        {
            return new ObservableCollection<int>(PatientServiceProxy.Current.HourRange);
        }
    }
    public ObservableCollection<PatientDTO> Patients { 
        get
        {
            var retval = new ObservableCollection<PatientDTO>
            (
                PatientServiceProxy
                .Current
                .Patients
                .Where(p=>p != null)
            );
            return retval;
        }
        
    }
    public ObservableCollection<PhysicianDTO> Physicians { 
        get
        {
            var retval = new ObservableCollection<PhysicianDTO>
            (
                PhysicianServiceProxy
                .Current
                .Physicians
                .Where(p=>p != null)
            );
            return retval;
        }
        
    }
    public ObservableCollection<Treatment> AvailableTreatments
    {
        get
        {
            var retval = new ObservableCollection<Treatment>
                (
                    TreatmentServiceProxy.Current.Treatments.Where(p => p != null)
                );
            return retval;
        }
    }
    public ObservableCollection<Treatment> TreatmentsCompleted
    {
        get
        {

            var retval = new ObservableCollection<Treatment>(
            model?.TreatmentsPerformed?.Where(t => t != null) ?? Enumerable.Empty<Treatment>());
            return retval;
        }
    }
    private Treatment? temptreatment;
    public Treatment? SelectedTreatment
    {
        get => temptreatment;
        set
        {
            if (model != null && value != null)
            {
                temptreatment = value;
            }
        }
    }
    public string? InsPlan
    {
        get => model?.patient?.InsurancePlan?.InsurancePlanName ?? "ERROR";
    }
    public AppointmentViewModel()
    {
        model = new Appointment();
        SetUpCommands();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public AppointmentViewModel(Appointment? _model)
    {
        model = _model;
        SetUpCommands();
    }
    public void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command((p)=> DoEdit(p as AppointmentViewModel));
        AddTreatmentCommand = new Command(DoTreatmentAdd);
    }
    private void DoDelete()
    {
        if(AppId > 0) AppointmentServiceProxy.Current.DeleteAppointment(AppId);
        Shell.Current.GoToAsync("//Appointments");
    }
    private void DoTreatmentAdd()
    {
        if (model != null && SelectedTreatment != null)
        {
            model.TreatmentsPerformed.Add(SelectedTreatment);
            model.AppointmentPrice += SelectedTreatment.TreatmentPrice;
            model.AppointmentPriceAfterInsurance += (SelectedTreatment.TreatmentPrice * (1 - model.patient.InsurancePlan.Coverage));
            Refresh();
        }
    }
    private void DoEdit(AppointmentViewModel? avm)
    {
        if(avm == null) return;
        var selectedappointmentid = avm?.AppId ?? 0;
        Shell.Current.GoToAsync($"//AppointmentDetails?appointmentid={selectedappointmentid}");
    }
    public void ExecuteAdd()
    {
        if (model!=null)
        {
            var tempApt = new Appointment
            {
                physician = tempphysician ?? model.physician,
                Hour = temphour != 0 ? temphour : model.Hour,
                StartTime = tempstartdate != DateTime.MinValue ? tempstartdate : model.StartTime
            };
            if(AppointmentServiceProxy.Current.ValidateAppointment(tempApt))
            {
                model.physician = tempApt.physician;
                model.Hour = tempApt.Hour;
                model.StartTime = tempApt.StartTime;

                AppointmentServiceProxy
                .Current
                .CreateOrUpdateAppointment(model);
                Shell.Current.GoToAsync("//Appointments"); 
            }
            else
            {
                Shell.Current.DisplayAlert("Error", "Choose a different time or physician.", "OK");
            }
        }
    }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(TreatmentsCompleted));
        NotifyPropertyChanged(nameof(PreInsurancePrice));
        NotifyPropertyChanged(nameof(PostInsurancePrice));
    }

}