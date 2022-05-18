namespace FitnessClubDB.Models.Database
{
    public partial class MembershipService
    {
        public int Id { get; set; }
        public int MembershipId { get; set; }
        public int ComplexId { get; set; }

        public virtual Complex Complex { get; set; } = null!;
        public virtual Membership Membership { get; set; } = null!;
    }
}
