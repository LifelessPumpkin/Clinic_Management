using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;


namespace App.Clinic.Views;

// QueryProperty so I can GoToAsync based on the query
[QueryProperty(nameof(PhysicianEID), "physicianEID")]
public partial class PhysicianView : ContentPage
{
	public PhysicianView()
	{
		InitializeComponent();
	}
    public int PhysicianEID { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to Physicians page
		Shell.Current.GoToAsync("//Physicians");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Add the physican
        (BindingContext as PhysicianViewModel)?.ExecuteAdd();
    }

    private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // This is all just setting binding context so its okay to leave in 
        // If the physician exists set the binding context equal to the physician
        if(PhysicianEID > 0)
        {
            var model = PhysicianServiceProxy.Current
                .Physicians.FirstOrDefault(p => p.EmployeeId == PhysicianEID);
            if(model != null)
            {
            BindingContext = new PhysicianViewModel(model);
            }
            else BindingContext = new PhysicianViewModel();
        } else
        {
            // The binding context is set to default
            BindingContext = new PhysicianViewModel();
        }
        
    }
}