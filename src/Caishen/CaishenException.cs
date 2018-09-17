using System;
using Caishen.Internals;

namespace Caishen
{
    public class CaishenException : Exception
    {
        internal CaishenExceptionReason Reason { get; }

        internal static CaishenException ProxyFactoryNonInterface()
        {
            return new CaishenException(
                CaishenExceptionReason.CannotCreateProxyForNonInterface,
                "TODO");
        }

        internal static CaishenException UnderlyingObjectIsNull()
        {
            return new CaishenException(
                CaishenExceptionReason.UnderlyingObjectIsNull,
                "TODO");
        }
        
        internal static CaishenException InvalidArgumentExpression()
        {
            return new CaishenException(
                CaishenExceptionReason.InvalidArgumentExpression,
                "TODO");
        }

        private CaishenException(CaishenExceptionReason reason, string message)
            : base(message)
        {
            Reason = reason;
        }
    }
}