using System.Linq.Expressions;

namespace Caishen.Internals.ArgCheckers
{
    internal interface IExpressionBasedArgCheckerFactory
    {
        IArgChecker CreateArgChecker(Expression expression);
    }
}