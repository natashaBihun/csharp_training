using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string _name;
        private string _header = "";
        private string _footer = "";

        [Column(Name = "group_name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [Column(Name = "group_header")]
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        [Column(Name = "group_footer")]
        public string Footer
        {
            get
            {
                return _footer;
            }
            set
            {
                _footer = value;
            }
        }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public GroupData() { }
        public GroupData(string name)
        {
            _name = name;
        }
        public GroupData(string name, string header, string footer)
        {
            _name = name;
            _header = header;
            _footer = footer;
        }
        public bool Equals(GroupData otherGroup)
        {
            if (Object.ReferenceEquals(otherGroup, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, otherGroup))
            {
                return true;
            }
            return Name == otherGroup.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString()
        {
            return "name =" + Name + "\n header = " + Header + "\n footer = " + Footer;
        }
        public int CompareTo(GroupData otherGroup)
        {
            if (Object.ReferenceEquals(otherGroup, null))
            {
                return 1;
            }
            return Name.CompareTo(otherGroup.Name);
        }

        public static List<GroupData> GetAll() {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts() {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR
                        .Where(t => t.GroupId == Id && t.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
    }
}
