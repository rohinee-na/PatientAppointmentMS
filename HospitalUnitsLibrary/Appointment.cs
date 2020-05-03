using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalUnitsLibrary
{
    public class Appointment
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private Doctor _consultingDoctor;
        private Patient _visitingPatient;
        private string _appointmentId;

        #region Properties

        public DateTime StartDateTime
        {
            get { return this._startTime; }
            set { this._startTime = value; }
        }

        public DateTime EndDateTime
        {
            get { return this._endTime; }
            set { this._endTime = value; }
        }
        public Doctor ConsultingDoctor
        {
            get { return this._consultingDoctor; }
            set { this._consultingDoctor = value; }

        }

        public Patient VisitingPatient
        {
            get { return this._visitingPatient; }
            set { this._visitingPatient = value; }
        }

        public string AppointmentId
        {
            get { return this._appointmentId; }
            set { this._appointmentId = value; }
        }

        #endregion

        #region Constructor

        public Appointment(DateTime startTime, Doctor doctor, Patient patient)
        {
            _startTime = startTime;
            _endTime = startTime.AddMinutes(30);
            _consultingDoctor = doctor;
            _visitingPatient = patient;
            System.Random randomGenerator = new Random();
            string autoApptId = $"APP{randomGenerator.Next(1, 1000)}";
            _appointmentId = autoApptId;
        }

        #endregion
    }
}
