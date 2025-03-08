using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;
[QueryProperty(nameof(appointmentid), "appointmentid")]
public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
		InitializeComponent();
        BindingContext = new AppointmentViewModel();
	}

  public int appointmentid{get;set;}

  private void AddClicked(object sender, EventArgs e)
  {
    (BindingContext as AppointmentViewModel)?.ExecuteAdd();
  }

  private void CancelClicked(object sender, EventArgs e)
  {
  Shell.Current.GoToAsync("//Appointments");
  }

  private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
  {
    if(appointmentid > 0)
      {
          var model = AppointmentServiceProxy.Current
              .Appointments.FirstOrDefault(p => p.AppointmentId == appointmentid);
          if(model != null)
          {
          BindingContext = new AppointmentViewModel(model);
          }
          else BindingContext = new AppointmentViewModel();
      } else
      {
          BindingContext = new AppointmentViewModel();
      }
  }

}