// @Author: Akram El Assas
// @License: CPOL

using Functional.Core;

// System.Lazy<T>
var lazyRandom = new Lazy<double>(GetRandomNumber);
//double randomX = lazyRandom; // compilation error: the type Lazy<double> != the type double
double randomX = lazyRandom.Value;
Console.WriteLine("\n Random with System.Lazy<double>.Value: {0}", randomX);

// MyLazy<T>
var myLazyRandom = new MyLazy<double>(GetRandomNumber);
double myRandomX = myLazyRandom; // implicit cast
Console.WriteLine("\n Random with MyLazy<double>: {0}", myRandomX);

Console.ReadKey(false);

static double GetRandomNumber() => new Random().NextDouble();