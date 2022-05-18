using System;
using System.Collections.Generic;

namespace FitnessClubDB.Models.Database
{
    public partial class Membership
    {
        public Membership()
        {
            Cheques = new HashSet<Cheque>();
            MembershipServices = new HashSet<MembershipService>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ValidityInDays { get; set; }
        public TimeSpan TimeLimitUntil { get; set; }

        public virtual ICollection<Cheque> Cheques { get; set; }
        public virtual ICollection<MembershipService> MembershipServices { get; set; }
    }
}
