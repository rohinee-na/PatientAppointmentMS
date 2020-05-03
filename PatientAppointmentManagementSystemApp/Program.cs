using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalUnitsLibrary;

namespace PatientAppointmentManagementSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Receptionist receptionist = new Receptionist();
            bool success = false;
            while (!success)
            {
                success = receptionist.LogIn();
            }
            PatientApptManager patientApptManager = new PatientApptManager();

            bool endApp = false;
            while (!endApp)
            {
                DisplayOptions();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        patientApptManager.AddNewPatientData();
                        break;
                    case "2":
                        patientApptManager.MakeNewAppointment();
                        break;
                    case "3":
                        patientApptManager.ManageAppointment();
                        break;
                    case "4":
                        patientApptManager.ShowAppointmentDashboard();
                        break;
                    case "5":
                        receptionist.ManageProfile();
                        break;
                    case "6":
                        endApp = true;
                        break;
                    default:
                        break;
                }
            }
            
        }

        static void DisplayOptions()
        {
            Console.WriteLine("Choose an option to proceed:");
            Console.WriteLine("1. Add New Patient");
            Console.WriteLine("2. Make an Appointment");
            Console.WriteLine("3. Manage Appointment");
            Console.WriteLine("4. Show Appointment Dashboard");
            Console.WriteLine("5. Manage Profile");
            Console.WriteLine("6. Logout");
        }
    }
}
