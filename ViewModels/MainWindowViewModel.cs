using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.ViewModels.Base;
using FitnessClubDB.Views.InsertWindows;
using FitnessClubDB.Views.ServiceWindows;

namespace FitnessClubDB.ViewModels;

public class MainWindowViewModel : ViewModel
{
    public BindingList<Cheque> Cheques => DBRoot.GetContext().Cheques.Local.ToBindingList();
    public BindingList<Client> Clients => DBRoot.GetContext().Clients.Local.ToBindingList();
    public BindingList<Complex> Complexes => DBRoot.GetContext().Complexes.Local.ToBindingList();
    public BindingList<Membership> Memberships => DBRoot.GetContext().Memberships.Local.ToBindingList();
    public BindingList<Trainer> Trainers => DBRoot.GetContext().Trainers.Local.ToBindingList();
    public BindingList<Visit> Visits => DBRoot.GetContext().Visits.Local.ToBindingList();
    public BindingList<Workout> Workouts => DBRoot.GetContext().Workouts.Local.ToBindingList();

    private IList? _home = null!;

    public IList? Home
    {
        get => _home;
        private set => Set(ref _home, value);
    }


    private TabItem? _selectedTab;

    public TabItem? SelectedTab
    {
        get => _selectedTab;
        set => Set(ref _selectedTab, value);
    }


    public MainWindowViewModel()
    {
        DBRoot.GetContext().Cheques.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Cheques));
        DBRoot.GetContext().Clients.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Clients));
        DBRoot.GetContext().Complexes.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Complexes));
        DBRoot.GetContext().Memberships.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Memberships));
        DBRoot.GetContext().Trainers.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Trainers));
        DBRoot.GetContext().Visits.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Visits));
        DBRoot.GetContext().Workouts.Local.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Workouts));
    }

    private RoutedCommand? _getTrainings;

    public RoutedCommand GetTrainings => _getTrainings ??= new RoutedCommand(parameter =>
    {
        var trainer = parameter as Trainer;

        Home = trainer?.Workouts.Select(workout => new
        {
            Клієнт = $"{workout.Client.LastName} {workout.Client.FirstName} {workout.Client.MiddleName}",
            Час = workout.StartTime
        }).ToList();
    }, o => SelectedTab.Header.Equals("Тренери"));


    private RoutedCommand? _getVisits;

    public RoutedCommand GetVisits => _getVisits ??= new RoutedCommand(parameter =>
    {
        var client = parameter as Client;

        Home = client?.Visits.Select(visit => new {Комплекс = visit.Complex.ComplexName, Час = visit.VisitTime})
            .ToList();
    }, o => SelectedTab.Header.Equals("Клієнти"));


    private RoutedCommand? _openNoteVisitWindow;

    public RoutedCommand OpenNoteVisitWindow => _openNoteVisitWindow ??= new RoutedCommand(parameter =>
    {
        var client = parameter as Client;

        var noteVisitWindow = new NoteVisitWindow(new NoteVisitViewModel(client));
        noteVisitWindow.Show();
    }, o => SelectedTab.Header.Equals("Клієнти"));


    private RoutedCommand? _openTrainerRecordWindow;

    public RoutedCommand OpenTrainerRecordWindow => _openTrainerRecordWindow ??= new RoutedCommand(parameter =>
    {
        var client = parameter as Client;

        var noteVisitWindow = new TrainerRecordWindow(new TrainerRecordViewModel(client));
        noteVisitWindow.Show();
    }, o => SelectedTab.Header.Equals("Клієнти"));


    private RoutedCommand? _openInsertTrainerWindow;

    public RoutedCommand OpenInsertTrainerWindow => _openInsertTrainerWindow ??= new RoutedCommand(_ =>
    {
        var noteVisitWindow = new InsertTrainerWindow(new InsertTrainerViewModel());
        noteVisitWindow.Show();
    });


    private RoutedCommand? _openRegisterNewClientWindow;

    public RoutedCommand OpenRegisterNewClientWindow => _openRegisterNewClientWindow ??= new RoutedCommand(_ =>
    {
        var noteVisitWindow = new RegisterNewClientWindow(new RegisterNewClientViewModel());
        noteVisitWindow.Show();
    });


    private RoutedCommand? _deleteTrainer;

    public RoutedCommand DeleteTrainer => _deleteTrainer ??= new RoutedCommand(parameter =>
    {
        if (parameter is not Trainer trainer)
            return;

        DBRoot.GetContext().Trainers.Remove(trainer);
        DBRoot.GetContext().SaveChanges();
    });


    private RoutedCommand? _getCheques;

    public RoutedCommand GetCheques => _getCheques ??= new RoutedCommand(parameter =>
    {
        var client = parameter as Client;

        Home = client?.Cheques.Select(cheque => new
        {
            Чек = $"{cheque.Id}",
            Клієнт = $"{cheque.Client.LastName} {cheque.Client.FirstName} {cheque.Client.MiddleName}",
            Час = cheque.PurchaseDate, Абонемент = cheque.Service.Title, Сума = cheque.Service.Price
        }).ToList();
    }, o => SelectedTab.Header.Equals("Клієнти"));
};