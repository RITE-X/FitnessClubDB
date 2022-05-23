using FitnessClubDB.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace FitnessClubDB.Models;

public static class Root
{
     public static FitnessClubContext FitnessClubContext { get; }

      static Root()
      {
          FitnessClubContext =
              new FitnessClubContext("Server=(localdb)\\mssqllocaldb;Database=FitnessClub;Trusted_Connection=True;");
              
          FitnessClubContext.Cheques.Load();
          FitnessClubContext.Clients.Load();
          FitnessClubContext.Complexes.Load();
          FitnessClubContext.Memberships.Load();
          FitnessClubContext.Trainers.Load();
          FitnessClubContext.Visits.Load();
          FitnessClubContext.Workouts.Load();

          
      }
}