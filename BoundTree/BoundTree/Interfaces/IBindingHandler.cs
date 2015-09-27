using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundTree.Interfaces
{
    public interface IBindingHandler
    {
        void HandleBinding(Node mainNode, Node minorNode);
    }
}
