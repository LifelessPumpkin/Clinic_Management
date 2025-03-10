using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;


namespace App.Clinic.Views;

// QueryProperty so I can GoToAsync based on the query
[QueryProperty(nameof(PatientId), "patientId")]
//i can have multiple query properties if i need, like for appointment

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}
    public int PatientId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to Patients Page
		Shell.Current.GoToAsync("//Patients");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Add the patient
        (BindingContext as PatientViewModel)?.ExecuteAdd();
    }

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // This is all just setting binding context so its okay to leave in 
        // If the patient exists set the binding context equal to the patient
        if(PatientId > 0)
        {
            var model = PatientServiceProxy.Current
                .Patients.FirstOrDefault(p => p.Id == PatientId);
            if(model != null)
            {
            BindingContext = new PatientViewModel(model);
            }
            else BindingContext = new PatientViewModel();
        } else
        {
            // The binding context is default
            BindingContext = new PatientViewModel();
        }
        
    }
}