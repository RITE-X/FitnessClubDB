using System;

namespace FitnessClubDB.Models.Database
{
    public partial class Cheque
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        
        public virtual Client Client { get; set; } = null!;
        public virtual Membership Service { get; set; } = null!;
    }
}
