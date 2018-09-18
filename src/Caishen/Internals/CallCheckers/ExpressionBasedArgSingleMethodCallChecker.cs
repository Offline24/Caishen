using System;
using System.Linq.Expressions;
using Caishen.Internals.ArgCheckers;

namespace Caishen.Internals.CallCheckers
{
    internal class ExpressionBasedArgSingleMethodCallChecker<TInterface, TValue> : ISingleMethodCallChecker
    {
        private readonly int _argumentCount;
        private readonly IArgChecker[] _argCheckers;
        
        public ExpressionBasedArgSingleMethodCallChecker(
            IExpressionBasedArgCheckerFactory expressionBasedArgCheckerFactory,
            Expression<Func<TInterface, TValue>> expression)
        {
            var funcArgumentProvider = (IArgumentProvider)expression.Body;
            
            _argumentCount = funcArgumentProvider.ArgumentCount;

            _argCheckers = new IArgChecker[_argumentCount];
            for (var i = 0; i < _argumentCount; i++)
            {
                var argument = funcArgumentProvider.GetArgument(i);
                var argChecker = expressionBasedArgCheckerFactory.CreateArgChecker(argument);
                _argCheckers[i] = argChecker;
            }
        }
        
        public bool DoesCallMatch(object[] arguments)
        {
            if (arguments.Length != _argumentCount)
            {
                return false;
            }

            for (var i = 0; i < _argumentCount; i++)
            {
                if (!_argCheckers[i].Match(arguments[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}