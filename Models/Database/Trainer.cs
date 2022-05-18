using System.Collections.Generic;

namespace FitnessClubDB.Models.Database
{
    public partial class Trainer
    {
        public Trainer()
        {
            Clients = new HashSet<Client>();
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Specialization { get; set; } = null!;

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
