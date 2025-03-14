﻿using Library.Clinic.DTO;
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

namespace App.Clinic.ViewModels;

public class AppointmentManagementViewModel: INotifyPropertyChanged
{
    public AppointmentManagementViewModel()
    {
        // Needs to be an observable collection
        Appointments = new ObservableCollection<AppointmentViewModel>();
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public AppointmentViewModel? SelectedAppointment { get; set; }

    public ObservableCollection<AppointmentViewModel> Appointments
    {
        get
            {
                var retval = new ObservableCollection<AppointmentViewModel>
                (
                    // Gets all the appointments from the service proxy
                    AppointmentServiceProxy
                    .Current
                    .Appointments
                    .Where(p=>p != null)
                    .Where(p=>p.patient == SelectedPatient)
                    .Select(p => new AppointmentViewModel(p))
                );
                return retval;
            }
            set{}
    }

    public PatientDTO? SelectedPatient{get;set;}
    public ObservableCollection<PatientDTO> Patients { 
        get
        {
            var retval = new ObservableCollection<PatientDTO>
            (
                // Gets the patients from the patient service proxy
                PatientServiceProxy
                .Current
                .Patients
                .Where(p=>p != null)
            );
            return retval;
        }
        
    }

    public void Delete()
    {
        if(SelectedAppointment == null)
        {
            return;
        }
        // Call the service proxy to delete the appointment
        AppointmentServiceProxy.Current.DeleteAppointment(SelectedAppointment.AppId);
        Refresh();
    }

    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Appointments));
    }

}
