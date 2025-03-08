using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;


namespace App.Clinic.Views;

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
		Shell.Current.GoToAsync("//Physicians");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianViewModel)?.ExecuteAdd();
    }

    private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
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
            BindingContext = new PhysicianViewModel();
        }
        
    }
}