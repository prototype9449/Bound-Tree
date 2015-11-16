using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundTree.Logic.ConnectionKinds
{
    public class StrictConnection : Connection
    {
        public override ConnectionKind ConnectionKind 
        { 
            get { return ConnectionKind.Strict; } 
        }

        public override string GetSign(bool isLeft)
        {
            return isLeft ? "<<" : ">>";
        }
    }
}
