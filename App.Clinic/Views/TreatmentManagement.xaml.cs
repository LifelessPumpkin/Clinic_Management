using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;

public partial class TreatmentManagement : ContentPage , INotifyPropertyChanged
{
	public TreatmentManagement()
	{
		InitializeComponent();
		BindingContext = new TreatmentManagementViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to main page
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked (object sender, EventArgs e)
    {
        // Go to treatment details page
        // treatmentid = 0 because its a new patient
        Shell.Current.GoToAsync("//TreatmentDetails?treatmentId=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        // Go to treatment details page
        // Go to exactly the treatment that is selected
        var selectedTreatmentId = (BindingContext as TreatmentManagementViewModel)?
            .SelectedTreatment?.Id ?? 0;
        Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        // Delete the selected treatment
        (BindingContext as TreatmentManagementViewModel)?.Delete();
    }

    private void TreatmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // Refresh when the page is navigated to
        (BindingContext as TreatmentManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        // Refresh the page
        (BindingContext as TreatmentManagementViewModel)?.Refresh();
    }
}