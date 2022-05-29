using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FitnessClubDB.Commands;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.Services.WindowCloser;
using FitnessClubDB.ViewModels.Base;

namespace FitnessClubDB.ViewModels;

public class NoteVisitViewModel : ViewModel, ICloseWindow, IDataErrorInfo
{
    private readonly Client _client;

    public NoteVisitViewModel(Client client)
    {
        _client = client;
        VisitDate = DateTime.Now;
    }

    public ObservableCollection<Complex> Complexes => DBRoot.GetContext().Complexes.Local.ToObservableCollection();

    private Complex? _selectedComplex;

    public Complex? SelectedComplex
    {
        get => _selectedComplex;
        set => Set(ref _selectedComplex, value);
    }

    private DateTime _visitDate;

    public DateTime VisitDate
    {
        get => _visitDate;
        set => Set(ref _visitDate, value);
    }

    private RoutedCommand? _noteVisit;
    public RoutedCommand NoteVisit => _noteVisit ??= new RoutedCommand(_ =>
    {
        DBRoot.GetContext().Visits.Local.Add(new Visit
            {ClientId = _client.Id, ComplexId = _selectedComplex.Id, VisitTime = _visitDate});
        DBRoot.GetContext().SaveChanges();
        Close?.Invoke();
    }, _ => SelectedComplex != null);

    public Action? Close { get; set; }
    public bool CanClose() => true;

    public string Error => throw new NotImplementedException();

    public string this[string columnName]
    {
        get
        {
            var error = string.Empty;
            switch (columnName)
            {
                case nameof(SelectedComplex):
                    if (SelectedComplex == null)
                        error = "Оберіть комплекс";
                    break;
            }


            return error;
        }
    }
}