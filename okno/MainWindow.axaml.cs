using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace okno;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private TimeSpan _elapsed;
    private bool _running;

    public MainWindow()
    {
        InitializeComponent();
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
        _timer.Tick += (_, _) =>
        {
            _elapsed += _timer.Interval;
            UpdateDisplay();
        };
    }

    private void OnStartPauseClick(object? sender, RoutedEventArgs e)
    {
        if (_running)
        {
            _timer.Stop();
            _running = false;
            StartPauseButton.Content = "Start";
            return;
        }

        _timer.Start();
        _running = true;
        StartPauseButton.Content = "Pauza";
    }

    private void OnResetClick(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _running = false;
        _elapsed = TimeSpan.Zero;
        StartPauseButton.Content = "Start";
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        var minutes = (int)_elapsed.TotalMinutes;
        TimeText.Text = $"{minutes:00}:{_elapsed.Seconds:00}.{_elapsed.Milliseconds / 10:00}";
    }
}
