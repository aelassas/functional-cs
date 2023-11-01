// @Author: Akram El Assas
// @License: CPOL

using Meta.Numerics;
using System;
using System.Windows;

namespace Functional.Core.WPF;

public static class Plane
{
    #region Helpers

    private static double EuclidianDistance(Point point1, Point point2) => Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));

    #endregion

    #region Subsets of the Euclidean Plane

    public static Predicate<Point> Disk(Point center, double radius) => p => EuclidianDistance(center, p) <= radius;

    public static Predicate<Point> HorizontalHalfPlane(double y, bool lowerThan) => p => lowerThan ? p.Y <= y : p.Y >= y;

    public static Predicate<Point> VerticalHalfPlane(double x, bool lowerThan) => p => lowerThan ? p.X <= x : p.X >= x;

    #endregion

    #region Translate

    private static Func<Point, Point> Translate(double deltax, double deltay) => p => new Point(p.X + deltax, p.Y + deltay);

    public static Predicate<Point> TranslateSet(this Predicate<Point> set, double deltax, double deltay) => x => set(Translate(-deltax, -deltay)(x));

    #endregion

    #region Scale

    private static Func<Point, Point> Scale(double deltax, double deltay, double lambdax, double lambday) => p => new Point(lambdax * p.X + deltax, lambday * p.Y + deltay);

    public static Predicate<Point> ScaleSet(this Predicate<Point> set, double deltax, double deltay, double lambdax,
        double lambday) =>
        x => set(Scale(-deltax / lambdax, -deltay / lambday, 1 / lambdax, 1 / lambday)(x));

    #endregion

    #region Rotate

    // rotate
    private static Func<Point, Point> Rotate(double theta) => p => new Point(p.X * Math.Cos(theta) - p.Y * Math.Sin(theta), p.X * Math.Sin(theta) + p.Y * Math.Cos(theta));

    public static Predicate<Point> RotateSet(this Predicate<Point> set, double theta) => p => set(Rotate(-theta)(p));

    #endregion

    #region Fractals

    // Newton Fractal P(z) = z^3 - 2*z + 2
    public static Func<Complex, Complex> NewtonFractal() => z => z * z * z - 2 * z + 2;

    // Mandelbrot Fractal Pc(z) = z^2 + c
    public static Func<Complex, Complex, Complex> MandelbrotFractal() => (c, z) => z * z + c;

    #endregion
}