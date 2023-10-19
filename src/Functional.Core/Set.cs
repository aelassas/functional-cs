// @Author: Akram El Assas
// @License: CPOL

namespace Functional.Core;

public static class Set
{
    #region Fundomantal sets

    // Empty Set
    public static Predicate<T> Empty<T>() => _ => false;

    // Set All
    public static Predicate<T> All<T>() => _ => true;

    // Singleton set
    public static Predicate<T> Singleton<T>(T e) where T : notnull => x => e.Equals(x);

    #endregion

    #region Primes

    // Primes
    internal static IEnumerable<int> Primes(Predicate<int> prime)
    {
        yield return 2;

        int p = 3;
        while (true)
        {
            if (prime(p)) yield return p;
            p += 2;
        }
    }

    // Checks whether or not a given number is prime
    public static bool IsPrime(int i)
    {
        if (i <= 1) return false;
        if (i < 4) return true; // 2 and 3 are primes
        if (i % 2 == 0) return false; // multiples of 2 are not prime
        if (i < 9) return true; // 5 and 7 are primes
        if (i % 3 == 0) return false; // multiples of 3 are not primes

        // If a divisor less than or equal to sqrt(i) is found
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

    #endregion
}