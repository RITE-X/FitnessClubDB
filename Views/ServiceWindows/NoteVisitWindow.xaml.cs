using System.Windows;
using FitnessClubDB.ViewModels;

namespace FitnessClubDB.Views.ServiceWindows;

public partial class NoteVisitWindow : Window
{
    public NoteVisitWindow(NoteVisitViewModel noteVisitViewModel)
    {
        Owner = Application.Current.MainWindow;
        DataContext = noteVisitViewModel;
        InitializeComponent();
   
    }
}