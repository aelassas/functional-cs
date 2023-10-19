// @Author: Akram El Assas
// @License: CPOL

namespace Functional.Core;

public static class BinaryOperation
{
    #region Extension methods on Predicate<T>

    /// <summary>
    /// Union of the set e and the set f
    /// </summary>
    public static Predicate<T> Union<T>(this Predicate<T> e, Predicate<T> f) => x => e(x) || f(x);

    /// <summary>
    /// Intersection of the set e and the set f
    /// </summary>
    public static Predicate<T> Intersection<T>(this Predicate<T> e, Predicate<T> f) => x => e(x) && f(x);

    /// <summary>
    /// Cartesian Product
    /// </summary>
    public static Func<T1, T2, bool> CartesianProduct<T1, T2>(this Predicate<T1> e, Predicate<T2> f) => (x, y) => e(x) && f(y);

    /// <summary>
    /// Complement of the set s
    /// </summary>
    public static Predicate<T> Complement<T>(this Predicate<T> e, Predicate<T> f) => x => e(x) && !f(x);

    /// <summary>
    /// Symmetric Difference of e and f
    /// </summary>
    public static Predicate<T> SymmetricDifferenceWithoutXor<T>(this Predicate<T> e, Predicate<T> f) => Union(e.Complement(f), f.Complement(e));

    /// <summary>
    /// Symmetric Difference of e and f
    /// </summary>
    public static Predicate<T> SymmetricDifferenceWithXor<T>(this Predicate<T> e, Predicate<T> f) => x => e(x) ^ f(x);

    /// <summary>
    /// Checks whether or not the set s containts x
    /// </summary>
    public static bool Contains<T>(this Predicate<T> e, T x) => e(x);

    /// <summary>
    /// Adds the element x to the set s
    /// </summary>
    public static Predicate<T> Add<T>(this Predicate<T> s, T e) where T : notnull => x => x.Equals(e) || s(x);

    /// <summary>
    /// Removes the element x from the set s
    /// </summary>
    public static Predicate<T> Remove<T>(this Predicate<T> s, T e) where T : notnull => x => !x.Equals(e) && s(x);

    #endregion
}