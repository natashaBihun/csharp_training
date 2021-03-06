﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
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
        private string _allPhones;
        private string _email = "";
        private string _allEmails;
        private string _bDay = "";
        private string _bMonth = "-";
        private string _bYear = "";
        private string _nameOfGroup = null;
        private string _allData = "";

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstname")]
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

        [Column(Name = "lastname")]
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

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

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

        [Column(Name = "nickname")]
        public string NickName { get; set; }

        [Column(Name = "company")]
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

        [Column(Name = "title")]
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

        [Column(Name = "address")]
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

        [Column(Name = "home")]
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

        [Column(Name = "mobile")]
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

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        public string AllPhones
        {
            get
            {
                if (_allPhones != null)
                {
                    return _allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(SecondaryHomePhone)).Trim();
                }
            }
            set
            {
                _allPhones = value;
            }
        }

        [Column(Name = "email")]
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

        [Column(Name = "email2")]
        public string SecondEmail { get; set; }

        [Column(Name = "email3")]
        public string ThirdEmail { get; set; }

        public string AllEmails
        {
            get
            {
                if (_allEmails != null)
                {
                    return _allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(SecondEmail) + CleanUpEmail(ThirdEmail)).Trim();
                }
            }
            set
            {
                _allEmails = value;
            }
        }

        [Column(Name = "homepage")]
        public string HomePage { get; set; }

        [Column(Name = "bday")]
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

        [Column(Name = "bmonth")]
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

        [Column(Name = "byear")]
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

        [Column(Name = "aday")]
        public string ADay { get; set; }

        [Column(Name = "amonth")]
        public string AMonth { get; set; }

        [Column(Name = "ayear")]
        public string AYear { get; set; }

        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }

        [Column(Name = "phone2")]
        public string SecondaryHomePhone { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

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
        public string AllData { get {
                if (_allData != null)
                {
                    return _allData;
                }
                else
                {
                    return (FormatAllData()).Trim();
                }
            } set {
                _allData = value;
            }
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "") {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }
        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }
        private string FormatAllData()
        {
            string result = "";

            if (IsFieldNotEmpty(FirstName)) result += FirstName;
            if (IsFieldNotEmpty(MiddleName)) result += " " + MiddleName;
            if (IsFieldNotEmpty(LastName)) result += " " + LastName;
            if (IsFieldNotEmpty(result)) result += "\r\n";
            if (IsFieldNotEmpty(NickName)) result += NickName + "\r\n";
            if (IsFieldNotEmpty(Title)) result += Title + "\r\n";
            if (IsFieldNotEmpty(Company)) result += Company + "\r\n";
            if (IsFieldNotEmpty(Address)) result += Address + "\r\n";
            if (IsFieldNotEmpty(HomePhone)) result += "H: " + HomePhone + "\r\n";
            if (IsFieldNotEmpty(MobilePhone)) result += "M: " + MobilePhone + "\r\n";
            if (IsFieldNotEmpty(WorkPhone)) result += "W: " + WorkPhone + "\r\n";
            if (IsFieldNotEmpty(Fax)) result += "F: " + Fax + "\r\n";
            if (IsFieldNotEmpty(AllEmails)) result += AllEmails + "\r\n";
            if (IsFieldNotEmpty(HomePage)) result += "Homepage:\r\n" + HomePage + "\r\n";
            if (FormattedDate(BDay, BMonth, BYear).Length > 0)
                result += "Birthday" + FormattedDate(BDay, BMonth, BYear) + "\r\n";
            if (FormattedDate(ADay, AMonth, AYear).Length > 0)
                result += "Anniversary" + FormattedDate(ADay, AMonth, AYear) + "\r\n";
            if (IsFieldNotEmpty(SecondaryAddress)) result += SecondaryAddress + "\r\n";
            if (IsFieldNotEmpty(SecondaryHomePhone)) result += "P: " + SecondaryHomePhone + "\r\n";
            if (IsFieldNotEmpty(Notes)) result += Notes;

            return result;
        }
        public bool IsFieldNotEmpty(string field)
        {
            return field != null && field != "" ? true : false;
        }
        public string FormattedDate(string day, string month, string year)
        {
            if (Int32.Parse(day) == 0) day = "";
            if (month == "-") month = "";

            string result = "";
            int years = 0;

            if (IsFieldNotEmpty(year))
            {
                years = DateTime.Now.Year - Int32.Parse(year);
                DateTime date = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.InvariantCulture);
                int monthInNumb = date.Month;
                if (IsFieldNotEmpty(month) && monthInNumb == DateTime.Now.Month)
                {
                    if (IsFieldNotEmpty(day) && Int32.Parse(day) > DateTime.Now.Day)
                    {
                        years--;
                    }
                }
                if (IsFieldNotEmpty(month) && monthInNumb > DateTime.Now.Month)
                {
                    years--;
                }
                result += " " + day + "." + " " + month + " " + year + " (" + years + ")";
            }
            else
            {
                if (IsFieldNotEmpty(day)) result += " " + day + ".";
                if (IsFieldNotEmpty(month)) result += " " + month;
            }

            return result;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        .Where(t => t.Deprecated == "0000-00-00 00:00:00")
                        select c).ToList();
            }
        }
        public List<GroupData> GetGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups
                        from gcr in db.GCR
                        .Where(t => t.ContactId == Id && t.GroupId == g.Id)
                        select g).Distinct().ToList();
            }
        }
    }
}
