using System;
using System.Linq.Expressions;
using Caishen.Internals;
using Caishen.Internals.ArgCheckers;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.ArgCheckers
{
    public class ExpressionBasedArgCheckerFactoryTests
    {
        private readonly ExpressionBasedArgCheckerFactory _sut = new ExpressionBasedArgCheckerFactory();
        
        [Fact]
        public void CreateAnyArgChecker()
        {
            // Given
            Expression<Func<TestClass, int>> typedExpression = x => x.GetNextInt(Arg.Any<int>());
            Expression argumentExpression = GetFirstBodyParameterExpression(typedExpression);
            
            // When
            var argChecker = _sut.CreateArgChecker(argumentExpression);
            
            // Then
            Assert.NotNull(argChecker);
            Assert.IsType<AnyArgChecker>(argChecker);
            
            Assert.True(argChecker.Match(null));
            Assert.True(argChecker.Match(11));
            Assert.True(argChecker.Match("s"));
        }
        
        [Fact]
        public void CreateItArgCheckerForValueType()
        {
            // Given
            Expression<Func<TestClass, int>> typedExpression = x => x.GetNextInt(10);
            Expression argumentExpression = GetFirstBodyParameterExpression(typedExpression);
            
            // When
            var argChecker = _sut.CreateArgChecker(argumentExpression);
            
            // Then
            Assert.NotNull(argChecker);
            Assert.IsType<ItArgChecker>(argChecker);
            
            Assert.True(argChecker.Match(10));
            Assert.False(argChecker.Match(11));
        }
        
        [Fact]
        public void CreateItArgCheckerForReferenceType()
        {
            // Given
            Expression<Func<string, bool>> typedExpression = x => x.Contains("str");
            Expression argumentExpression = GetFirstBodyParameterExpression(typedExpression);
            
            // When
            var argChecker = _sut.CreateArgChecker(argumentExpression);
            
            // Then
            Assert.NotNull(argChecker);
            Assert.IsType<ItArgChecker>(argChecker);
            
            Assert.True(argChecker.Match("str"));
            Assert.False(argChecker.Match("oth"));
        }
        
        [Fact]
        public void CreateMatchArgChecker()
        {
            // Given
            Expression<Func<TestClass, int>> typedExpression = x => x.GetNextInt(Arg.Match<int>(y => y > 10));
            Expression argumentExpression = GetFirstBodyParameterExpression(typedExpression);
            
            // When
            var argChecker = _sut.CreateArgChecker(argumentExpression);
            
            // Then
            Assert.NotNull(argChecker);
            Assert.IsType<MatchArgChecker<int>>(argChecker);
            
            Assert.True(argChecker.Match(11));
            Assert.False(argChecker.Match(9));
        }
        
        [Fact]
        public void ShouldThrowExceptionForInvalidExpressionType()
        {
            // Given
            Expression binaryExpression =
                Expression.MakeBinary(
                    ExpressionType.Subtract,
                    Expression.Constant(53),
                    Expression.Constant(14));
            
            // When
            IArgChecker CreateArgCheckerAction() => _sut.CreateArgChecker(binaryExpression);

            // Then
            var exception = Assert.Throws<CaishenException>((Func<IArgChecker>) CreateArgCheckerAction);
            Assert.Equal(CaishenExceptionReason.InvalidArgumentExpression, exception.Reason);
        }
        
        [Fact]
        public void ShouldThrowExceptionForInvalidMethodCall()
        {
            // Given
            Expression<Func<TestClass, int>> typedExpression = x => x.GetNextInt( TestMethodThatGenerateInt() );
            Expression argumentExpression = GetFirstBodyParameterExpression(typedExpression);
            
            // When
            IArgChecker CreateArgCheckerAction() => _sut.CreateArgChecker(argumentExpression);

            // Then
            var exception = Assert.Throws<CaishenException>((Func<IArgChecker>) CreateArgCheckerAction);
            Assert.Equal(CaishenExceptionReason.InvalidArgumentExpression, exception.Reason);
        }

        private static Expression GetFirstBodyParameterExpression<TInput, TReturn>(
            Expression<Func<TInput, TReturn>> expression)
        {
            return ((IArgumentProvider) expression.Body).GetArgument(0);
        }

        private static int TestMethodThatGenerateInt() => 1;
    }
}