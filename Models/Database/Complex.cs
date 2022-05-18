using System.Collections.Generic;

namespace FitnessClubDB.Models.Database
{
    public partial class Complex
    {
        public Complex()
        {
            MembershipServices = new HashSet<MembershipService>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string ComplexName { get; set; } = null!;

        public virtual ICollection<MembershipService> MembershipServices { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
