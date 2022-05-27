using System.Windows;

namespace FitnessClubDB.Services.WindowCloser;

public class WindowCloser
{
    public static bool GetEnableWindowClosing(DependencyObject obj)
    {
        return (bool) obj.GetValue(EnableWindowClosingProperty);
    }

    public static void SetEnableWindowClosing(DependencyObject obj, bool value)
    {
        obj.SetValue(EnableWindowClosingProperty, value);
    }

    public static readonly DependencyProperty EnableWindowClosingProperty =
        DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser),
            new PropertyMetadata(false, OnEnableWindowClosingChanged));

    private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window window)
        {
            window.Loaded += (_, _) =>
            {
                if (window.DataContext is not ICloseWindow closeWindow)
                    return;

                closeWindow.Close += () => { window.Close(); };

                window.Closing += (_, cancelEventArgs) => { cancelEventArgs.Cancel = !closeWindow.CanClose(); };
            };
        }
    }
}