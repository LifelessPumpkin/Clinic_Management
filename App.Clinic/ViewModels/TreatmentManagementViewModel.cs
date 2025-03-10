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

namespace App.Clinic.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        public TreatmentManagementViewModel()
        {
            // Needs to be an observable collection
            Treatments = new ObservableCollection<TreatmentViewModel>();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        // Enables properties to be refreshed
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TreatmentViewModel? SelectedTreatment { get; set; }

        public ObservableCollection<TreatmentViewModel> Treatments
        {
            get
            {
                // Fetches treatments from treatment service proxy
                var retval = new ObservableCollection<TreatmentViewModel>
                (
                    TreatmentServiceProxy
                    .Current
                    .Treatments
                    .Where(t => t != null)
                    .Select(t => new TreatmentViewModel(t))
                );
                return retval;
            }
            set { }
        }

        public void Delete()
        {
            if (SelectedTreatment == null)
            {
                return;
            }
            // Call treatment service proxy to delete
            TreatmentServiceProxy.Current.DeleteTreatment(SelectedTreatment.Id);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Treatments));
        }

    }
}
