using System;

namespace FitnessClubDB.Models.Database
{
    public partial class Visit
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ComplexId { get; set; }
        public DateTime VisitTime { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Complex Complex { get; set; } = null!;
    }
}
