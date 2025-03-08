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
    public class PatientManagementViewModel: INotifyPropertyChanged
    {
        public PatientManagementViewModel()
        {
            Patients = new ObservableCollection<PatientViewModel>();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Query {get;set;}
        public PatientViewModel? SelectedPatient { get; set; }
        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                var retval = new ObservableCollection<PatientViewModel>(PatientServiceProxy
                    .Current
                    .Patients
                    .Where(p => p != null)
                    .Where(p=>p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Select(p=>new PatientViewModel(p)));
                //var som = new ob
                return retval;
            }
            set{}
        }

        public void Delete()
        {
            if(SelectedPatient == null)
            {
                return;
            }
            PatientServiceProxy.Current.DeletePatient(SelectedPatient.Id);
            Refresh();
        }
        

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }
    }
}
