using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caishen.Internals.KeyCreators;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators
{
    public class KeyCreatorTests
    {
        [Fact]
        public void Default_Same()
        {
            // Given
            var sut = KeyCreator.Default;
            var method1 = MethodBase.GetCurrentMethod();
            var method2 = MethodBase.GetCurrentMethod();
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {1, "abc", new List<int> {2, 3}};
            
            // When
            var result1 = sut.CreateKey(method1, input1);
            var result2 = sut.CreateKey(method2, input2);
            
            // Then
            Assert.Equal(result1, result2);
        }
        
        [Fact]
        public void Default_DifferentOrder()
        {
            // Given
            var sut = KeyCreator.Default;
            var method1 = MethodBase.GetCurrentMethod();
            var method2 = MethodBase.GetCurrentMethod();
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {"abc", 1, new List<int> {2, 3}};
            
            // When
            var result2 = sut.CreateKey(method1, input2);
            var result1 = sut.CreateKey(method2, input1);

            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void Default_DifferentArguments()
        {
            // Given
            var sut = KeyCreator.Default;
            var method1 = MethodBase.GetCurrentMethod();
            var method2 = MethodBase.GetCurrentMethod();
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {1, "abc", new List<int> {1, 3}};
            
            // When
            var result1 = sut.CreateKey(method1, input1);
            var result2 = sut.CreateKey(method2, input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void Default_DifferentArguments2()
        {
            // Given
            var sut = KeyCreator.Default;
            var method1 = MethodBase.GetCurrentMethod();
            var method2 = MethodBase.GetCurrentMethod();
            var input1 = new object[] {"1"};
            var input2 = new object[] {1};
            
            // When
            var result1 = sut.CreateKey(method1, input1);
            var result2 = sut.CreateKey(method2, input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void Default_DifferentMethods()
        {
            MethodBase GetOtherMethodBase() => MethodBase.GetCurrentMethod();
            
            // Given
            var sut = KeyCreator.Default;
            var method1 = MethodBase.GetCurrentMethod();
            var method2 = GetOtherMethodBase();
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {1, "abc", new List<int> {2, 3}};
            
            // When
            var result1 = sut.CreateKey(method1, input1);
            var result2 = sut.CreateKey(method2, input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void Default_DifferentOverload()
        {
            MethodBase GetMethod(params Type[] types) =>
                typeof(TestClass).GetMethod(nameof(TestClass.TestMethod), types);   
            
            // Given
            var sut = KeyCreator.Default;

            var methodsWithArguments = new (MethodBase method, object[] arguments)[]
            {
                (GetMethod(), new object[] { }),
                (GetMethod(typeof(int)), new object[] { 1 }),
                (GetMethod(typeof(int), typeof(int)), new object[] { 1, 2 }),
                (GetMethod(typeof(string)), new object[] { "1" }),
            };
            
            // When
            var results = methodsWithArguments
                .Select(x => sut.CreateKey(x.method, x.arguments))
                .ToList();
            
            // Then
            Assert.Equal(4, results.Count);
            
            for (var i = 0; i < results.Count; i++)
            {
                for (var j = i+1; j < results.Count; j++)
                {
                    var key1 = results[i];
                    var key2 = results[j];
                    
                    Assert.NotEqual(key1, key2);
                }
            }
        }

        private class TestClass
        {
            public int TestMethod() => 0;
            public int TestMethod(int x) => x;
            public int TestMethod(int x, int y) => x + y;
            public int TestMethod(string x) => x.Length;
        }
    }
}