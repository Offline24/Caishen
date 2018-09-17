using System;
using System.Linq.Expressions;

namespace Caishen
{
    public static class Arg
    {
        public static T Any<T>() => default(T);
        public static T Match<T>(Expression<Func<T, bool>> expression) => default(T);
    }
}