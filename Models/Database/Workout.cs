using System;

namespace FitnessClubDB.Models.Database
{
    public partial class Workout
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Trainer Trainer { get; set; } = null!;
    }
}
