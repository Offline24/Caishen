using System;
using System.Linq.Expressions;

namespace Caishen.Internals.ArgCheckers
{
    internal class MatchArgChecker<TArgument> : IArgChecker
    {
        private readonly Func<TArgument, bool> _checkMethod;

        private readonly Type _argumentType;
        private readonly Type _underlyingNullableType;

        public MatchArgChecker(Expression<Func<TArgument, bool>> matchExpression)
        {
            _checkMethod = matchExpression.Compile();
            
            _argumentType = typeof(TArgument);
            
            if (_argumentType.IsGenericType && _argumentType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                _underlyingNullableType = _argumentType.GetGenericArguments()[0];
            }
            else
            {
                _underlyingNullableType = null;
            }
        }

        public bool Match(object argument)
        {
            if (argument == null)
            {
                if (!_argumentType.IsValueType || _underlyingNullableType != null)
                    return _checkMethod(default(TArgument));

                return false;
            }

            var argumentType = argument.GetType();
            
            if (argumentType == _argumentType || argumentType == _underlyingNullableType)
            {
                return _checkMethod((TArgument)argument);
            }

            return false;
        }
    }
}