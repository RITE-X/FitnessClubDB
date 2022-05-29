using System;
using System.ComponentModel;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class TrainerRecordViewModel : ViewModel, ICloseWindow
{
    private readonly Client _client;

    public TrainerRecordViewModel(Client client)
    {
        _client = client;
    }

    public BindingList<Specialization> Specializations => DBRoot.GetContext().Specializations.Local.ToBindingList();

    private Trainer? _selectedTrainer;

    public Trainer? SelectedTrainer
    {
        get => _selectedTrainer;
        set => Set(ref _selectedTrainer, value);
    }

    private Specialization? _selectedSpecialization;

    public Specialization? SelectedSpecialization
    {
        get => _selectedSpecialization;
        set => Set(ref _selectedSpecialization, value);
    }

    private DateTime _startTime;

    public DateTime StartTime
    {
        get => _startTime;
        set => Set(ref _startTime, value);
    }


    private RoutedCommand? _insertWorkout;

    public RoutedCommand InsertWorkout => _insertWorkout ??= new RoutedCommand(_ =>
    {
        DBRoot.GetContext().Workouts.Local.Add(new Workout
            {ClientId = _client.Id, StartTime = StartTime, Trainer = SelectedTrainer});

        DBRoot.GetContext().SaveChanges();
        Close?.Invoke();
    }, _ => SelectedTrainer != null && SelectedSpecialization != null && StartTime > DateTime.Now);

    public Action? Close { get; set; }

    public bool CanClose() => true;
}