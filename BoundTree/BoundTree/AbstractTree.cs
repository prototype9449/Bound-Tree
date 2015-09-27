using System;
using System.Collections;
using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree
{
    public abstract class AbstractTree
    {
        private Dictionary<Identificator,Identificator> _boundNodes = new Dictionary<Identificator, Identificator>();

        public INode Root { get; set; }

        protected AbstractTree(INode root)
        {
            Root = root;
        }

        public void Add(INode node)
        {
            Root.Add(node);
        }
        public INode GetByIdentificator(Identificator identificator)
        {
            return Root.GetNodeByIdentificator(identificator);
        }


        public void HandleBinding(INode mainNode, INode minorNode)
        {
            _boundNodes.Add(mainNode.Identificator, minorNode.Identificator);
        }
    }
}
