namespace App.Clinic
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void PatientsClicked(object sender, EventArgs e)
        {
            // Go to patients page
            Shell.Current.GoToAsync("//Patients");
        }

        private void PhysiciansClicked(object sender, EventArgs e)
        {
            // Go to physicians page
            Shell.Current.GoToAsync("//Physicians");
        }

        private void AppointmentsClicked(object sender, EventArgs e)
        {
            // Go to appointments page
            Shell.Current.GoToAsync("//Appointments");
        }

        private void TreatmentsClicked(object sender, EventArgs e)
        {
            // Go to treatments page
            Shell.Current.GoToAsync("//Treatments");
        }
    }

}
