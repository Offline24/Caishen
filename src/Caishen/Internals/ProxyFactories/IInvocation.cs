using System;
using System.Reflection;

namespace Caishen.Internals.ProxyFactories
{
    public interface IInvocation
    {
        object[] Arguments { get; }
        Type[] GenericArguments { get; }
        object InvocationTarget { get; }
        MethodInfo Method { get; }
        MethodInfo MethodInvocationTarget { get; }
        object Proxy { get; }
        object ReturnValue { get; set; }
        Type TargetType { get; }
        object GetArgumentValue(int index);
        MethodInfo GetConcreteMethod();
        MethodInfo GetConcreteMethodInvocationTarget();
        void Proceed();
        void SetArgumentValue(int index, object value);
    }
}