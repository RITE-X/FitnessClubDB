using FitnessClubDB.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace FitnessClubDB.Models;

public static class DBRoot
{
    private static readonly FitnessClubContext FitnessClubContext;

    static DBRoot()
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
        FitnessClubContext.Specializations.Load();
        FitnessClubContext.MembershipServices.Load();
    }

    public static FitnessClubContext GetContext()
    {
        return FitnessClubContext;
    }
}