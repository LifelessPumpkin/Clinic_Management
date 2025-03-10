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
    public class PhysicianManagementViewModel: INotifyPropertyChanged
    {
        public PhysicianManagementViewModel()
        {
            // Needs to be an observable collection
            Physicians = new ObservableCollection<PhysicianViewModel>();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        // Enables properties to be refreshed
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Query {get;set;}

        public PhysicianViewModel? SelectedPhysician { get; set; }
        public ObservableCollection<PhysicianViewModel> Physicians
        {
            get
            {
                return new ObservableCollection<PhysicianViewModel>
                (
                    // Calls physican service proxy to get all the physicians
                    PhysicianServiceProxy
                    .Current
                    .Physicians
                    .Where(p=>p != null)
                    .Where(p => p.LName.ToUpper().Contains(Query?.ToUpper()?? string.Empty))
                    .Select(p => new PhysicianViewModel(p))
                );
            }
            set{}
        }

        public void Delete()
        {
            if(SelectedPhysician == null)
            {
                return;
            }
            // Call service proxy to delete physician
            PhysicianServiceProxy.Current.DeletePhysician(SelectedPhysician.EID);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Physicians));
        }
    }
}
