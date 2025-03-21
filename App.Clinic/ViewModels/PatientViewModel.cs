﻿using App.Clinic.Views;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public PatientDTO? model { get; set; }
        public ICommand? DeleteCommand {get;set;}
        public ICommand? EditCommand {get;set;}

        public int Id
        {
            get
            {
                if(model == null)
                {
                    return -1;
                }

                return model.Id;
            }

            set
            {
                if(model != null && model.Id != value) {
                    model.Id = value;
                }
            }
        }

        public string Name
        {
            get => model?.Name ?? string.Empty;
            set
            {
                if(model != null)
                {
                    model.Name = value;
                }
            }
        }
        public InsurancePlan? SelectedInsurancePlan
        {
            get => model?.InsurancePlan ?? new InsurancePlan() { InsurancePlanName="error"};
            set
            {
                if(model != null && value != null)
                {
                    model.InsurancePlan = value;
                }
            }
        }
        public ObservableCollection<InsurancePlan> InsurancePlans
        {
            get
            {
                var retval = new ObservableCollection<InsurancePlan>
                (
                    // Gets all available insurance plans from service proxy
                    PatientServiceProxy
                    .Current
                    .InsurancePlans
                    .Where(i => i != null)
                );
                return retval;
            }

        }

        public PatientViewModel()
        {
            model = new PatientDTO();
            SetUpCommands();
        }

        public PatientViewModel(PatientDTO? _model)
        {
            model = _model;
            SetUpCommands();
        }

        public void SetUpCommands()
        {
            // Commands to attach to the patients
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p)=> DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            // Call service proxy to delete the patient
            if(Id > 0) PatientServiceProxy.Current.DeletePatient(Id);
            Shell.Current.GoToAsync("//Patients");
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if(pvm == null) return;
            var selectedPatientId = pvm?.Id ?? 0;
            // Go to the patient details page to edit
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public async void ExecuteAdd()
        {
            if (model != null)
            {
                // Call service proxy to add the patient
                await PatientServiceProxy
                .Current
                .AddOrUpdatePatient(model);
            }
            
            // Go back to management page
            await Shell.Current.GoToAsync("//Patients");
        }
    }
}
