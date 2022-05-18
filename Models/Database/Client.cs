using System.Collections.Generic;

namespace FitnessClubDB.Models.Database
{
    public partial class Client
    {
        public Client()
        {
            Cheques = new HashSet<Cheque>();
            Visits = new HashSet<Visit>();
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public int TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; } = null!;
        public virtual ICollection<Cheque> Cheques { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
