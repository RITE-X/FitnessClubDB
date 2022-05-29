using System;
using System.ComponentModel;
using System.Linq;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class InsertTrainerViewModel : ViewModel, ICloseWindow
{
    public InsertTrainerViewModel()
    {
        SelectedSpecialization = DBRoot.GetContext().Specializations.First();
    }


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
}