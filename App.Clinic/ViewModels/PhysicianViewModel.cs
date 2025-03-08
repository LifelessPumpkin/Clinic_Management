using App.Clinic.Views;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel
    {
        private PhysicianDTO? model { get; set; }
        public ICommand? DeleteCommand {get;set;}
        public ICommand? EditCommand {get;set;}
        public int EID
        {
            get
            {
                if(model == null)
                {
                    return -1;
                }

                return model.EmployeeId;
            }

            set
            {
                if(model != null && model.EmployeeId != value) {
                    model.EmployeeId = value;
                }
            }
        }

        public string LastName
        {
            get => model?.LName ?? string.Empty;
            set
            {
                if(model != null)
                {
                    model.LName = value;
                }
            }
        }

        public string FirstName
        {
            get => model?.FName ?? string.Empty;
            set
            {
                if(model != null)
                {
                    model.FName = value;
                }
            }
        }

        public PhysicianViewModel()
        {
            model = new PhysicianDTO();
            SetUpCommands();
        }

        public PhysicianViewModel(PhysicianDTO? _model)
        {
            model = _model;
            SetUpCommands();
        }

        public void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p)=> DoEdit(p as PhysicianViewModel));
        }

        private void DoDelete()
        {
            if(EID > 0) PhysicianServiceProxy.Current.DeletePhysician(EID);
            Shell.Current.GoToAsync("//Physicians");
        }

        private void DoEdit(PhysicianViewModel? pvm)
        {
            if(pvm == null) return;
            var selectedPhysicianEID = pvm?.EID ?? 0;
            Shell.Current.GoToAsync($"//PhysicianDetails?physicianEID={selectedPhysicianEID}");
        }

        public async void ExecuteAdd()
        {
            if (model != null)
            {
                await PhysicianServiceProxy
                .Current
                .AddOrUpdatePhysician(model);
            }
            await Shell.Current.GoToAsync("//Physicians");
        }
    }
}