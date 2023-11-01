// @Author: Akram El Assas
// @License: CPOL

using Functional.Core;
using Functional.Core.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Functional.EuclideanPlane;

public partial class MainWindow
{
    #region Constructor

    public MainWindow()
    {
        InitializeComponent();
    }

    #endregion

    #region Animations and Fractal

    // init //

    private const double DELTA = 50;
    private const double TWO_PI = 2 * Math.PI;
    private const double HALF_PI = Math.PI / 2;
    private double _diskDeltay;
    private double _lambdaFactor = 1;
    private double _diskScaleDeltay;
    private double _theta;
    private readonly Predicate<Point> _disk = Plane.Disk(new Point(0, -170), 80);
    private readonly Predicate<Point> _disk2 = Plane.Disk(new Point(0, -230), 20);
    private readonly Predicate<Point> _halfPlane = Plane.VerticalHalfPlane(220, false);
    private readonly Dictionary<string, DispatcherTimer> _timers = new();
    private string _previousTimer = string.Empty;

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var translateTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1, 0) };
        translateTimer.Tick += TranslateTimer_Tick;
        _timers.Add("translateTimer", translateTimer);

        var scaleTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1, 0) };
        scaleTimer.Tick += ScaleTimer_Tick;
        _timers.Add("scaleTimer", scaleTimer);

        var rotateTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1, 0) };
        rotateTimer.Tick += RotateTimer_Tick;
        _timers.Add("rotateTimer", rotateTimer);
    }

    private void StopPreviousAnimation()
    {
        if (!string.IsNullOrEmpty(_previousTimer)) _timers[_previousTimer].Stop();
    }

    private void RunTimer(string key)
    {
        StopPreviousAnimation();
        _timers[_previousTimer = key].Start();
    }

    // Translate //

    private void TranslateDiskAnimation()
    {
        // Timer
        RunTimer("translateTimer");
    }

    private void TranslateTimer_Tick(object? sender, EventArgs e)
    {
        _diskDeltay = _diskDeltay <= PlaneCanvas.Height ? _diskDeltay + DELTA : DELTA;
        Predicate<Point> translatedDisk = _diskDeltay <= PlaneCanvas.Height ? _disk.TranslateSet(0, _diskDeltay) : _disk;
        translatedDisk.Draw(PlaneCanvas);
    }

    // Scale //

    private void ScaleDiskAnimation()
    {
        // Timer
        RunTimer("scaleTimer");
    }

    private void ScaleTimer_Tick(object? sender, EventArgs e)
    {
        _diskScaleDeltay = _diskScaleDeltay <= PlaneCanvas.Height ? _diskScaleDeltay + DELTA : DELTA;
        _lambdaFactor = _diskScaleDeltay <= PlaneCanvas.Height ? _lambdaFactor + 0.5 : 1;
        Predicate<Point> scaledDisk = _diskScaleDeltay <= PlaneCanvas.Height
            ? _disk2.ScaleSet(0, _diskScaleDeltay, _lambdaFactor, 1)
            : _disk2;
        scaledDisk.Draw(PlaneCanvas);
    }

    // Rotate //

    private void RotateHalfPlaneAnimation()
    {
        // Timer
        RunTimer("rotateTimer");
    }

    private void RotateTimer_Tick(object? sender, EventArgs e)
    {
        _halfPlane.RotateSet(_theta).Draw(PlaneCanvas);
        _theta += HALF_PI;
        _theta %= TWO_PI;
    }

    // Fractal //

    private void DrawNewtonFractal()
    {
        StopPreviousAnimation();
        Plane.NewtonFractal().Draw(PlaneCanvas);
    }

    private void DrawMandelbrotFractal()
    {
        StopPreviousAnimation();
        Plane.MandelbrotFractal().Draw(PlaneCanvas, 20, 1.5);
    }

    #endregion

    #region ComboBox Selection

    private void Disk_Selected(object sender, RoutedEventArgs e)
    {
        StopPreviousAnimation();
        Plane.Disk(new Point(0, 0), 50).Draw(PlaneCanvas);
    }

    private void HorizentalHalfPlane_Selected(object sender, RoutedEventArgs e)
    {
        StopPreviousAnimation();
        Plane.HorizontalHalfPlane(0, true).Draw(PlaneCanvas);
    }

    private void VerticalHalfPlane_Selected(object sender, RoutedEventArgs e)
    {
        StopPreviousAnimation();
        Plane.VerticalHalfPlane(0, false).Draw(PlaneCanvas);
    }

    private void HalfDisk_Selected(object sender, RoutedEventArgs e)
    {
        StopPreviousAnimation();
        Plane.VerticalHalfPlane(0, false).Intersection(Plane.Disk(new Point(0, 0), 50)).Draw(PlaneCanvas);
    }

    private void TranslateAnimation_Selected(object sender, RoutedEventArgs e)
    {
        TranslateDiskAnimation();
    }

    private void ScaleAnimation_Selected(object sender, RoutedEventArgs e)
    {
        ScaleDiskAnimation();
    }

    private void RotateAnimation_Selected(object sender, RoutedEventArgs e)
    {
        RotateHalfPlaneAnimation();
    }

    private void NewtonFractal_Selected(object sender, RoutedEventArgs e)
    {
        DrawNewtonFractal();
    }

    private void MandelbrotFractal_Selected(object sender, RoutedEventArgs e)
    {
        DrawMandelbrotFractal();
    }

    #endregion
}