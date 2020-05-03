using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalUnitsLibrary;

namespace PatientAppointmentManagementSystemApp
{
    class HospitalDBManager
    {
        #region Data Fields
        private List<Department> _departments;
        private List<Patient> _patients;
        private List<Appointment> _appointments;

        #endregion

        #region Properties

        public List<Department> Departments
        {
            get { return this._departments; }
            set { this._departments = value; }
        }

        public List<Patient> Patients
        {
            get { return this._patients; }
            set { this._patients = value; }
        }

        public List<Appointment> Appointments
        {
            get { return this._appointments; }
            set { this._appointments = value; }
        }
        #endregion

        #region Constructor

        public HospitalDBManager()
        {
            //Initialize and populate the list of Departments and doctors
            _departments = new List<Department>();
            
            Department opd = new Department("Out Patient Department",101);
            opd.DoctorsList.Add(new Doctor("Meredith Grey","meredith.grey@generalhospital.com",4325185738));
            opd.DoctorsList.Add(new Doctor("Alex Karev","alex.karev@generalhospital.com",4354346794));
            _departments.Add(opd);
            Department paediatrics = new Department("Paediatrics", 102);
            paediatrics.DoctorsList.Add(new Doctor("Miranda Bailey", "miranda.bailey@generalhospital.com", 4324436788));
            paediatrics.DoctorsList.Add(new Doctor("Richard Webber", "richard.webber@generalhospital.com", 434464577494));
            _departments.Add(paediatrics);
            Department entDepartment = new Department("ENT Department", 103);
            entDepartment.DoctorsList.Add(new Doctor("Christina Yang", "christine.yang@generalhospital.com", 4387378467));
            entDepartment.DoctorsList.Add(new Doctor("Derek Shepherd", "derek.shepherd@generalhospital.com", 4354543578));
            _departments.Add(entDepartment);
            Department gynaecology = new Department("Gynaecology Department", 104);
            gynaecology.DoctorsList.Add(new Doctor("April Kepner", "april.kepner@generalhospital.com", 4325185738));
            gynaecology.DoctorsList.Add(new Doctor("Jo Wilson", "jo.wilson@generalhospital.com", 4354345674));
            _departments.Add(gynaecology);
            Department neurology = new Department("Neurology Department", 105);
            neurology.DoctorsList.Add(new Doctor("Owen Hunt", "owen.hunt@generalhospital.com", 4398341040));
            neurology.DoctorsList.Add(new Doctor("Jackson Avery", "jackson.avery@generalhospital.com", 4396057380));
            _departments.Add(neurology);
            Department oncology = new Department("Oncology Department", 106);
            oncology.DoctorsList.Add(new Doctor("Amelia Shepherd", "amelia.shepherd@generalhospital.com", 4303815963));
            oncology.DoctorsList.Add(new Doctor("Izzie Stevens", "izzie.stevens@generalhospital.com", 43936547262));
            _departments.Add(oncology);

            //Initialize the patient list
            _patients = new List<Patient>();
            _appointments = new List<Appointment>();
        }


        #endregion

        #region Methods

        public Patient GetPatient(string mrn)
        {
            foreach (var patient in Patients)
            {
                if (patient.Mrn.Equals(mrn))
                {
                    return patient;
                }
            }

            return null;
        }

        public bool ValidateAndAddAppointment(Appointment newAppt)
        {
            bool apptValid = ValidateAppointment(newAppt);
            if (apptValid)
            {
                Appointments.Add(newAppt);
                return true;
            }
            else
            {
                Console.WriteLine("Entered date/time is not valid.");
                return false;
            }
        }

        public bool ChangeAppointment(Appointment appt,DateTime oldDateTime)
        {
            bool apptValid = ValidateAppointment(appt);
            if (apptValid)
            {
                appt.EndDateTime = appt.StartDateTime.AddMinutes(30);
                return true;
            }
            else
            {
                appt.StartDateTime = oldDateTime;
                return false;
            }
        }
        
        public bool ValidateAppointment(Appointment appt)
        {
            if (appt.StartDateTime < DateTime.Now)
            {
                Console.WriteLine("Entered date and time is past. Please enter valid date and time:");
                bool valid = DateTime.TryParse(Console.ReadLine(), out DateTime newDT);
                if (valid)
                {
                    appt.StartDateTime = newDT;
                    ValidateAppointment(appt);
                }
                else
                {
                    return false;
                }
            }
            foreach (var existingAppt in Appointments.ToList())
            {
                if ((appt.ConsultingDoctor.Equals(existingAppt.ConsultingDoctor)) &&
                    (appt.StartDateTime >= existingAppt.StartDateTime) &&
                    (appt.StartDateTime < existingAppt.EndDateTime))
                {

                    Console.WriteLine("Slot full. Enter alternate date and time:");
                    bool valid = DateTime.TryParse(Console.ReadLine(), out DateTime newTime);
                    if (valid)
                    {
                        appt.StartDateTime = newTime;
                        appt.EndDateTime = newTime.AddMinutes(30);
                        ValidateAppointment(appt);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            //No appointments yet - return true
            return true;
        }

        public void ShowDepartments()
        {
            Console.WriteLine("Sl.No.   Department Name");
            Console.WriteLine("------------------------------------------");
            foreach (var department in Departments)
            {
                Console.WriteLine(Departments.IndexOf(department) +1+ ".   " + department.DeptName);
            }
        }

        public void ShowDoctors(int departmentNo)
        {
            Console.WriteLine("Sl.No.     Doctor Name");
            Console.WriteLine("------------------------------------------");
            Department dept = Departments[departmentNo];
            if (dept == null)
            {
                Console.WriteLine("Department number not valid. Exiting.");
                return;
            }
            foreach (var doctor in dept.DoctorsList)
            {
                Console.WriteLine(dept.DoctorsList.IndexOf(doctor)+1 + ".    " + doctor.Name);

            }
        }

        public void ShowPatients()
        {
            Console.WriteLine("MRN          Patient Name");
            Console.WriteLine("------------------------------------------");
            foreach (var patient in Patients)
            {
                Console.WriteLine(patient.Mrn + "     " + patient.Name);
            }
        }

        public void ShowAppointmentDashboard(DateTime date)
        {
            Console.WriteLine("Sl.No.   Appt. ID     Patient Name    Doctor Name    Appointment Date and Time");
            Console.WriteLine("------------------------------------------------------------------------");
            foreach (var appointment in Appointments)
            {
                if (appointment.StartDateTime.Date == date.Date)
                {
                    Console.WriteLine(Appointments.IndexOf(appointment)+1+ "          " 
                                      +appointment.AppointmentId + "          "
                                      +appointment.VisitingPatient.Name+"          "
                                      +appointment.ConsultingDoctor.Name+"          "
                                      +appointment.StartDateTime);
                }
            }
        }

        public void ShowAppointmentDashboard()
        {
            Console.WriteLine("Sl.No.   Appt. ID         Patient Name        Doctor Name       Appointment Date and Time");
            Console.WriteLine("------------------------------------------------------------------------------------");
            foreach (var appointment in Appointments)
            {
                Console.WriteLine(Appointments.IndexOf(appointment) + 1 + "          "
                                      + appointment.AppointmentId + "          "
                                      + appointment.VisitingPatient.Name + "          "
                                      + appointment.ConsultingDoctor.Name + "          "
                                      + appointment.StartDateTime);
                
            }
            Console.WriteLine();
        }
        public Appointment GetAppointment(string apptId)
        {
            foreach (var appointment in Appointments)
            {
                if (appointment.AppointmentId.Equals(apptId))
                {
                    return appointment;
                }
            }

            return null;
        }
        #endregion
    }
}
