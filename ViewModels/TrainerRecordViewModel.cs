using System;
using System.ComponentModel;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class TrainerRecordViewModel : ViewModel, ICloseWindow, IDataErrorInfo
{
    private readonly Client _client;

    public TrainerRecordViewModel(Client client)
    {
        _client = client;
        StartTime = DateTime.Now;
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

    
    public bool IsDateAccessible
    {
        get
        {
            var flag = false;
            foreach (var cheque in _client.Cheques)
            {
                foreach (var membershipServices in cheque.Service.MembershipServices)
                {
                    flag = membershipServices.Membership.TimeLimitUntil > StartTime.TimeOfDay;
                }
            }

            return flag;
        }
    }
    
    public Action? Close { get; set; }

    public bool CanClose() => true;
    public string Error =>  throw new NotImplementedException();

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;

            switch (columnName)
            {
                case nameof(SelectedSpecialization):
                    if (SelectedSpecialization is null)
                        error = "Оберіть спеціалізацію";
                    break;

                case nameof(SelectedTrainer):
                    if (SelectedTrainer is null)
                        error = "Оберіть тренера";
                    break;
    
                case nameof(StartTime):
                    if (StartTime < DateTime.Now)
                        error = "Не можна вибрати минулу дату";
                    else if(!IsDateAccessible)
                        error = "Ваш абонемент не дозволяє обрати цей час";
                    break;
                  
            }
            
            return error;
        }
    }
}