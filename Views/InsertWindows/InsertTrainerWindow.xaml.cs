using System.Windows;
using FitnessClubDB.ViewModels;

namespace FitnessClubDB.Views.InsertWindows;

public partial class InsertTrainerWindow : Window
{
    public InsertTrainerWindow(InsertTrainerViewModel viewModel)
    {
        Owner = Application.Current.MainWindow;
        DataContext = viewModel;
        InitializeComponent();
    }
}