using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;

public partial class PhysicianManagement : ContentPage, INotifyPropertyChanged
{
    
	public PhysicianManagement()
	{
		InitializeComponent();
		BindingContext = new PhysicianManagementViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to MainPage
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Go to physician details page
        // physicianeid = 0 because its a new physician
        Shell.Current.GoToAsync("//PhysicianDetails?physicianEID=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        // Go to physician details page
        // Go to exactly the physician that is selected
        var selectedPhysicianEID = (BindingContext as PhysicianManagementViewModel)?
            .SelectedPhysician?.EID ?? 0;
        Shell.Current.GoToAsync($"//PhysicianDetails?physicianEID={selectedPhysicianEID}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        // Delete selected physician
        (BindingContext as PhysicianManagementViewModel)?.Delete();
    }

    private void PhysicianManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // Refresh when page is navigated to 
        (BindingContext as PhysicianManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        // Refresh page
        (BindingContext as PhysicianManagementViewModel)?.Refresh();
    }


}