using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalUnitsLibrary
{
    public class Patient:PersonBase
    {
        #region Patient Fields
        
        private string _mrn;
        #endregion

        #region Properties
        

        public string Mrn
        {
            get { return this._mrn; }

            set { this._mrn = value; }
        }

        
        #endregion

        #region Constructor

        public Patient(string name, string email, long phoneNumber): base(name, email, phoneNumber)
        {

            System.Random randomGenerator = new Random();
            //New interpolation grammar works .net fx 4.5 onwards
            string autoMrn = $"MRN{randomGenerator.Next(1, 1000)}";
            this._mrn = autoMrn;
        }

        #endregion
    }
}
