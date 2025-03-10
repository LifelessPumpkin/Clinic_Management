using App.Clinic.ViewModels;
using Library.Clinic.Services;

namespace App.Clinic.Views;

[QueryProperty(nameof(appointmentid), "appointmentid")]
public partial class AppointmentDescription : ContentPage
{
    public AppointmentDescription()
    {
        InitializeComponent();
        BindingContext = new AppointmentViewModel();
    }

    public int appointmentid { get; set; }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.ExecuteAdd();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to Appointment Page
        Shell.Current.GoToAsync("//Appointments");
    }

    private void AppointmentDescription_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (appointmentid > 0)
        {
            var model = AppointmentServiceProxy.Current
                .Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
            if (model != null)
            {
                BindingContext = new AppointmentViewModel(model);
            }
            else BindingContext = new AppointmentViewModel();
        }
        else
        {
            BindingContext = new AppointmentViewModel();
        }
    }

    //private void RefreshClicked(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //{
    //    (BindingContext as AppointmentViewModel)?.Refresh();
    //}
}