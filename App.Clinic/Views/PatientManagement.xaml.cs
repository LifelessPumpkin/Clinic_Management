using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage, INotifyPropertyChanged
{
    
	public PatientManagement()
	{
		InitializeComponent();
		BindingContext = new PatientManagementViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to mainpage
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Go to patient details page
        // patientid = 0 because its a new patient
        Shell.Current.GoToAsync("//PatientDetails?patientId=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        // Go to patient details page
        // Go to exactly the patient that is selected
        var selectedPatientId = (BindingContext as PatientManagementViewModel)?
            .SelectedPatient?.Id ?? 0;
        Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        // Delete selected patient
        (BindingContext as PatientManagementViewModel)?.Delete();
    }

    private void PatientManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // Refresh the page when navigated to
        (BindingContext as PatientManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        // Refresh the page
        (BindingContext as PatientManagementViewModel)?.Refresh();
    }

}