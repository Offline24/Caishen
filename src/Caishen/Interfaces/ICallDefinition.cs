using System;
using System.Reflection;

namespace Caishen.Interfaces
{
    public interface ICallDefinition
    {
        object[] Arguments { get; }
        Type[] GenericArguments { get; }
        MethodInfo Method { get; }
    }
}