using System;
using System.ComponentModel;
using System.Linq;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class RegisterNewClientViewModel : ViewModel, ICloseWindow, IDataErrorInfo
{
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

    public BindingList<Trainer> Trainers => DBRoot.GetContext().Trainers.Local.ToBindingList();


    private Trainer? _selectedTrainer;

    public Trainer? SelectedTrainer
    {
        get => _selectedTrainer;
        set => Set(ref _selectedTrainer, value);
    }

    public BindingList<Membership> Memberships => DBRoot.GetContext().Memberships.Local.ToBindingList();

    private Membership? _selectedMembership;

    public Membership? SelectedMembership
    {
        get => _selectedMembership;
        set => Set(ref _selectedMembership, value);
    }

    private RoutedCommand? _registerNewClient;

    public RoutedCommand? RegisterNewClient => _registerNewClient ??= new RoutedCommand(o =>
    {
        DBRoot.GetContext().Clients.Local.Add(new Client
            {FirstName = FirstName, LastName = LastName, MiddleName = MiddleName, Trainer = SelectedTrainer});


        DBRoot.GetContext().Cheques.Local.Add(new Cheque
        {
            ClientId = DBRoot.GetContext().Clients.Local.Last().Id, PurchaseDate = DateTime.Now,
            Service = SelectedMembership
        });

        DBRoot.GetContext().SaveChanges();

        Close?.Invoke();
    }, o => !string.IsNullOrEmpty(FirstName)  && !string.IsNullOrEmpty(LastName) && SelectedTrainer != null && SelectedMembership != null);

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
                
                case nameof(SelectedTrainer):
                    if (SelectedTrainer is null)
                        error = "Оберіть тренера";
                    break;
                case nameof(SelectedMembership):
                    if (SelectedMembership is null)
                        error = "Оберіть абонемент";
                    break;
            }
            
            return error;
        }
    }
}