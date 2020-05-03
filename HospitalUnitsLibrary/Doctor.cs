using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalUnitsLibrary
{
    public class Doctor : PersonBase
    {
        
        #region Constructor

        public Doctor(string name,string email, long phoneNumber) : base(name, email, phoneNumber)
        {
        }

        #endregion
    }
}
