using System.Windows;
using FitnessClubDB.ViewModels;

namespace FitnessClubDB.Views.ServiceWindows;

public partial class TrainerRecordWindow : Window
{
    public TrainerRecordWindow(TrainerRecordViewModel trainerRecordViewModel)
    {
        Owner = Application.Current.MainWindow;
        DataContext = trainerRecordViewModel;
        InitializeComponent();
    }
}