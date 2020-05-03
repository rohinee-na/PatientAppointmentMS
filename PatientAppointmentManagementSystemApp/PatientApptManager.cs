using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationLibrary;
using HospitalUnitsLibrary;

namespace PatientAppointmentManagementSystemApp
{
    class PatientApptManager
    {
        private HospitalDBManager _hospitalDB = null;
        private SMSDeliveryManager _smsDeliveryManager;
        private EmailDeliveryManager _emailDeliveryManager;

        public event EmailDelegate ApptCreatedEmail = null;
        public event SMSDelegate ApptCreatedSMS = null;

        #region Constructor

        public PatientApptManager()
        {
            _hospitalDB = new HospitalDBManager();
            _smsDeliveryManager = new SMSDeliveryManager();
            _emailDeliveryManager = new EmailDeliveryManager();
            SMSDelegate smsDelegate = new SMSDelegate(_smsDeliveryManager.SendNotification);
            EmailDelegate emailDelegate = new EmailDelegate(_emailDeliveryManager.SendNotification);
            ApptCreatedSMS += smsDelegate;
            ApptCreatedEmail += emailDelegate;
        }

        #endregion
        #region Methods

        public void AddNewPatientData()
        {
            Console.WriteLine("Enter patient details:");
            Console.Write("Patient Name: ");
            string patientName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone Number: ");
            long.TryParse(Console.ReadLine(), out long phoneNumber);
            Patient newPatient = new Patient(patientName,email,phoneNumber);
            _hospitalDB.Patients.Add(newPatient);
            Console.WriteLine("Patient added successfully! MRN is "+newPatient.Mrn);

        }
        public void MakeNewAppointment()
        {
            Console.WriteLine("Enter patient MRN or enter S to show all patients to show from: ");
            string mrn = Console.ReadLine();
            if (mrn.Equals("s")||mrn.Equals("S"))
            {
                _hospitalDB.ShowPatients();
                Console.WriteLine("Enter MRN: ");
                mrn = Console.ReadLine();
            }

            Patient visitingPatient = _hospitalDB.GetPatient(mrn);
            if (visitingPatient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }
            Console.WriteLine("Choose Department : ");
            _hospitalDB.ShowDepartments();
            Console.Write("Enter Sl. No.: ");
            bool valid = Int32.TryParse(Console.ReadLine(),out int deptIndex);
            Console.WriteLine("Choose Doctor:");
            if (valid)
            {
               _hospitalDB.ShowDoctors(deptIndex-1);
            }
            Console.Write("Enter Sl.No.: ");
            valid = Int32.TryParse(Console.ReadLine(),out int drIndex);
            Doctor consultingDoctor = null;
            if (valid)
            {
                consultingDoctor = _hospitalDB.Departments[deptIndex-1].DoctorsList[drIndex-1];
            }

            if (consultingDoctor != null)
            {
                Console.WriteLine("Enter Date and Time of appointment as mm/dd/yyyy hh:mm");
                valid = DateTime.TryParse(Console.ReadLine(), out DateTime apptTime);
                if (valid)
                {
                    Appointment appt = new Appointment(apptTime,consultingDoctor,visitingPatient);
                    bool apptAdded = _hospitalDB.ValidateAndAddAppointment(appt);
                    if (apptAdded)
                    {

                        Console.WriteLine("Appointment added successfully!");
                        Console.WriteLine();
                        string docMessage = "Dear "+consultingDoctor.Name+", You have an appointment with patient " + visitingPatient.Name + " on " +
                                            appt.StartDateTime+". Appointment ID: " +appt.AppointmentId;
                        ApptCreatedSMS.Invoke(consultingDoctor.PhoneNumber.ToString(),
                            docMessage);
                        ApptCreatedEmail.Invoke(consultingDoctor.Email,docMessage);
                        Console.WriteLine();
                        string patientMessage = "Dear "+visitingPatient.Name +", You have an appointment with Doctor " + consultingDoctor.Name + " on " +
                                                appt.StartDateTime+". Appointment ID:"+ appt.AppointmentId;
                        ApptCreatedSMS.Invoke(visitingPatient.PhoneNumber.ToString(),patientMessage);
                        ApptCreatedEmail.Invoke(visitingPatient.Email,patientMessage);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Appointment not created. Please try again later.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Option. Doctor not found. Exiting.");
                return;
            }



        }

        public void ManageAppointment()
        {
            Console.WriteLine("Enter appointment ID: ");
            string apptId = Console.ReadLine();
            Appointment appointment = _hospitalDB.GetAppointment(apptId);
            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                Console.WriteLine();
                return;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Choose option to proceed:");
                Console.WriteLine("1. Change Date/Time");
                Console.WriteLine("2. Cancel Appointment");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Enter new date and time as mm/dd/yyyy hh:mm");
                        bool valid = DateTime.TryParse(Console.ReadLine(), out DateTime newDateTime);
                        if (valid)
                        {
                            DateTime oldDateTime = appointment.StartDateTime;
                            appointment.StartDateTime = newDateTime;
                            bool apptChanged = _hospitalDB.ChangeAppointment(appointment,oldDateTime);
                            if (apptChanged)
                            {
                                Console.WriteLine("Appointment updated.");
                                string docMessage = "Dear " + appointment.ConsultingDoctor.Name +
                                                    ", Your appointment with patient " +
                                                    appointment.VisitingPatient.Name + " has been changed to " +
                                                    appointment.StartDateTime + ". Appointment ID: " +
                                                    appointment.AppointmentId;
                                ApptCreatedSMS.Invoke(appointment.ConsultingDoctor.PhoneNumber.ToString(),
                                    docMessage);
                                ApptCreatedEmail.Invoke(appointment.ConsultingDoctor.Email, docMessage);

                                Console.WriteLine();
                                string patientMessage =
                                    "Dear " + appointment.VisitingPatient.Name + ", Your appointment with Doctor " +
                                    appointment.ConsultingDoctor.Name + " has been changed to " +
                                    appointment.StartDateTime + ". Appointment ID:" + appointment.AppointmentId;
                                ApptCreatedSMS.Invoke(appointment.VisitingPatient.PhoneNumber.ToString(),
                                    patientMessage);
                                ApptCreatedEmail.Invoke(appointment.VisitingPatient.Email, patientMessage);

                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Appointment not changed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date entered. Appointment not changed.");
                        }
                        break;
                    case "2":
                        string message = "Your appointment with ID " + appointment.AppointmentId +
                                         "has been cancelled.";
                        _hospitalDB.Appointments.RemoveAt(_hospitalDB.Appointments.IndexOf(appointment));
                        ApptCreatedSMS.Invoke(appointment.ConsultingDoctor.PhoneNumber.ToString(),message);
                        ApptCreatedEmail.Invoke(appointment.ConsultingDoctor.Email,message);
                        ApptCreatedSMS.Invoke(appointment.VisitingPatient.PhoneNumber.ToString(),message);
                        ApptCreatedEmail.Invoke(appointment.VisitingPatient.Email,message);
                        break;
                    default:
                        break;

                }
            }
        }

        public void ShowAppointmentDashboard()
        {
            Console.WriteLine("Enter date as mm/dd/yyyy or enter A to show all appointments:");
            string input = Console.ReadLine();
            bool valid = DateTime.TryParse(input, out DateTime date);
            if (valid)
            {
                _hospitalDB.ShowAppointmentDashboard(date);
            }
            else if (input.Equals("A") || input.Equals("a"))
            {
                _hospitalDB.ShowAppointmentDashboard();
            }
            else
            {
                Console.WriteLine("Invalid option/date entered.");
            }

        }
        
        #endregion
    }
}
