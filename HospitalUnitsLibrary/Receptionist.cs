using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalUnitsLibrary
{
    public class Receptionist
    {
        #region Receptionist Credentials
        private string _username;
        private string _password;
        #endregion

        #region Properties

        public string UserName
        {
            get { return this._username; }
            set { this._username = value; }
        }

        public string PassWord
        {
            set { this._password = value; }
        }
#endregion
        public bool LogIn()
        {
            Console.WriteLine("Enter credentials:");
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Password:");
            string password = Console.ReadLine();
            if (username.Equals(UserName) && password.Equals(this._password))
            {
                Console.WriteLine("Login successful");
                return true;
            }
            else
            {
                return false;
            }

        }

        public void ManageProfile()
        {
            Console.WriteLine("Enter new password:");
            string newPw = Console.ReadLine();
            PassWord = newPw;
            Console.WriteLine("Password successfully changed!");
        }

        
        #region Constructor

        public Receptionist()
        {
            UserName = "receptionist";
            PassWord = "pass123";
        }

        #endregion
    }
}
