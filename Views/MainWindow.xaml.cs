using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using FitnessClubDB.Models;
using FitnessClubDB.Models.Database;
using FitnessClubDB.ViewModels;

namespace FitnessClubDB.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            => e.Column.Header = ((PropertyDescriptor) e.PropertyDescriptor)?.DisplayName ?? e.Column.Header;

        // private void Home_OnRowEditEnding(object? sender, DataGridRowEditEndingEventArgs e)
        // {
        //   e
        // }
    }
}