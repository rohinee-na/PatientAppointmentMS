using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalUnitsLibrary
{
    public class Department
    {
        #region Department Details
        private string _deptName;
        private int _deptNumber;
        private List<Doctor> _doctorsList;
        #endregion

        #region Properties

        public string DeptName
        {
            get { return this._deptName; }
            set { this._deptName = value; }
        }

        public int DeptNumber
        {
            get { return this._deptNumber; }
            set { this._deptNumber = value; }
        }
        public List<Doctor> DoctorsList
        {
            get { return this._doctorsList; }
            set { this._doctorsList = value; }
        }

        #endregion

        #region Constructor

        public Department(string deptName,int deptNumber)
        {
            _doctorsList = new List<Doctor>();
            this._deptName = deptName;
            this._deptNumber = deptNumber;
        }

        #endregion
    }
}
