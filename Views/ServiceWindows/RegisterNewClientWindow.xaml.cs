using System.Windows;
using FitnessClubDB.ViewModels;

namespace FitnessClubDB.Views.ServiceWindows;

public partial class RegisterNewClientWindow : Window
{
    public RegisterNewClientWindow(RegisterNewClientViewModel viewModel)
    {
        Owner = Application.Current.MainWindow;
        DataContext = viewModel;
        InitializeComponent();
    }

}