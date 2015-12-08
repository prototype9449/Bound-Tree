using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    public static class ConnectionSignHelper
    {
        public const string StrictConnectionSign = "+";
        public const string RelativeConnectionSign = "*";
        public const string NoneConnectionSign = "-";

        public static string GetConnectionSigh(ConnectionKind connectionKind)
        {
            Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

            if (connectionKind == ConnectionKind.None)
                return NoneConnectionSign;

            return connectionKind == ConnectionKind.Strict ? StrictConnectionSign : RelativeConnectionSign;
        }
    }
}