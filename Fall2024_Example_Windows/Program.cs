using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool isContinue = true;

            do
            {
                Console.WriteLine("A. Add a patient");
                Console.WriteLine("B. Add a physician");
                Console.WriteLine("C. Add an appointment");
                Console.WriteLine("D. List medical notes");
                Console.WriteLine("E. Delete a patient");
                Console.WriteLine("Q. Quit");
                Console.WriteLine("Select an option - ");

                // var today = new DateTime();
                // today = DateTime.Today;
                // Console.WriteLine(today);
                // Console.WriteLine(today.Hour);

                
                string input = Console.ReadLine() ?? string.Empty;


                if (char.TryParse(input, out char choice))
                {
                    switch (choice)
                    {
                        
                        case 'a':
                        case 'A':
                            //Enter patient information
                            Console.WriteLine("Please enter patient name - ");
                            var name = Console.ReadLine();

                            Console.WriteLine("\nPlease enter patient birthday(MM/DD/YYYY)");
                            var bday = new DateOnly();
                            int month,day=0,year=0;
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid month between 1 and 12 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out month) && month >= 1 && month <= 12)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid month between 1 and 12 - ");
                            }
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid day between 1 and 31 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out day) && day >= 1 && day <= 31)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid day between 1 and 31 - ");
                            }
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid year between 1900 and 2100 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out year) && year >= 1900 && year <= 2100)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid year between 1900 and 2100 - ");
                            }

                            bday = bday.AddMonths(month-1);
                            bday = bday.AddYears(year-1);
                            bday = bday.AddDays(day -1);

                            Console.WriteLine("\nPlease enter patient address - ");
                            var address = Console.ReadLine();

                            Console.WriteLine("\nPlease enter patient race - ");
                            var race = Console.ReadLine();

                            Console.WriteLine("\nPlease enter patient gender - ");
                            var gender = Console.ReadLine();
                            
                            Console.WriteLine("\nPlease enter patient SSN(123-45-6789) - ");
                            var ssn = Console.ReadLine();

                            
                            //create the new patient
                            var newPatient = new Patient 
                            {
                                Birthday = bday,
                                Name = name ?? string.Empty, 
                                Address = address ?? string.Empty, 
                                Race = race ?? string.Empty, 
                                Gender = gender ?? string.Empty, 
                                SSN = ssn ?? string.Empty
                                
                            };

                            //add patient into list
                            PatientServiceProxy.Current.AddOrUpdatePatient(newPatient);
                            
                            //Check for diagnoses
                            bool PatientCont=true;
                            Console.WriteLine("Does the patient have any Pre-existing diagnoses?(Y/N) - ");
                            string diagnoses = Console.ReadLine() ?? string.Empty;
                            while(PatientCont)
                            {
                            if(diagnoses == "Y"||diagnoses=="y")
                            {
                                //add diagnosis
                                PatientServiceProxy.Current.AddDiagnosis(newPatient.Id);

                                //check if there is another diagnosis
                                Console.WriteLine("Does the patient have another diagnosis?(Y/N) - ");
                                var choose = Console.ReadLine();
                                if(choose=="Y"||choose=="y") PatientCont=true;
                                else PatientCont=false;

                                
                            }
                            else PatientCont=false;
                            }
                            //print out Diagnoses
                            PatientServiceProxy.Current.PrintDiagnoses(newPatient.Id);

                            //check for prescriptions
                            PatientCont=true;
                            Console.WriteLine("Does the patient have any prescriptions?(Y/N) - ");
                            string prescriptions = Console.ReadLine() ?? string.Empty;
                            while(PatientCont)
                            {
                            if(prescriptions == "Y"||prescriptions=="y")
                            {
                                //add prescription
                                PatientServiceProxy.Current.AddPrescription(newPatient.Id);

                                //check if there is another prescription
                                Console.WriteLine("Does the patient have another prescription?(Y/N) - ");
                                var choose = Console.ReadLine();
                                if(choose=="Y"||choose=="y") PatientCont=true;
                                else PatientCont=false;

                                
                            }
                            else PatientCont=false;
                            }
                            //print out prescriptions
                            PatientServiceProxy.Current.PrintPrescription(newPatient.Id);
                            
                            
                            break;
                        
                        case 'b':
                        case 'B':
                            Console.WriteLine("Please enter physician name - ");
                            var physname = Console.ReadLine() ?? string.Empty;
                            Console.WriteLine("\nPlease enter physician graduation date(MM/DD/YYYY)");
                            var graddate = new DateOnly();
                            int physmonth,physday=0,physyear=0;
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid month between 1 and 12 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out physmonth) && physmonth >= 1 && physmonth <= 12)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid month between 1 and 12 - ");
                            }
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid day between 1 and 31 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out physday) && physday >= 1 && physday <= 31)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid day between 1 and 31 - ");
                            }
                            while (true)
                            {
                                Console.WriteLine("Please enter a valid year between 1900 and 2100 - ");
                                var monthInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(monthInput, out physyear) && physyear >= 1900 && physyear <= 2100)
                                    break;
                                else
                                    Console.WriteLine("Please enter a valid year between 1900 and 2100 - ");
                            }

                            graddate = graddate.AddMonths(physmonth-1);
                            graddate = graddate.AddYears(physyear-1);
                            graddate = graddate.AddDays(physday -1);

                            Console.WriteLine("Please enter physician license number - ");
                            var physlicense = Console.ReadLine() ?? string.Empty;

                            var newphysician = new Physician{GradDate = graddate, LName = physname, LicenseNumber = physlicense};

                            PatientServiceProxy.Current.AddOrUpdatePhysician(newphysician);
                            
                            bool Physcont=true;
                            Console.WriteLine("Does the physician have any specializations?(Y/N) - ");
                            string specializations = Console.ReadLine() ?? string.Empty;
                            while(Physcont)
                            {
                            if(specializations == "Y"||specializations=="y")
                            {
                                //add diagnosis
                                PatientServiceProxy.Current.AddSpecialization(newphysician.EmployeeId);

                                //check if there is another diagnosis
                                Console.WriteLine("Does the patient have another specialization?(Y/N) - ");
                                var choose = Console.ReadLine();
                                if(choose=="Y"||choose=="y") Physcont=true;
                                else Physcont=false;

                                
                            }
                            else Physcont=false;
                            }
                            //print out Specializations
                            PatientServiceProxy.Current.PrintSpecializations(newphysician.EmployeeId);

                    
                            break;
                        
                        case 'c':
                        case 'C':
                            //enter in patient id
                            Console.WriteLine("Please enter the patient ID to set up appointment - ");
                            Console.WriteLine("Id - ");
                            var patid = Console.ReadLine()??string.Empty;
                            int number = 0;
                            number = int.Parse(patid);

                            //enter in physician id
                            Console.WriteLine("Please enter the physician ID - ");
                            Console.WriteLine("Id - ");
                            var physid = Console.ReadLine()??string.Empty;
                            int tempnumb = 0;
                            tempnumb = int.Parse(physid);

                            Console.WriteLine("\nPlease enter the date for your appointment(MM/DD/YYYY) - ");
                            var appointmentdate = new DateTime();
                            appointmentdate = DateTime.Today;
                            var newdate = Console.ReadLine() ?? string.Empty;
                            appointmentdate = DateTime.Parse(newdate);

                            Console.WriteLine("Please enter the time for your 1 hour appointment between 8am to 5pm(8-5) - ");
                            var hours = Console.ReadLine() ?? string.Empty;
                            int anothernumber = int.Parse(hours);

                            //checks to make sure day falls between monday-friday
                            if(appointmentdate.DayOfWeek != DayOfWeek.Sunday && appointmentdate.DayOfWeek != DayOfWeek.Saturday)
                            {
                                //checks to make sure falls within 8am-5pm
                                if(anothernumber >= 8 && anothernumber < 13)
                                {
                                    // anothernumber-=1;
                                    TimeSpan timespan = new(anothernumber,0,0);
                                    appointmentdate = appointmentdate.Add(timespan);
                                    // PatientServiceProxy.Current.CreateAppointment(number, tempnumb, appointmentdate);
                                    // Console.WriteLine(anothernumber);
                                }
                                else if(anothernumber < 6 && anothernumber > 0) 
                                {
                                    anothernumber+=12;
                                    TimeSpan timespan = new(anothernumber,0,0);
                                    appointmentdate = appointmentdate.Add(timespan);
                                    // Console.WriteLine(anothernumber);
                                    // PatientServiceProxy.Current.CreateAppointment(number, tempnumb, appointmentdate);
                                }
                                else 
                                {
                                    Console.WriteLine("Invalid must be between 8am to 5pm");
                                }
                            }
                            else Console.WriteLine("Invalid must be between monday-friday - ");                            
                            
                            
                            break;
                        
                        case 'd':
                        case 'D':
                            //print medical notes
                            Console.WriteLine("Please enter the patient ID to see medical notes - ");
                            Console.WriteLine("Id - ");
                            var pid = Console.ReadLine()??string.Empty;
                            int numb = 0;
                            numb = int.Parse(pid);
                            PatientServiceProxy.Current.PrintDiagnoses(numb);
                            PatientServiceProxy.Current.PrintPrescription(numb);
                            break;

                        case 'e':
                        case 'E':
                            Console.WriteLine("havent deleted yet");
                            Console.WriteLine(PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == 1));
                            PatientServiceProxy.Current.DeletePatient(1);
                            Console.WriteLine(PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == 1));
                            break;

                        case 'r':
                        case 'R':

                            break;
                        
                        case 'q':
                        case 'Q':
                            isContinue = false;
                            break;

                        case 't':
                        case 'T':
                            
                            break;
                        default:
                            Console.WriteLine("That is an invalid choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"{choice} is not a char");
                }
            }while (isContinue);

        }
    }
    
}