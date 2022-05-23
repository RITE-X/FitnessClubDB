using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels.View;

public class MainWindowViewModel : ViewModel
{
    public BindingList<Cheque> Cheques => Root.FitnessClubContext.Cheques.Local.ToBindingList();
    public BindingList<Client> Clients => Root.FitnessClubContext.Clients.Local.ToBindingList();
    public BindingList<Complex> Complexes => Root.FitnessClubContext.Complexes.Local.ToBindingList();
    public BindingList<Membership> Memberships => Root.FitnessClubContext.Memberships.Local.ToBindingList();
    public BindingList<Trainer> Trainers => Root.FitnessClubContext.Trainers.Local.ToBindingList();


    public BindingList<Visit> Visits => Root.FitnessClubContext.Visits.Local.ToBindingList();
    public BindingList<Workout> Workouts => Root.FitnessClubContext.Workouts.Local.ToBindingList();

    private IList _home = null!;

    public IList Home
    {
        get => _home;
        set => Set(ref _home, value);
    }

    public MainWindowViewModel()
    {
        Root.FitnessClubContext.Cheques.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Cheques));
        Root.FitnessClubContext.Clients.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Clients));
        Root.FitnessClubContext.Complexes.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Complexes));
        Root.FitnessClubContext.Memberships.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Memberships));
        Root.FitnessClubContext.Trainers.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Trainers));
        Root.FitnessClubContext.Visits.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Visits));
        Root.FitnessClubContext.Workouts.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Workouts));
       
    }

    private RoutedCommand? _getTrainings;

    public RoutedCommand GetTrainings => _getTrainings ??= new RoutedCommand(o =>
    {
        var trainerId = (int) o;

        var trainer = Root.FitnessClubContext.Trainers.Find(trainerId);

        Home = trainer.Workouts.Select(workout => new
        {
            Клієнт = workout.Client.LastName + workout.Client.FirstName + workout.Client.MiddleName,
            Час = workout.StartTime
        }).ToList();
    });

    private RoutedCommand? _getVisits;
    //TODO add -1 check
    public RoutedCommand GetVisits => _getVisits ??= new RoutedCommand(o =>
    {
        var clientId = (int) o;

        var client = Root.FitnessClubContext.Clients.Find(clientId);

        Home = client.Visits.Select(visit => new {Комплекс = visit.Complex.ComplexName, Час = visit.VisitTime}).ToList();


    });
    
};