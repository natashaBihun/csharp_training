using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectData() { }
        public bool Equals(ProjectData otherProject)
        {
            if (Object.ReferenceEquals(otherProject, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, otherProject))
            {
                return true;
            }
            return Name == otherProject.Name;
        }
        public int CompareTo(ProjectData otherProject)
        {
            if (Object.ReferenceEquals(otherProject, null))
            {
                return 1;
            }
            return Name.CompareTo(otherProject.Name);
        }
        public override string ToString()
        {
            return "id =" + Id + "\n name = " + Name + "\n descr. = " + Description;
        }
    }
}
