// @Author: Akram El Assas
// @License: CPOL

namespace Functional.Core;

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
