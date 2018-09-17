using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Caishen.Internals.ArgCheckers
{
    internal class ExpressionBasedArgCheckerFactory : IExpressionBasedArgCheckerFactory
    {
        private static readonly Type ArgClassType;
        private static readonly MethodInfo ArgAnyMethodInfo;
        private static readonly MethodInfo ArgMatchMethodInfo;
        private static readonly Type GenericMatchArgCheckerType;
        
        public IArgChecker CreateArgChecker(Expression expression)
        {
            switch (expression)
            {
                case ConstantExpression constantExpression:
                    return CreateFromConstantExpression(constantExpression);
                case MethodCallExpression methodCallExpression:
                    return CreateFromMethodCallExpression(methodCallExpression);
                default:
                    throw CaishenException.InvalidArgumentExpression();
            }
        }

        static ExpressionBasedArgCheckerFactory()
        {
            ArgClassType = typeof(Arg);
            ArgAnyMethodInfo = ArgClassType.GetMethod(nameof(Arg.Any));
            ArgMatchMethodInfo = ArgClassType.GetMethod(nameof(Arg.Match));
            GenericMatchArgCheckerType = typeof(MatchArgChecker<>);
        }

        private static IArgChecker CreateFromMethodCallExpression(MethodCallExpression expression)
        {
            if (expression.Method.IsGenericMethod && expression.Method.DeclaringType == ArgClassType)
            {
                var expressionGenericMethodDefinition = expression.Method.GetGenericMethodDefinition();

                if (expressionGenericMethodDefinition == ArgAnyMethodInfo)
                {
                    return new AnyArgChecker();
                }

                if (expressionGenericMethodDefinition == ArgMatchMethodInfo)
                {
                    var genericParameters = expression.Method.GetGenericArguments();
                    var typedMatchArgCheckerType = GenericMatchArgCheckerType.MakeGenericType(genericParameters);

                    var constructorArgument = ((UnaryExpression)expression.Arguments[0]).Operand;
                    var constructorArguments = new object[] { constructorArgument };
                    var matchArgChecker = (IArgChecker)Activator.CreateInstance(
                        typedMatchArgCheckerType,
                        constructorArguments);
                    
                    return matchArgChecker;
                }
            }
            
            throw CaishenException.InvalidArgumentExpression();
        }

        private static IArgChecker CreateFromConstantExpression(ConstantExpression expression)
        {
            return new ItArgChecker(expression.Value);
        }
    }
}