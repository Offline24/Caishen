using System;
using Caishen.Internals;

namespace Caishen
{
    public class CaishenException : Exception
    {
        internal CaishenExceptionReason Reason { get; }

        public static CaishenException ProxyFactoryNonInterface()
        {
            return new CaishenException(
                CaishenExceptionReason.CannotCreateProxyForNonInterface,
                "TODO");
        }

        public static CaishenException UnderlyingObjectIsNull()
        {
            return new CaishenException(
                CaishenExceptionReason.UnderlyingObjectIsNull,
                "TODO");
        }

        private CaishenException(CaishenExceptionReason reason, string message)
            : base(message)
        {
            Reason = reason;
        }
    }
}