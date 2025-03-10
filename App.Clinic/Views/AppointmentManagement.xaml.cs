using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;

public partial class AppointmentManagement : ContentPage, INotifyPropertyChanged
{
	public AppointmentManagement()
	{
		InitializeComponent();
		BindingContext = new AppointmentManagementViewModel();
	}

	private void AddClicked(object sender, EventArgs e)
    {
        // Go to patient details page
        // New patient so appointmentid=0
        Shell.Current.GoToAsync("//AppointmentDetails?appointmentid=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        // Go to patient details page
        // Go to exactly the patient that is selected
        var selectedappointmentid = (BindingContext as AppointmentManagementViewModel)?
            .SelectedAppointment?.AppId ?? 0;
        Shell.Current.GoToAsync($"//AppointmentDetails?appointmentid={selectedappointmentid}");
    }
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AppointmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Delete();
    }

    private void AppointmentDetailsClicked(object sender, EventArgs e)
    {
        var selectedappointmentid = (BindingContext as AppointmentManagementViewModel)?
            .SelectedAppointment?.AppId ?? 0;
        Shell.Current.GoToAsync($"//AppointmentDescriptions?appointmentid={selectedappointmentid}");
    }
}