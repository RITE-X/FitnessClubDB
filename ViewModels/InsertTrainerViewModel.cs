using System;
using System.ComponentModel;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class InsertTrainerViewModel : ViewModel, ICloseWindow, IDataErrorInfo
{
    
    public BindingList<Specialization> Specializations => DBRoot.GetContext().Specializations.Local.ToBindingList();

    private Specialization? _selectedSpecialization;

    public Specialization? SelectedSpecialization
    {
        get => _selectedSpecialization;
        set => Set(ref _selectedSpecialization, value);
    }


    private string? _firstName;

    public string? FirstName
    {
        get => _firstName;
        set => Set(ref _firstName, value);
    }

    private string? _lastName;

    public string? LastName
    {
        get => _lastName;
        set => Set(ref _lastName, value);
    }

    private string? _middleName;

    public string? MiddleName
    {
        get => _middleName;
        set => Set(ref _middleName, value);
    }


    private RoutedCommand? _insertTrainer;
    public RoutedCommand InsertTrainer => _insertTrainer ??= new RoutedCommand(o =>
        {
            DBRoot.GetContext().Trainers.Add(new Trainer
            {
                FirstName = FirstName, LastName = LastName, MiddleName = MiddleName,
                Specialization = SelectedSpecialization
            });
            DBRoot.GetContext().SaveChanges();
            Close?.Invoke();
        },
        o => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(MiddleName) &&
             SelectedSpecialization != null);

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
                case nameof(FirstName):
                    if (string.IsNullOrEmpty(FirstName))
                        error = "Вкажіть ім'я";
                    break;

                case nameof(LastName):
                    if (string.IsNullOrEmpty(LastName))
                        error = "Вкажіть прізвище";
                    break;
                
                case nameof(MiddleName):
                    if (string.IsNullOrEmpty(MiddleName))
                        error = "Вкажіть по батькові";
                    break;
                
                case nameof(SelectedSpecialization):
                    if (SelectedSpecialization is null)
                        error = "Оберіть спеціалізацію";
                    break;
            }
            
            return error;
        }
    }
}