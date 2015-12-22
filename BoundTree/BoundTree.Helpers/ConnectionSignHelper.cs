using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    public static class ConnectionSignHelper
    {
        public const char StrictConnectionSign = '+';
        public const char RelativeConnectionSign = '*';
        public const char NoneConnectionSign = '-';

        public static string GetConnectionSigh(ConnectionKind connectionKind)
        {
            Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

            if (connectionKind == ConnectionKind.None)
                return NoneConnectionSign.ToString();

            return connectionKind == ConnectionKind.Strict 
                ? StrictConnectionSign.ToString() 
                : RelativeConnectionSign.ToString();
        }
    }
}