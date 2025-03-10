using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
namespace App.Clinic.Views;

[QueryProperty(nameof(TreatmentId), "treatmentId")]
public partial class TreatmentView : ContentPage
{
    public TreatmentView()
    {
        InitializeComponent();

    }
    public int TreatmentId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        // Go back to Treatments page
        Shell.Current.GoToAsync("//Treatments");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Add the patient 
        (BindingContext as TreatmentViewModel)?.ExecuteAdd();
    }

    private void TreatmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        // This is all just setting binding context so its okay to leave in 
        // If the treatment exists set the binding context equal to the treatment
        if (TreatmentId > 0)
        {
            var model = TreatmentServiceProxy.Current
                .Treatments.FirstOrDefault(t => t.TreatmentId == TreatmentId);
            if (model != null)
            {
                BindingContext = new TreatmentViewModel(model);
            }
            else BindingContext = new TreatmentViewModel();
        }
        else
        {
            // The binding context is set to default
            BindingContext = new TreatmentViewModel();
        }

    }
}