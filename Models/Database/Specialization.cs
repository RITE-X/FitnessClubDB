using System.Collections.Generic;

namespace FitnessClubDB.Models.Database
{
    public partial class Specialization
    {
        public Specialization()
        {
            Trainers = new HashSet<Trainer>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}