using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string _name;
        private string _header = "";
        private string _footer = "";

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
    }
}
