using System;
using System.Linq.Expressions;
using Caishen.Internals.ArgCheckers;
using Caishen.Internals.CallCheckers;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.CallCheckers
{
    public class ExpressionBasedArgSingleMethodCallCheckerTests
    {
        [Fact]
        public void DoesCallMatchWithArguments1()
        {
            // Given
            var sut = CreateSut(x => x.GetStringWithParameters(Arg.Match<int>(y => y > 5), Arg.Any<string>()));
            var matchingArguments = new object[] {6, ""};
            var notMatchingArguments = new object[] {4, ""};
            
            // When
            var matchingResult = sut.DoesCallMatch(matchingArguments);
            var notMatchingResult = sut.DoesCallMatch(notMatchingArguments);
            
            // Then
            Assert.True(matchingResult);
            Assert.False(notMatchingResult);
        }
        
        [Fact]
        public void DoesCallMatchWithArguments2()
        {
            // Given
            var sut = CreateSut(x => x.GetIntWithParameters(Arg.Any<int>(), Arg.Match<string>(y => y.Length > 5)));
            var matchingArguments = new object[] {6, "123456"};
            var notMatchingArguments = new object[] {4, "1234"};
            
            // When
            var matchingResult = sut.DoesCallMatch(matchingArguments);
            var notMatchingResult = sut.DoesCallMatch(notMatchingArguments);
            
            // Then
            Assert.True(matchingResult);
            Assert.False(notMatchingResult);
        }
        
        [Fact]
        public void DoesCallMatchWithoutArguments()
        {
            // Given
            var sut = CreateSut(x => x.GetIntNoParameters());
            var matchingArguments = new object[] { };
            var notMatchingArguments = new object[] { 1 };
            
            // When
            var matchingResult = sut.DoesCallMatch(matchingArguments);
            var notMatchingResult = sut.DoesCallMatch(notMatchingArguments);
            
            // Then
            Assert.True(matchingResult);
            Assert.False(notMatchingResult);
        }

        private ExpressionBasedArgSingleMethodCallChecker<ITestInterfaceWithFunctions, TValue> CreateSut<TValue>(
            Expression<Func<ITestInterfaceWithFunctions, TValue>> expression
            )
        {
            var defaultArgCheckerFactory = new ExpressionBasedArgCheckerFactory();

            var sut = new ExpressionBasedArgSingleMethodCallChecker<ITestInterfaceWithFunctions, TValue>(
                defaultArgCheckerFactory,
                expression);

            return sut;
        }
    }
}