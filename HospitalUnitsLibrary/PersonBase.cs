namespace HospitalUnitsLibrary
{
    public class PersonBase
    {
        private string _name;
        private string _email;
        private long _phoneNumber;

        public PersonBase(string name,string email, long phoneNumber)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string Email
        {
            get { return this._email; }

            set { this._email = value; }
        }

        public long PhoneNumber
        {
            get { return this._phoneNumber; }

            set { this._phoneNumber = value; }
        }
    }
}