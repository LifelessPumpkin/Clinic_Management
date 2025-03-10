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
    public class TreatmentViewModel
    {
        public Treatment? model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int Id
        {
            get
            {
                if (model == null)
                {
                    return -1;
                }

                return model.TreatmentId;
            }

            set
            {
                if (model != null && model.TreatmentId != value)
                {
                    model.TreatmentId = value;
                }
            }
        }

        public double Price
        {
            get
            {
                if (model == null)
                {
                    return -1;
                }

                return model.TreatmentPrice;
            }

            set
            {
                if (model != null && model.TreatmentPrice != value)
                {
                    model.TreatmentPrice = value;
                }
            }
        }
        public string Name
        {
            get => model?.TreatmentName ?? string.Empty;
            set
            {
                if (model != null)
                {
                    model.TreatmentName = value;
                }
            }
        }

        public TreatmentViewModel()
        {
            model = new Treatment();
            SetUpCommands();
        }

        public TreatmentViewModel(Treatment? _model)
        {
            model = _model;
            SetUpCommands();
        }

        public void SetUpCommands()
        {
            // Commands to attach to the treatments in the management page
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((t) => DoEdit(t as TreatmentViewModel));
        }

        private void DoDelete()
        {
            // Call treatment service proxy to delete the treatment
            if (Id > 0) TreatmentServiceProxy.Current.DeleteTreatment(Id);
            
            // Go back to management page
            Shell.Current.GoToAsync("//Treatments");
        }

        private void DoEdit(TreatmentViewModel? tvm)
        {
            if (tvm == null) return;
            var selectedTreatmentId = tvm?.Id ?? 0;
            // Go to the selected treatment page
            Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
        }

        public void ExecuteAdd()
        {
            if (model != null)
            {
                // Call the service proxy to add the treatment
                TreatmentServiceProxy
                .Current
                .CreateOrUpdateTreatment(model);
            }

            // Go back to the management page
            Shell.Current.GoToAsync("//Treatments");
        }
    }
}

