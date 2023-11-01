using Meta.Numerics;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace Functional.Core.WPF;

public static class WpfHelper
{
    #region Extension methods on Predicate<Point>

    public static void Draw(this Predicate<Point> set, Image plane)
    {
        var bitmap = new Bitmap((int)plane.Width, (int)plane.Height);

        //
        // Graph drawing
        //
        double semiWidth = plane.Width / 2;
        double semiHeight = plane.Height / 2;

        double xMin = -semiWidth;
        double xMax = +semiWidth;
        double yMin = -semiHeight;
        double yMax = +semiHeight;

        for (int x = 0; x < bitmap.Height; x++)
        {
            double xp = xMin + x * (xMax - xMin) / plane.Width;

            for (int y = 0; y < bitmap.Width; y++)
            {
                double yp = yMax - y * (yMax - yMin) / plane.Height;

                if (set(new Point(xp, yp)))
                {
                    bitmap.SetPixel(x, y, Color.Black);
                }
            }
        }

        plane.Source = Imaging.CreateBitmapSourceFromHBitmap(
            bitmap.GetHbitmap(),
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
    }

    public static void Draw(this Func<Complex, Complex> fractal, Image plane)
    {
        var bitmap = new Bitmap((int)plane.Width, (int)plane.Height);

        const double reMin = -3.0;
        const double reMax = +3.0;
        const double imMin = -3.0;
        const double imMax = +3.0;

        for (int x = 0; x < plane.Width; x++)
        {
            double re = reMin + x * (reMax - reMin) / plane.Width;
            for (int y = 0; y < plane.Height; y++)
            {
                double im = imMax - y * (imMax - imMin) / plane.Height;

                var z = new Complex(re, im);
                Complex fz = fractal(z);

                if (Double.IsInfinity(fz.Re) || Double.IsNaN(fz.Re) || Double.IsInfinity(fz.Im) ||
                    Double.IsNaN(fz.Im))
                {
                    continue;
                }

                ColorTriplet hsv = ColorMap.ComplexToHsv(fz);

                ColorTriplet rgb = ColorMap.HsvToRgb(hsv);
                var r = (int)Math.Truncate(255.0 * rgb.X);
                var g = (int)Math.Truncate(255.0 * rgb.Y);
                var b = (int)Math.Truncate(255.0 * rgb.Z);
                Color color = Color.FromArgb(r, g, b);

                bitmap.SetPixel(x, y, color);
            }
        }

        plane.Source = Imaging.CreateBitmapSourceFromHBitmap(
            bitmap.GetHbitmap(),
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
    }

    public static void Draw(this Func<Complex, Complex, Complex> fractal, Image plane, int iterationsPerPixel, double boundary)
    {
        var bitmap = new Bitmap((int)plane.Width, (int)plane.Height);

        const double reMin = -1.5;
        const double reMax = +1.5;
        const double imMin = -1.5;
        const double imMax = +1.5;

        for (int x = 0; x < plane.Width; x++)
        {
            double re = reMin + x * (reMax - reMin) / plane.Width;
            for (int y = 0; y < plane.Height; y++)
            {
                double im = imMax - y * (imMax - imMin) / plane.Height;

                var c = new Complex(re, im);
                Complex z = new Complex(0, 0);
                for (int i = 0; i < iterationsPerPixel; i++) z = fractal(c, z);

                if (Math.Sqrt(z.Re * z.Re + z.Im * z.Im) < boundary)
                {
                    bitmap.SetPixel(x, y, Color.Black);
                }
            }
        }

        plane.Source = Imaging.CreateBitmapSourceFromHBitmap(
            bitmap.GetHbitmap(),
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));
    }

    #endregion
}