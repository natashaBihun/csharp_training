using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string _firstName;
        private string _lastName = "";
        private string _formattedName;
        private string _title = "";
        private string _company = "";
        private string _address = "";
        private string _homePhone = "";
        private string _mobilePhone = "";
        private string _email = "";
        private string _bDay = "";
        private string _bMonth = "-";
        private string _bYear = "";
        private string _nameOfGroup = null;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public string FormattedName
        {
            get
            {
                return _firstName + " " + _lastName;
            }
            set
            {
                _formattedName = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                _homePhone = value;
            }
        }
        public string MobilePhone
        {
            get
            {
                return _mobilePhone;
            }
            set
            {
                _mobilePhone = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public string BDay
        {
            get
            {
                return _bDay;
            }
            set
            {
                _bDay = value;
            }
        }
        public string BMonth
        {
            get
            {
                return _bMonth;
            }
            set
            {
                _bMonth = value;
            }
        }
        public string BYear
        {
            get
            {
                return _bYear;
            }
            set
            {
                _bYear = value;
            }
        }
        public string NameOfGroup
        {
            get
            {
                return _nameOfGroup;
            }
            set
            {
                _nameOfGroup = value;
            }
        }
        public string Id { get; set; }
        public string WorkPhone { get; set; }
        public string SecondEmail { get; set; }
        public string ThirdEmail { get; set; }

        public ContactData() { }

        public ContactData(string firstName)
        {
            _firstName = firstName;
        }
        public ContactData(
            string firstName, string lastName, string title, string company, string address, string homePhone,
            string mobilePhone, string email, string bDay, string bMonth, string bYear, string nameOfGroup)
        {

            _firstName = firstName;
            _lastName = lastName;
            _formattedName = _firstName + _lastName;
            _title = title;
            _company = company;
            _address = address;
            _homePhone = homePhone;
            _mobilePhone = mobilePhone;
            _email = email;
            _bDay = bDay;
            _bMonth = bMonth;
            _bYear = bYear;
            _nameOfGroup = nameOfGroup;
        }

        public bool Equals(ContactData otherContact)
        {
            if (Object.ReferenceEquals(otherContact, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, otherContact))
            {
                return true;
            }
            return FormattedName == otherContact.FormattedName;
        }
        public override int GetHashCode()
        {
            return FormattedName.GetHashCode();
        }
        public override string ToString()
        {
            return FormattedName;
        }
        public int CompareTo(ContactData otherContact)
        {
            if (Object.ReferenceEquals(otherContact, null))
            {
                return 1;
            }
            return otherContact.FormattedName.CompareTo(otherContact.FormattedName);
        }
    }
}
