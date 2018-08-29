using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Caishen.Internals.ProxyFactory
{
    internal class CastleCoreProxyFactory<TInterface> : IProxyFactory<TInterface> where TInterface : class
    {
        private readonly IProxyGenerator _proxyGenerator = new ProxyGenerator();
        
        public CastleCoreProxyFactory()
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw CaishenException.ProxyFactoryNonInterface();
            }
        }
        
        public TInterface CreateProxy(TInterface underlyingObject, params IInterceptor[] interceptors)
        {
            if (underlyingObject == null)
            {
                throw CaishenException.UnderlyingObjectIsNull();
            }
            
            var castleInterceptors = interceptors
                .Select(interceptor => new CastleInterceptor(interceptor) as Castle.DynamicProxy.IInterceptor)
                .ToArray();
            var proxyObject = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(underlyingObject, castleInterceptors);

            return proxyObject;
        }

        private class CastleInterceptor : Castle.DynamicProxy.IInterceptor
        {
            private readonly IInterceptor _interceptor;

            public CastleInterceptor(IInterceptor interceptor)
            {
                _interceptor = interceptor;
            }

            public void Intercept(Castle.DynamicProxy.IInvocation castleInvocation)
            {
                var invocation = TranslateInvocation(castleInvocation);
                _interceptor.Intercept(invocation);
            }

            private IInvocation TranslateInvocation(Castle.DynamicProxy.IInvocation castleInvocation)
            {
                return new Invocation(castleInvocation);
            }

            private class Invocation : IInvocation
            {
                private readonly Castle.DynamicProxy.IInvocation _castleInvocation;

                public Invocation(Castle.DynamicProxy.IInvocation castleInvocation)
                {
                    _castleInvocation = castleInvocation;
                }

                public object[] Arguments => _castleInvocation.Arguments;
                public Type[] GenericArguments => _castleInvocation.GenericArguments;
                public object InvocationTarget => _castleInvocation.InvocationTarget;
                public MethodInfo Method => _castleInvocation.Method;
                public MethodInfo MethodInvocationTarget => _castleInvocation.MethodInvocationTarget;
                public object Proxy => _castleInvocation.Proxy;
                public object ReturnValue
                {
                    get => _castleInvocation.ReturnValue;
                    set => _castleInvocation.ReturnValue = value;
                }
                public Type TargetType => _castleInvocation.TargetType;
                public object GetArgumentValue(int index) => _castleInvocation.GetArgumentValue(index);
                public MethodInfo GetConcreteMethod() => _castleInvocation.GetConcreteMethod();
                public MethodInfo GetConcreteMethodInvocationTarget() => _castleInvocation.GetConcreteMethodInvocationTarget();
                public void Proceed() => _castleInvocation.Proceed();
                public void SetArgumentValue(int index, object value) => _castleInvocation.SetArgumentValue(index, value);
            }
        }
    }
}