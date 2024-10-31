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