[![build](https://github.com/aelassas/functional-cs/actions/workflows/build.yml/badge.svg)](https://github.com/aelassas/functional-cs/actions/workflows/build.yml) [![test](https://github.com/aelassas/functional-cs/actions/workflows/test.yml/badge.svg)](https://github.com/aelassas/functional-cs/actions/workflows/test.yml) [![coverage](https://img.shields.io/codecov/c/github/aelassas/functional-cs/main.svg?logo=codecov)](https://codecov.io/gh/aelassas/functional-cs)

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Article.gif?raw=true)

## Contents

1.  [Introduction](#intro)
2.  [Development Environment](#dev-env)
3.  [Representing Data Through Functions](#rep-data)
    1.  [Sets](#sets)
    2.  [Binary Operations](#bin-op)
    3.  [Go Further](#func-go-further)
4.  [Euclidean Plane](#plane)
    1.  [Drawing a Disk](#disk)
    2.  [Drawing Horizontal and Vertical Half-planes](#half-plane)
    3.  [Functions](#func)
    4.  [Go Further](#plane-further)
5.  [Fractals](#fractals)
    1.  [Complex Numbers and Drawing](#complex-draw)
    2.  [Mandelbrot Fractal](#mandelbrot-fractal)
    3.  [Newton Fractal](#newton-fractal)
    4.  [Go Further](#fractal-further)
6.  [Introduction to Laziness](#laziness)
7.  [Unit Tests](#unit-tests)

## <a id="intro" name="intro">Introduction</a>

Functional programming is a programming paradigm based on functions, their compositions, and also on decomposition into functions.

There are two possible properties of functions:

1.  **Purity**: Functions have results that depend strictly on their arguments, with no other side effect. Purity leads to compartmentalization, localization, stability, and determinism.
2.  **First-class citizenship**: Functions have value status. Functions can be named, assigned, typed, created on demand, passed as an argument to a function, be the result of a function, and stored in a data structure. First-class citizenship leads to flexibility of use and compositionality.

Functional programming consists in exploiting one and/or the other of these two properties.

C# supports functional programming concepts, though it's primarily an object-oriented language. Here are the main functional programming features supported in C#:
* **First-class functions**: lambda expressions, delegates, method references
* **Immutability**: records, immutable collections, readonly members
* **Pure functions**: methods without side effects, deterministic outputs
* **Higher-order functions**: LINQ operations, methods that take functions as parameters
* **Pattern matching**: switch expressions, property patterns, type patterns
* **Expression trees**: represent code as data, used extensively in LINQ providers

This article will not discuss the basics of functional programming, as you can find numerous resources on this topic. Instead, it will talk about functional programming in C# applied to algebra, numbers, Euclidean plane, and fractals. The examples provided in this article will start from simple to more complex but always illustrated in a simple, straightforward and easy-to-understand manner.

## <a id="dev-env" name="dev-env">Development Environment</a>

To run the source code, you will need to install [Visual Studio 2022](https://visualstudio.microsoft.com/). Once installed, clone the GitHub repository:
```
git clone https://github.com/aelassas/functional-cs.git
```

Then open `./functional-cs/Functional.sln` with Visual Studio.

Below are the projects available in the solution:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/solution.png?raw=true)

*   `Functional.Core` is a class library that contains sets functions and helpers.
*   `Functional.Core.WPF` is a WPF class library that contains plane and fractals functions and helpers.
*   `Functional.EuclideanPlane` is a WPF application that contains Euclidean Plane and Fractals samples.
*   `Functional.Laziness` is a Console application that contains Laziness samples.
*   `Functional.Set` is a Console application that contains sets samples.
*   `Functional.UnitTests` is the unit tests project.

To run numbers demo, run `Functional.Set` project.

To run Euclidean plane and fractals demos, run `Functional.EuclideanPlane` project.

To run laziness demo, run `Functional.Laziness` project.

To run unit tests, run the following command:
```
cd functional-cs/tests/Functional.UnitTests
dotnet test
```

## <a id="rep-data" name="rep-data">Representing Data Through Functions</a>

Let `S` be any set of elements `a`, `b`, `c` ... (for instance, the books on the table, or the videos in YouTube, or the points of the Euclidean plane) and let `S'` be any subset of these elements (for instance, the green books on the table, or the cultural videos in YouTube, or the points in the circle of radius 1 centered at the origin of the Euclidean plane).

The _[Characteristic Function](http://en.wikipedia.org/wiki/Indicator_function)_ `S'(x)` of the set `S'` is a function which associates either `true` or `false` with each element `x` of `S`.

```
S'(x) = true if x is in S'
S'(x) = false if x is not in S'
```

Let `S` be the set of books on the table and let `S'` be the set of green books on the table. Let `a` and `b` be two green books, and let `c` and `d` be two red books on the table. Then:

```
S'(a) = S'(b) = true
S'(c) = S'(d) = false
```

Let `S` be the set of the videos in YouTube and let `S'` be the set of cultural videos in YouTube. Let `a` and `b` be two cultural videos in YouTube, and `c` and `d` be two non-cultural videos in YouTube. Then:

```
S'(a) = S'(b) = true
S'(c) = S'(d) = false
```

Let `S` be the set of the points in the Euclidean plane and let `S'` be the set of the points in the circle of radius 1 centered at the origin of the Euclidean plane (0, 0) _(unit circle)_. Let `a` and `b` be two points in the unit circle, and let `c` and `d` be two points in a circle of radius 2 centered at the origin of the Euclidean plane. Then:

```
S'(a) = S'(b) = true
S'(c) = S'(d) = false
```

Thus, any set `S'` can always be represented by its _Characteristic Function_. A function that takes as argument an element and returns `true` if this element is in `S'`, `false` otherwise. In other words, a set (abstract data type) can be represented through a `Predicate` in C#.

```csharp
Predicate<T> set;
```

In the next sections, we will see how to represent some fundamental sets in the algebra of sets through C# in a functional way, then we will define generic binary operations on sets. We will then apply these operations on numbers then on subsets of the Euclidean Plane. Sets are abstract data structures, the subsets of numbers and the subsets of the Euclidean plane are the representation of abstract data-structures, and finally the binary operations are the generic logics that works on any representation of the abstract data structures.

### <a id="sets" name="sets">Sets</a>

This section introduces the representation of some fundamental sets in the algebra of sets through C#.

#### Empty Set

![Image](https://github.com/aelassas/functional-cs/blob/main/img/EmptySet.png?raw=true)

Let `E` be the empty set and `Empty` its _Characteristic function_. In algebra of sets, `E` is the unique set having no elements. Therefore, `Empty` can be defined as follows:

```
Empty(x) = false if x is in E
Empty(x) = false if x is not in E
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
public static Predicate<T> Empty<T>() => _ => false;
```

In algebra of sets, `Empty` is represented as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/EmptySetCharacteristicFunction.png?raw=true)

Thus, running the code below:

```csharp
Console.WriteLine("\nEmpty set:");
Console.WriteLine("Is 7 in {{}}? {0}", Empty<int>()(7));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/EmptySetDemo.PNG?raw=true)

#### Set All

![Image](https://github.com/aelassas/functional-cs/blob/main/img/AllSet.png?raw=true)

Let `S` be a set and `S'` be the subset of `S` that contains all the elements and `All` its _Characteristic function_. In algebra of sets, `S'` is the full set that contains all the elements. Therefore, `All` can be defined like this:

```
All(x) = true if x is in S
```

Thus, the representation of `S'` in C# can be defined as follows:

```csharp
public static Predicate<T> All<T>() => _ => true;
```

In algebra of sets, `All` is represented as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/AllSetCharacteristicFunction.png?raw=true)

Thus, running the code below:

```csharp
Console.WriteLine("Is 7 in the integers set? {0}", All<int>()(7));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/AllSetDemo.png?raw=true)

#### Singleton Set

Let `E` be the Singleton set and `Singleton` its _Characteristic function_. In algebra of sets, `E` also known as unit set, or 1-tuple is a set with exactly one element `e`. Therefore, `Singleton` can be defined as follows:

```
Singleton(x) = true if x is e
Singleton(x) = false if x is not e
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
public static Predicate<T> Singleton<T>(T e) where T : notnull => x => e.Equals(x);
```

Thus, running the code below:

```csharp
Console.WriteLine("Is 7 in the singleton {{0}}? {0}", Singleton(0)(7));
Console.WriteLine("Is 7 in the singleton {{7}}? {0}", Singleton(7)(7));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/SingletonSetDemo.png?raw=true)

#### Other Sets

This section presents subsets of the integers set.

##### Even numbers

Let `E` be the set of even numbers and `Even` its _Characteristic function_. In mathematics, an even number is a number which is a multiple of two. Therefore, `Even` can be defined as follows:

```
Even(x) = true if x is a multiple of 2
Even(x) = false if x is not a multiple of 2
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
Predicate<int> even = i => i % 2 == 0;
```

Thus, running the code below:

```csharp
Console.WriteLine("Is {0} even? {1}", 99, even(99));
Console.WriteLine("Is {0} even? {1}", 998, even(998));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/EvenSetDemo.PNG?raw=true)

##### Odd Numbers

Let `E` be the set of odd numbers and `Odd` its _Characteristic function_. In mathematics, an odd number is a number which is not a multiple of two. Therefore, `Odd` can be defined as follows:

```
Odd(x) = true if x is not a multiple of 2
Odd(x) = false if x is a multiple of 2
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
Predicate<int> odd = i => i % 2 == 1;
```

Thus, running the code below:

```csharp
Console.WriteLine("Is {0} odd? {1}", 99, odd(99));
Console.WriteLine("Is {0} odd? {1}", 998, odd(998));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/OddSetDemo.PNG?raw=true)

##### Multiples of 3

Let `E` be the set of multiples of 3 and `MultipleOfThree` its _Characteristic function_. In mathematics, a multiple of 3 is a number divisible by 3\. Therefore, `MultipleOfThree` can be defined as follows:

```
MultipleOfThree(x) = true if x is divisible by 3
MultipleOfThree(x) = false if x is not divisible by 3
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
Predicate<int> multipleOfThree = i => i % 3 == 0;
```

Thus, running the code below:

```csharp
Console.WriteLine("Is {0} a multiple of 3? {1}", 99, multipleOfThree(99));
Console.WriteLine("Is {0} a multiple of 3? {1}", 998, multipleOfThree(998));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/MultiplesOf3SetDemo.PNG?raw=true)

##### Multiples of 5

Let `E` be the set of multiples of 5 and `MultipleOfFive` its _Characteristic function_. In mathematics, a multiple of 5 is a number divisible by 5\. Therefore, `MultipleOfFive` can be defined as follows:

```
MultipleOfFive(x) = true if x is divisible by 5
MultipleOfFive(x) = false if x is not divisible by 5
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
Predicate<int> multipleOfFive = i => i % 5 == 0;
```

Thus, running the code below:

```csharp
Console.WriteLine("Is {0} a multiple of 5? {1}", 15, multipleOfFive(15));
Console.WriteLine("Is {0} a multiple of 5? {1}", 998, multipleOfFive(998));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/MultiplesOf5SetDemo.PNG?raw=true)

##### Prime Numbers

A long time ago, when I was playing with [Project Euler](http://projecteuler.net/) problems, I had to resolve the following one:

```
By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, 
we can see that the 6th prime is 13.
What is the 10 001st prime number?
```

To resolve this problem, I first had to write a fast algorithm that checks whether a given number is prime or not. Once the algorithm was written, I wrote an iterative algorithm that iterates through primes until the 10 001st prime number was found. Nevertheless, is the next iterative algorithm really necessary? You will see.

The algorithm that checks whether a given number is prime or not is the _Characteristical function_ of the primes set.

Let `E` be the set of primes and `Prime` its _Characteristic function_. In mathematics, a prime is a natural number greater than 1 that has no positive divisors other than 1 and itself. Therefore, `Prime` can be defined as follows:

```
Prime(x) = true if x is prime
Prime(x) = false if x is not prime
```

Thus, the representation of `E` in C# can be defined as follows:

```csharp
Predicate<int> prime = IsPrime;
```

where `IsPrime` is a method that checks whether a given number is prime or not.

```csharp
static bool IsPrime(int i)
{
    if (i == 1) return false;            // 1 is not prime
    if (i < 4) return true;              // 2 and 3 are primes
    if ((i >> 1) * 2 == i) return false; // multiples of 2 are not prime
    if (i < 9) return true;              // 5 and 7 are primes
    if (i % 3 == 0) return false;        // multiples of 3 are not primes

    // If a divisor less than or equal to sqrt(i) is found,
    // then i is not prime
    int sqrt = (int)Math.Sqrt(i);
    for (int d = 5; d <= sqrt; d += 6)
    {
        if (i % d == 0) return false;
        if (i % (d + 2) == 0) return false;
    }

    // Otherwise i is prime
    return true;
}
```

Thus, running the code below to resolve our problem:

```csharp
int p = Primes(prime).Skip(10000).First();
Console.WriteLine("The 10 001st prime number is {0}", p);
```

where `Primes` is defined below:

```csharp
static IEnumerable <int> Primes(Predicate<int> prime)
{
    yield return 2;

    int p = 3;
    while (true)
    {
        if (prime(p)) yield return p;
        p += 2;
    }
}
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/PrimesSetDemo.PNG?raw=true)

### <a id="bin-op" name="bin-op">Binary Operations</a>

This section presents several fundamental operations for constructing new sets from given sets and for manipulating sets. Below is the [Venn diagram](http://en.wikipedia.org/wiki/Venn_diagram) in the algebra of sets.

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Logical_connectives_Hasse_diagram.png?raw=true)

#### Union

![Image](https://github.com/aelassas/functional-cs/blob/main/img/UnionOperation.png?raw=true)

Let `E` and `F` be two sets. The _union_ of `E` and `F`, denoted by `E U F` is the set of all elements which are members of either `E` and `F`.

Let `Union` be the _union_ operation. Thus, the `Union` operation can be implemented as follows in C#:

```csharp
public static Predicate<T> Union<T>(this Predicate<T> e, Predicate<T> f)
  => x => e(x) || f(x);
```

As you can see, `Union` is an extension function on the _Characteristic function_ of a set. All the operations will be defined as extension functions on the _Characteristic function_ of a set. Thereby, running the code below:

```csharp
Console.WriteLine("Is 7 in the union of Even and Odd Integers Set? {0}", Even.Union(Odd)(7));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/UnionOperationDemo.png?raw=true)

#### Intersection

![Image](https://github.com/aelassas/functional-cs/blob/main/img/IntersectionOperation.png?raw=true)

Let `E` and `F` be two sets. The _intersection_ of `E` and `F`, denoted by `E n F` is the set of all elements which are members of both `E` and `F`.

Let `Intersection` be the _intersection_ operation. Thus, the `Intersection` operation can be implemented as follows in C#:

```csharp
public static Predicate<T> Intersection<T>(this Predicate<T> e, Predicate<T> f)
  => x => e(x) && f(x);
```

As you can see, `Intersection` is an extension function on the _Characteristic function_ of a set. Thereby, running the code below:

```csharp
Predicate<int> multiplesOfThreeAndFive = multipleOfThree.Intersection(multipleOfFive);
Console.WriteLine("Is 15 a multiple of 3 and 5? {0}", multiplesOfThreeAndFive(15));
Console.WriteLine("Is 10 a multiple of 3 and 5? {0}", multiplesOfThreeAndFive(10));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/IntersectionOperationDemo.png?raw=true)

#### Cartesian Product

![Image](https://github.com/aelassas/functional-cs/blob/main/img/CartesianProductOperation.png?raw=true)

Let `E` and `F` be two sets. The _cartesian product_ of `E` and `F`, denoted by `E × F` is the set of all ordered pairs `(e, f)` such that `e` is a member of `E` and `f` is a member of `F`.

Let `CartesianProduct` be the _cartesian product_ operation. Thus, the `CartesianProduct` operation can be implemented as follows in C#:

```csharp
public static Func<T1, T2, bool> 
       CartesianProduct<T1, T2>(this Predicate<T1> e, Predicate<T2> f) => (x, y)
  => e(x) && f(y);
```

As you can see, `CartesianProduct` is an extension function on the _Characteristic function_ of a set. Thereby, running the code below:

```csharp
Func<int, int, bool> cartesianProduct = multipleOfThree.CartesianProduct(multipleOfFive);
Console.WriteLine("Is (9, 15) in MultipleOfThree x MultipleOfFive? {0}", 
                                 cartesianProduct(9, 15));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/CartesianProductOperationDemo.PNG?raw=true)

#### Complements

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ComplementOperation.png?raw=true)

Let `E` and `F` be two sets. The _relative complement_ of `F` in `E`, denoted by `E \ F` is the set of all elements which are members of `E` but not members of `F`.

Let `Complement` be the _relative complement_ operation. Thus, the `Complement` operation can be implemented as follows in C#:

```csharp
public static Predicate<T> Complement<T>(this Predicate<T> e, Predicate<T> f)
  => x => e(x) && !f(x);
```

As you can see, `Complement` is an extension method on the _Characteristic function_ of a set. Thereby, running the code below:

```csharp
Console.WriteLine("Is 15 in MultipleOfThree \\ MultipleOfFive set? {0}", 
          multipleOfThree.Complement(multipleOfFive)(15));
Console.WriteLine("Is 9 in MultipleOfThree \\ MultipleOfFive set? {0}", 
          multipleOfThree.Complement(multipleOfFive)(9));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ComplementOperationDemo.PNG?raw=true)

#### Symmetric Difference

![Image](https://github.com/aelassas/functional-cs/blob/main/img/SymetricDifference.png?raw=true)

Let `E` and `F` be two sets. The _symmetric difference_ of `E` and `F`, denoted by `E Δ F` is the set of all elements which are members of either `E` and `F` but not in the intersection of `E` and `F`.

Let `SymmetricDifference` be the _symmetric difference_ operation. Thus, the `SymmetricDifference` operation can be implemented in two ways in C#. A trivial way is to use the union and complement operations as follows:

```csharp
public static Predicate<T> SymmetricDifferenceWithoutXor<T>
              (this Predicate<T> e, Predicate<T> f)
  => Union(e.Complement(f), f.Complement(e));
```

Another way is to use the `XOR` binary operation as follows:

```csharp
public static Predicate<T> SymmetricDifferenceWithXor<T>
              (this Predicate<T> e, Predicate<T> f)
  => x => e(x) ^ f(x);
```

As you can see, `SymmetricDifferenceWithoutXor` and `SymmetricDifferenceWithXor` are extension methods on the _Characteristic function_ of a set. Thereby, running the code below:

```csharp
// SymmetricDifference without XOR
Console.WriteLine("\nSymmetricDifference without XOR:");
Predicate<int> sdWithoutXor = prime.SymmetricDifferenceWithoutXor(even);
Console.WriteLine("Is 2 in the symmetric difference of prime and even Sets? {0}", 
                   sdWithoutXor(2));
Console.WriteLine("Is 4 in the symmetric difference of prime and even Sets? {0}", 
                   sdWithoutXor(4));
Console.WriteLine("Is 7 in the symmetric difference of prime and even Sets? {0}", 
                   sdWithoutXor(7));

// SymmetricDifference with XOR
Console.WriteLine("\nSymmetricDifference with XOR:");
Predicate<int> sdWithXor = prime.SymmetricDifferenceWithXor(even);
Console.WriteLine("Is 2 in the symetric difference of prime and even Sets? {0}", 
                   sdWithXor(2));
Console.WriteLine("Is 4 in the symmetric difference of prime and even Sets? {0}", 
                   sdWithXor(4));
Console.WriteLine("Is 7 in the symmetric difference of prime and even Sets? {0}", 
                   sdWithXor(7));
```

gives the following results:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/SymetricDifferenceDemo.png?raw=true)

#### Other Operations

This section presents other useful binary operations on sets.

##### Contains

Let `Contains` be the operation that checks whether or not an element is in a set. This operation is an extension function on the _Characteristic function_ of a set that takes as parameter an element and returns `true` if the element is in the set, `false` otherwise.

Thus, this operation is defined as follows in C#:

```csharp
public static bool Contains<T>(this Predicate<T> e, T x) => e(x);
```

Therefore, running the code below:

```csharp
Console.WriteLine("Is 7 in the singleton {{0}}? {0}", Singleton(0).Contains(7));
Console.WriteLine("Is 7 in the singleton {{7}}? {0}", Singleton(7).Contains(7));
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ContainsOperationDemo.PNG?raw=true)

##### Add

Let `Add` be the operation that adds an element to a set. This operation is an extension function on the _Characteristic function_ of a set that takes as parameter an element and adds it to the set.

Thus, this operation is defined as follows in C#:

```csharp
public static Predicate<T> Add<T>(this Predicate<T> s, T e) where T : notnull
  => x => x.Equals(e) || s(x);
```

Therefore, running the code below:

```csharp
Console.WriteLine("Is 7 in {{0, 7}}? {0}", Singleton(0).Add(7)(7));
Console.WriteLine("Is 0 in {{1, 0}}? {0}", Singleton(1).Add(0)(0));
Console.WriteLine("Is 7 in {{19, 0}}? {0}", Singleton(19).Add(0)(7));
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/AddOperationDemo.PNG?raw=true)

##### Remove

Let `Remove` be the operation that removes an element from a set. This operation is an extension function on the _Characteristic function_ of a set that takes as parameter an element and removes it from the set.

Thus, this operation is defined as follows in C#:

```csharp
public static Predicate<T> Remove<T>(this Predicate<T> s, T e) where T : notnull
  => x => !x.Equals(e) && s(x);
```

Therefore, running the code below:

```csharp
Console.WriteLine("Is 7 in {{}}? {0}", Singleton(0).Remove(0)(7));
Console.WriteLine("Is 0 in {{}}? {0}", Singleton(7).Remove(7)(0));
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RemoveOperationDemo.PNG?raw=true)

### <a id="func-go-further" name="func-go-further">For Those Who Want to Go Further</a>

You can see how easily we can do some algebra of sets in C# through _functional programming_. In the previous sections was shown the most fundamental definitions. But, if you want to go further, you can think about:

*   Relations over sets
*   Abstract algebra, such as monoids, groups, fields, rings, K-vectorial spaces and so on
*   Inclusion-exclusion principle
*   Russell's paradox
*   Cantor's paradox
*   Dual vector space
*   Theorems and Corollaries

## <a id="plane" name="plane">Euclidean Plane</a>

In the previous section, the fundamental concepts on sets were implemented in C#. In this section, we will practice the concepts implemented on the set of _plane points (Euclidean plane)_.

### <a id="disk" name="disk">Drawing a Disk</a>

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Disk.png?raw=true)

A disk is a subset of a plane bounded by a circle. There are two types of disks. _Closed_ disks which are disks that contain the points of the circle that constitutes its boundary, and _Open_ disks which are disks that do not contain the points of the circle that constitutes its boundary.

In this section, we will set up the _Characterstic function_ of the _Closed_ disk and draw it in WPF.

To set up the _Characterstic function_, we need first a function that calculates the _Euclidean Distance_ between two points in the plane. This function is implemented as follows:

```csharp
private static double EuclidianDistance(Point point1, Point point2)
  => Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
```

where `Point` is a `struct` defined in the `System.Windows` namespace. This formula is based on Pythagoras' Theorem.

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Pythagorean_theorem_abc.png?raw=true)

where `c` is the _Euclidean distance_, `a²` is `(point1.X - point2.X)²` and `b²` is `(point1.Y - point2.Y)²`.

Let `Disk` be the _Characteristic function_ of a closed disk. In algebra of sets, the definition of a closed disk in the reals set is as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ClosedDiskDefinition.PNG?raw=true)

where `a` and `b` are the coordinates of the center and `R` the radius.

Thus, the implementation of `Disk` in C# is as follows:

```csharp
public static Predicate<Point> Disk(Point center, double radius)
  => p => EuclidianDistance(center, p) <= radius;
```

In order to view the set in a result, I decided to implement a function `Draw` that draws a set in the _Euclidean plane_. I chose _WPF_ and thus used the `System.Windows.Controls.Image` as a canvas and a `Bitmap` as the context.

Thus, I've built the _Euclidean plane_ illustrated below through the method `Draw`.

![Image](https://github.com/aelassas/functional-cs/blob/main/img/EuclideanPlane.png?raw=true)

Below the implementation of the method.

```csharp
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
```

In the `Draw` method, a `bitmap` having the same width and same height as the _Euclidean plane_ container is created. Then each point in pixels `(x,y)` of the `bitmap` is replaced by a black point if it belongs to the `set`. `xMin`, `xMax`, `yMin` and `yMax` are the bounding values illustrated in the figure of the _Euclidean plane_ above.

As you can see, `Draw` is an extension function on the _Characteristic function_ of a set of points. Therefore, running the code below:

```csharp
Plane.Disk(new Point(0, 0), 20).Draw(PlaneCanvas);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ClosedDiskDemo.PNG?raw=true)

### <a id="half-plane" name="half-plane">Drawing Horizontal and Vertical Half-Planes</a>

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Half-Plane.jpg?raw=true)

A _horizontal_ or a _vertical_ half-plane is either of the two subsets into which a plane divides the Euclidean space. A _horizontal_ half-plane is either of the two subsets into which a plane divides the Euclidean space through a line perpendicular with the _Y axis_ like in the figure above. A _vertical_ half-plane is either of the two subsets into which a plane divides the Euclidean space through a line perpendicular with the _X axis_.

In this section, we will set up the _Characteristic functions_ of the _horizontal_ and _vertical_ half-planes, draw them in WPF and see what we can do if we combine them with the _disk_ subset.

Let `HorizontalHalfPlane` be the _Characteristic function_ of a _horizontal_ half-plane. The implementation of `HorizontalHalfPlane` in C# is as follows:

```csharp
public static Predicate<Point> HorizontalHalfPlane(double y, bool lowerThan)
  => p => lowerThan ? p.Y <= y : p.Y >= y;
```

Thus, running the code below:

```csharp
Plane.HorizontalHalfPlane(0, true).Draw(PlaneCanvas);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/HorizentalHalfPlaneDemo.PNG?raw=true)

Let `VerticalHalfPlane` be the _Characteristic function_ of a _vertical_ half-plane. The implementation of `VerticalHalfPlane` in C# is as follows:

```csharp
public static Predicate<Point> VerticalHalfPlane(double x, bool lowerThan)
  => p => lowerThan ? p.X <= x : p.X >= x;
```

Thus, running the code below:

```csharp
Plane.VerticalHalfPlane(0, false).Draw(PlaneCanvas);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/VerticalHalfPlaneDemo.PNG?raw=true)

In the first section of the article, we set up basic binary operations on sets. Thus, by combining the intersection of a `disk` and a `half-plane` for example, we can draw the half-disk subset.

Therefore, running the sample below:

```csharp
Plane.VerticalHalfPlane(0, false).Intersection(Plane.Disk(new Point(0, 0), 20)).Draw(PlaneCanvas);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/HalfDiskDemo.PNG?raw=true)

### <a id="func" name="func">Functions</a>

This section presents functions on the sets in the Euclidean plane.

#### Translate

![Image](https://github.com/aelassas/functional-cs/blob/main/img/TranslateFunction.png?raw=true)

Let `Translate` be the function that translates a point in the plane. In Euclidean geometry, `Translate` is a function that moves a given point a constant distance in a specified direction. Thus the implementation in C# is as follows:

```csharp
private static Func<Point, Point> Translate(double deltax, double deltay)
  => p => new Point(p.X + deltax, p.Y + deltay);
```

where `(deltax, deltay)` is the constant vector of the translation.

Let `TranslateSet` be the function that translates a set in the plane. This function is simply implemented as follows in C#:

```csharp
public static Predicate<Point> TranslateSet
       (this Predicate<Point> set, double deltax, double deltay)
  => x => set(Translate(-deltax, -deltay)(x));
```

`TranslateSet` is an extension function on a set. It takes as parameters `deltax` which is the delta distance in the first Euclidean dimension and `deltay` which is the delta distance in the second Euclidean dimension. If a point _P (x, y)_ is translated in a set _S_, then its coordinates will change to _(x', y') = (x + delatx, y + deltay)_. Thus, the point _(x' - delatx, y' - deltay)_ will always belong to the set _S_. In set algebra, `TranslateSet` is called isomorph, in other words, the set of all translations forms the _translation group T_, which is isomorphic to the space itself. This explains the main logic of the function.

Thus, running the code below in our WPF application:

```csharp
TranslateDiskAnimation();
```

where `TranslateDiskAnimation` is described below:

```csharp
private const double Delta = 50;
private double _diskDeltay;
private readonly Predicate<Point> _disk = Plane.Disk(new Point(0, -170), 80);

private void TranslateDiskAnimation()
{
    DispatcherTimer diskTimer = new DispatcherTimer 
                    { Interval = new TimeSpan(0, 0, 0, 1, 0) };
    diskTimer.Tick += TranslateTimer_Tick;
    diskTimer.Start();
}

private void TranslateTimer_Tick(object? sender, EventArgs e)
{
    _diskDeltay = _diskDeltay <= plan.Height ? _diskDeltay + Delta : Delta;
    Predicate<Point> translatedDisk = _diskDeltay <= plan.Height ? 
                                      _disk.TranslateSet(0, _diskDeltay) : _disk;
    translatedDisk.Draw(PlaneCanvas);
}
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/TranslationDemo.gif?raw=true)

#### Homothety

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ScaleFunction.gif?raw=true)

Let `Scale` be the function that sends any point _M_ to another point _N_ such that the segment _SN_ is on the same line as _SM_, but scaled by a factor **λ**. In algebra of sets, `Scale` is formulated as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ScaleFormula.PNG?raw=true)

Thus the implementation in C# is as follows:

```csharp
private static Func<Point, Point> Scale
    (double deltax, double deltay, double lambdax, double lambday)
  => p => new Point(lambdax * p.X + deltax, lambday * p.Y + deltay);
```

where `(deltax, deltay)` is the constant vector of the translation and `(lambdax, lambday)` is the **λ** vector.

Let `ScaleSet` be the function that applies an homothety on a set in the plan. This function is simply implemented as follows in C#:

```csharp
public static Predicate<Point> ScaleSet
    (this Predicate<Point> set, double deltax, double deltay, double lambdax,
    double lambday) =>
    x => set(Scale(-deltax / lambdax, -deltay / lambday, 1 / lambdax, 1 / lambday)(x));
```

`ScaleSet` is an extension function on a set. It takes as parameters `deltax` which is the delta distance in the first Euclidean dimension, `deltay` which is the delta distance in the second Euclidean dimension and `(lambdax, lambday)` which is the constant factor vector **λ**. If a point _P (x, y)_ is transformed through `ScaleSet` in a set _S_, then its coordinates will change to _(x', y') = (lambdax * x + delatx, lambday * y + deltay)_. Thus, the point _((x'- delatx)/lambdax, (y' - deltay)/lambday)_ will always belong to the set _S_, If **λ** is different from the vector 0, of course. In algebra of sets, `ScaleSet` is called isomorph, in other words the set of all homotheties forms the _Homothety group H_, which is isomorphic to the space itself \ {0}. This explains the main logic of the function.

Thus, running the code below in our WPF application:

```csharp
ScaleDiskAnimation();
```

where `ScaleDiskAnimation` is described below:

```csharp
private const double Delta = 50;
private double _lambdaFactor = 1;
private double _diskScaleDeltay;
private readonly Predicate<Point> _disk2 = Plane.Disk(new Point(0, -230), 20);

private void ScaleDiskAnimation()
{
    DispatcherTimer scaleTimer = new DispatcherTimer 
        { Interval = new TimeSpan(0, 0, 0, 1, 0) };
    scaleTimer.Tick += ScaleTimer_Tick;
    scaleTimer.Start();
}

private void ScaleTimer_Tick(object? sender, EventArgs e)
{
    _diskScaleDeltay = _diskScaleDeltay <= plan.Height ? 
                       _diskScaleDeltay + Delta : Delta;
    _lambdaFactor = _diskScaleDeltay <= plan.Height ? _lambdaFactor + 0.5 : 1;
    Predicate<Point> scaledDisk = _diskScaleDeltay <= plan.Height
                                        ? _disk2.ScaleSet(0, _diskScaleDeltay, 
                                          _lambdaFactor, 1)
                                        : _disk2;
    scaledDisk.Draw(PlaneCanvas);
}
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/ScaleDemo.gif?raw=true)

#### Rotate

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotionFunction.png?raw=true)

Let `Rotation` be the function that rotates a point with an angle **θ**. In matrix algebra, `Rotation` is formulated as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotionFormula1.png?raw=true)

where _(x', y')_ are the co-ordinates of the point after rotation, and the formula for _x'_ and _y'_ is as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotionFormula2.png?raw=true)

The demonstration of this formula is very simple. Have a look at this rotation.

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotationDemonstration1.png?raw=true)

Below the demonstration:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotationDemonstration2.png?raw=true)

Thus the implementation in C# is as follows:

```csharp
private static Func<Point, Point> Rotate(double theta)
  => p => new Point(p.X * Math.Cos(theta) - p.Y * Math.Sin(theta),
                    p.X * Math.Sin(theta) + p.Y * Math.Cos(theta));
```

Let `RotateSet` be the function that applies a rotation on a set in the plane with the angle **θ**. This function is simply implemented as follow in C#.

```csharp
public static Predicate<Point> RotateSet(this Predicate<Point> set, double theta)
  => p => set(Rotate(-theta)(p));
```

`RotateSet` is an extension function on a set. It takes as parameter `theta` which is the angle of the rotation. If a point _P (x, y)_ is transformed through `RotateSet` in a set _S_, then its coordinates will change to _(x', y') = (x * cos(_θ_) - y * sin(_θ_), x * cos(_θ_) + y * sin(_θ_))_. Thus, the point _(x' * cos(_θ_) + y' * sin(_θ_), x' * cos(_θ_) - y' * sin(_θ_))_ will always belong to the set _S_. In algebra of sets, `RotateSet` is called isomorph, in other words, the set of all rotations forms the _Rotation group R_, which is isomorphic to the space itself. This explains the main logic of the function.

Thus, running the code below in our WPF application:

```csharp
RotateHalfPlaneAnimation();
```

where `RotateHalfPlaneAnimation` is described below:

```csharp
private double _theta;
private const double TWO_PI = 2 * Math.PI;
private const double HALF_PI = Math.PI / 2;
private readonly Predicate<Point> _halfPlane = Plane.VerticalHalfPlane(220, false);

private void RotateHalfPlaneAnimation()
{
    DispatcherTimer rotateTimer = 
          new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1, 0) };
    rotateTimer.Tick += RotateTimer_Tick;
    rotateTimer.Start();
}

private void RotateTimer_Tick(object? sender, EventArgs e)
{
    _halfPlane.RotateSet(_theta).Draw(PlaneCanvas);
    _theta += HALF_PI;
    _theta %= TWO_PI;
}
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/RotationDemo.gif?raw=true)

### <a id="plane-further" name="plane-further">For Those Who Want to Go Further</a>

Very simple, isn't it? For those who want to go further, you can explore these:

*   Ellipse
*   Three-dimensional Euclidean space
*   Ellipsoid
*   Paraboloid
*   Hyperboloid
*   Spherical harmonics
*   Superellipsoid
*   Haumea
*   Homoeoid
*   Focaloid

## <a id="fractals" name="fractals">Fractals</a>

![Image](https://github.com/aelassas/functional-cs/blob/main/img/Mandelbrot_zoom.gif?raw=true)

Fractals are sets that have a fractal dimension that usually exceeds their topological dimension and may fall between the integers. For example, the _Mandelbrot_ set is a fractal defined by a family of complex quadratic polynomials:

```
Pc(z) = z^2 + c
```

where `c` is a complex. The _Mandelbrot_ fractal is defined as the set of all points `c` such that the above sequence does not escape to infinity. In algebra of sets, this is formulated as follows:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/MandlebrotRelation.PNG?raw=true)

A Mandelbrot set is illustrated above.

Fractals (abstract data type) can always be represented as follows in C#:

```csharp
Func<Complex, Complex> fractal;
```

### <a id="complex-draw" name="complex-draw">Complex Numbers and Drawing</a>

In order to be able to draw fractals, I needed to manipulate _Complex_ numbers. Thus, I've used `Meta.numerics` library. I also needed an utility to draw complex numbers in a `Bitmap`, thus I used `ColorMap` and `ClorTriplet` classes that are available in this CodeProject [article](http://www.codeproject.com/Articles/80641/Visualizing-Complex-Functions).

### <a id="mandelbrot-fractal" name="mandelbrot-fractal">Mandelbrot Fractal</a>

I've created a _Mandelbrot_ (abstract data type representation) `P(z) = z^2 + c` that is available below.

```csharp
public static Func<Complex, Complex, Complex> MandelbrotFractal() => (c, z) => z * z + c;
```

In order to be able to draw _Complex_ numbers, I needed to update the `Draw` function. Thus, I created an overload of the `Draw` function that uses `ColorMap` and `ClorTriplet` classes. Below is the implementation in C#.

```csharp
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

            if (Double.IsInfinity(fz.Re) || Double.IsNaN(fz.Re) || 
                                            Double.IsInfinity(fz.Im) ||
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
```

Thus, running the code below:

```csharp
Plane.MandelbrotFractal().Draw(PlaneCanvas, 20, 1.5);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/mandelbrol.png?raw=true)

### <a id="newton-fractal" name="newton-fractal">Newton Fractal</a>

I've also created a _Newton Fractal_ (abstract data type representation) `P(z) = z^3 - 2*z + 2` that is available below:

```csharp
public static Func<Complex, Complex> NewtonFractal() => z => z * z * z - 2 * z + 2;
```

Thus, running the code below:

```csharp
Plane.NewtonFractal().Draw(PlaneCanvas);
```

gives the following result:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/NewtonFractalDemo.PNG?raw=true)

### <a id="fractal-further" name="fractal-further">For Those Who Want to Go Further</a>

For those who want to go further, you can explore these:

*   Mandelbrot Fractals
*   Julia Fractals
*   Other Newton Fractals
*   Other Fractals

## <a id="laziness" name="laziness">Introduction to Laziness</a>

In this section, we will see how to make a type _Lazy_.

_Lazy evaluation_ is an evaluation strategy which delays the evaluation of an expression until its value is needed and which also avoids repeated evaluations. The sharing can reduce the running time of certain functions by an exponential factor over other non-strict evaluation strategies, such as call-by-name. Below the benefits of Lazy evaluation.

*   Performance increases by avoiding needless calculations, and error conditions in evaluating compound expressions
*   The ability to construct potentially infinite data structure: We can easily create an infinite set of integers, for example, through a function (see the example on prime numbers in the [Sets](#sets) section)
*   The ability to define control flow (structures) as abstractions instead of primitives

Let's have a look at the code below:

```csharp
public class MyLazy<T>
{
    #region Fields

    private readonly Func<T> _f;
    private bool _hasValue;
    private T? _value;

    #endregion

    #region Constructors

    public MyLazy(Func<T> f)
    {
        _f = f;
    }

    #endregion

    #region Operators

    //
    // Use objects of type MyLazy<T> as objects of type T 
    // through implicit keyword
    //
    public static implicit operator T?(MyLazy<T?> lazy)
    {
        if (!lazy._hasValue)
        {
            lazy._value = lazy._f();
            lazy._hasValue = true;
        }

        return lazy._value;
    }

    #endregion
}
```

`MyLazy<T>` is a generic class that contains the following fields:

*   `_f`: A function for _lazy_ evaluation that returns a value of type `T`
*   `_value`: A value of type `T` _(frozen value)_
*   `_hasValue`: A boolean that indicates whether the value has been calculated or not

In order to use objects of type `MyLazy<T>` as objects of type `T`, the `implicit` keyword is used. The evaluation is done at type casting time, this operation is called _thaw_.

Thus, running the code below:

```csharp
var myLazyRandom = new MyLazy<double>(GetRandomNumber);
double myRandomX = myLazyRandom;
Console.WriteLine("\n Random with MyLazy<double>: {0}", myRandomX);
```

where `GetRandomNumber` returns a random `double` as follows:

```csharp
static double GetRandomNumber() => new Random().NextDouble();
```

gives the following output:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/LazyDemo1.PNG?raw=true)

The .NET Framework 4 introduced a class `System.Lazy<T>` for lazy evaluation. This class returns the value through the property `Value`. Running the code below:

```csharp
var lazyRandom = new Lazy<double>(GetRandomNumber);
double randomX = lazyRandom;
```

gives a compilation error because the type `Lazy<T>` is different from the type `double`.

To work with the value of the class `System.Lazy<T>`, the property `Value` has to be used as follows:

```csharp
var lazyRandom = new Lazy<double>(GetRandomNumber);
double randomX = lazyRandom.Value;
Console.WriteLine("\n Random with System.Lazy<double>.Value: {0}", randomX);
```

which gives the following output:

![Image](https://github.com/aelassas/functional-cs/blob/main/img/LazyDemo2.PNG?raw=true)

The .NET Framework 4 also introduced `ThreadLocal` and `LazyInitializer` for Lazy evaluation.

## <a id="unit-tests" name="unit-tests">Unit Tests</a>

Below are the unit tests for sets of numbers using xUnit.

```csharp
using Functional.Core;

namespace Functional.UnitTests;

public class SetUnitTest
{
    [Fact]
    public void TestEmptySet()
    {
        Assert.False(Set.Empty<int>()(7));
    }

    [Fact]
    public void TestSetAll()
    {
        Assert.True(Set.All<int>()(7));
    }

    [Fact]
    public void TestSingleton()
    {
        Assert.False(Set.Singleton(0)(7));
        Assert.True(Set.Singleton(7)(7));
    }

    [Fact]
    public void TestEvenNumbers()
    {
        Assert.False(Set.Even(99));
        Assert.True(Set.Even(998));
    }

    [Fact]
    public void TestOddNumbers()
    {
        Assert.True(Set.Odd(99));
        Assert.False(Set.Odd(998));
    }

    [Fact]
    public void TestMultiplesOfThree()
    {
        Assert.True(Set.MultipleOfThree(99));
        Assert.False(Set.MultipleOfThree(998));
    }

    [Fact]
    public void TestMultiplesOfFive()
    {
        Assert.True(Set.MultipleOfThree(15));
        Assert.False(Set.MultipleOfThree(998));
    }

    [Fact]
    public void TestPrimes()
    {
        Assert.False(Set.Prime(0));
        Assert.True(Set.Prime(2));
        Assert.False(Set.Prime(4));
        Assert.Equal(104743, Set.Primes(Set.Prime).Skip(10000).First());
    }

    [Fact]
    public void TestUnion()
    {
        Assert.True(Set.Even.Union(Set.Odd)(7));
    }

    [Fact]
    public void TestIntersection()
    {
        Predicate<int> multiplesOfThreeAndFive = Set.MultipleOfThree.Intersection(Set.MultipleOfFive);
        Assert.True(multiplesOfThreeAndFive(15));
        Assert.False(multiplesOfThreeAndFive(10));
    }

    [Fact]
    public void TestCartesianProduct()
    {
        Func<int, int, bool> cartesianProduct = Set.MultipleOfThree.CartesianProduct(Set.MultipleOfFive);
        Assert.True(cartesianProduct(9, 15));
        Assert.False(cartesianProduct(10, 15));
    }

    [Fact]
    public void TestComplement()
    {
        Assert.False(Set.MultipleOfThree.Complement(Set.MultipleOfFive)(15));
        Assert.True(Set.MultipleOfThree.Complement(Set.MultipleOfFive)(9));
    }

    [Fact]
    public void TestSymmetricDifferenceWithoutXor()
    {
        Predicate<int> sdWithoutXor = Set.Prime.SymmetricDifferenceWithoutXor(Set.Even);
        Assert.False(sdWithoutXor(2));
        Assert.True(sdWithoutXor(4));
        Assert.True(sdWithoutXor(7));
    }

    [Fact]
    public void TestSymmetricDifferenceWithXor()
    {
        Predicate<int> sdWithXor = Set.Prime.SymmetricDifferenceWithXor(Set.Even);
        Assert.False(sdWithXor(2));
        Assert.True(sdWithXor(4));
        Assert.True(sdWithXor(7));
    }

    [Fact]
    public void TestContains()
    {
        Assert.False(Set.Singleton(0).Contains(7));
        Assert.True(Set.Singleton(7).Contains(7));
    }

    [Fact]
    public void TestAdd()
    {
        Assert.True(Set.Singleton(0).Add(7)(7));
        Assert.True(Set.Singleton(1).Add(0)(0));
        Assert.False(Set.Singleton(19).Add(0)(7));
    }

    [Fact]
    public void TestRemove()
    {
        Assert.False(Set.Singleton(0).Remove(0)(7));
        Assert.False(Set.Singleton(7).Remove(7)(0));
        Assert.False(Set.All<int>().Remove(0)(0));
        Assert.True(Set.All<int>().Remove(0)(7));
    }
}
```

Below are unit tests for lazy evaluation.

```csharp
using Functional.Core;

namespace Functional.UnitTests;

public class LazyUnitTest
{
    static double GetRandomNumber() => new Random().NextDouble();

    [Fact]
    public void TestMyLazy()
    {
        var myLazyRandom = new MyLazy<double>(GetRandomNumber);
        double myRandomX = myLazyRandom; // implicit cast
        Assert.NotNull(myLazyRandom);
        Assert.Equal(myRandomX, myLazyRandom);
    }
}
```

After running the unit tests with the following commands:
```
cd functional-cs/tests/Functional.UnitTests
dotnet test --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

We reach 100% of code coverage. You can generate the coverage report with the following command after running the unit tests:
```
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"./coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

The coverage report is written in `./coveragereport` folder.

That's it! I hope you enjoyed reading.
