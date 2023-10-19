// @Author: Akram El Assas
// @License: CPOL

namespace Functional.Core;

public static class ConsoleHelper
{
    #region Shell

    public static void Shell()
    {
        Console.WriteLine("\nRepresenting data through functions");

        // Empty set
        Console.WriteLine("\nEmpty set:");
        Console.WriteLine("Is 7 in {{}}? {0}", Set.Empty<int>()(7));

        // All set
        Console.WriteLine("\nAll set:");
        Console.WriteLine("Is 7 in the integers set? {0}", Set.All<int>()(7));

        // Singleton set
        Console.WriteLine("\nSingleton set:");
        Console.WriteLine("Is 7 in the singleton {{0}}? {0}", Set.Singleton(0)(7));
        Console.WriteLine("Is 7 in the singleton {{7}}? {0}", Set.Singleton(7)(7));

        // Other sets
        Console.WriteLine("\nOther sets:");
        Predicate<int> even = i => i % 2 == 0;
        Console.WriteLine("Is {0} even? {1}", 99, even(99));
        Console.WriteLine("Is {0} even? {1}", 998, even(998));

        Predicate<int> odd = i => i % 2 == 1;
        Console.WriteLine("Is {0} odd? {1}", 99, odd(99));
        Console.WriteLine("Is {0} odd? {1}", 998, odd(998));

        Predicate<int> multipleOfThree = i => i % 3 == 0;
        Console.WriteLine("Is {0} a multiple of 3? {1}", 99, multipleOfThree(99));
        Console.WriteLine("Is {0} a multiple of 3? {1}", 998, multipleOfThree(998));

        Predicate<int> multipleOfFive = i => i % 5 == 0;
        Console.WriteLine("Is {0} a multiple of 5? {1}", 15, multipleOfFive(15));
        Console.WriteLine("Is {0} a multiple of 5? {1}", 998, multipleOfFive(998));

        Predicate<int> prime = Set.IsPrime;
        int p = Set.Primes(prime).Skip(10000).First();
        Console.WriteLine("The 10 001st prime number is {0}", p);

        // Union
        Console.WriteLine("\nUnion:");
        Console.WriteLine("Is 7 in the union of Even and Odd Integers Set? {0}", even.Union(odd)(7));

        // Intersection
        Console.WriteLine("\nIntersection:");
        Predicate<int> multiplesOfThreeAndFive = multipleOfThree.Intersection(multipleOfFive);
        Console.WriteLine("Is 15 a multiple of 3 and 5? {0}", multiplesOfThreeAndFive(15));
        Console.WriteLine("Is 10 a multiple of 3 and 5? {0}", multiplesOfThreeAndFive(10));

        // Cartesian Product
        Console.WriteLine("\nCartesian Product:");
        Func<int, int, bool> cartesianProduct = multipleOfThree.CartesianProduct(multipleOfFive);
        Console.WriteLine("Is (9, 15) in MultipleOfThree x MultipleOfFive? {0}", cartesianProduct(9, 15));

        // Complements
        Console.WriteLine("\nComplement:");
        Console.WriteLine("Is 15 in MultipleOfThree \\ MultipleOfFive set? {0}",
            multipleOfThree.Complement(multipleOfFive)(15));
        Console.WriteLine("Is 9 in MultipleOfThree \\ MultipleOfFive set? {0}",
            multipleOfThree.Complement(multipleOfFive)(9));

        // SymmetricDifference without XOR
        Console.WriteLine("\nSymmetricDifference without XOR:");
        Predicate<int> sdWithoutXor = prime.SymmetricDifferenceWithoutXor(even);
        Console.WriteLine("Is 2 in the symetric difference of prime and even Sets? {0}", sdWithoutXor(2));
        Console.WriteLine("Is 4 in the symetric difference of prime and even Sets? {0}", sdWithoutXor(4));
        Console.WriteLine("Is 7 in the symetric difference of prime and even Sets? {0}", sdWithoutXor(7));

        // SymmetricDifference with XOR
        Console.WriteLine("\nSymmetricDifference with XOR:");
        Predicate<int> sdWithXor = prime.SymmetricDifferenceWithXor(even);
        Console.WriteLine("Is 2 in the symetric difference of prime and even Sets? {0}", sdWithXor(2));
        Console.WriteLine("Is 4 in the symetric difference of prime and even Sets? {0}", sdWithXor(4));
        Console.WriteLine("Is 7 in the symetric difference of prime and even Sets? {0}", sdWithXor(7));

        // Contains
        Console.WriteLine("\nContains:");
        Console.WriteLine("Is 7 in the singleton {{0}}? {0}", Set.Singleton(0).Contains(7));
        Console.WriteLine("Is 7 in the singleton {{7}}? {0}", Set.Singleton(7).Contains(7));

        // Add
        Console.WriteLine("\nAdd:");
        Console.WriteLine("Is 7 in {{0, 7}}? {0}", Set.Singleton(0).Add(7)(7));
        Console.WriteLine("Is 0 in {{1, 0}}? {0}", Set.Singleton(1).Add(0)(0));
        Console.WriteLine("Is 7 in {{19, 0}}? {0}", Set.Singleton(19).Add(0)(7));

        // Remove
        Console.WriteLine("\nRemove:");
        Console.WriteLine("Is 7 in {{}}? {0}", Set.Singleton(0).Remove(0)(7));
        Console.WriteLine("Is 0 in {{}}? {0}", Set.Singleton(7).Remove(7)(0));

        Console.ReadKey(false);
    }

    #endregion
}