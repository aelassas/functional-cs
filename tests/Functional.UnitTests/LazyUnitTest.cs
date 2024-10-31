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
