﻿using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;

namespace BoundTree
{
    abstract public class Node
    {
        private BindingHelper _bindingHelper = new BindingHelper();
        private readonly IBindingHandler _bindingHandler;
        public Identificator Identificator { get; private set; }
        public List<Node> Nodes { get; internal set; }

        public IBindingHandler BindingHandler
        {
            get { return _bindingHandler; }
        }

        protected Node(Identificator identificator, IBindingHandler bindingHandler)
        {
            _bindingHandler = bindingHandler;
            Identificator = identificator;
            Nodes = new List<Node>();
        }

        protected Node(Node node, IBindingHandler bindingHandler)
        {
            Identificator = node.Identificator;
            _bindingHandler = bindingHandler;
            Nodes = new List<Node>();
        }

        public bool BindWith(Node otherNode)
        {
            if (_bindingHelper.Bind(this, otherNode))
            {
                BindingHandler.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }

        public bool Add(Node otherNode)
        {
            if (this.Identificator.NeedToPutInside(otherNode.Identificator))
            {
                Nodes.Add(otherNode);
                return true;
            }
            foreach (var node in Nodes)
            {
                if (node.Identificator.NeedToInsert(otherNode.Identificator))
                {
                    return node.Add(otherNode);
                }
            }
            return false;

        }

        public Node GetNodeById(Identificator identificator)
        {
            if (identificator == this.Identificator)
                return this;

            foreach (var node in Nodes)
            {
                if (node.Identificator.NeedToInsert(identificator))
                {
                    return node.GetNodeById(identificator);
                }
                if (node.Identificator == identificator) 
                    return node;
            }

            return null;
        }

        public Node GetNewInstanceById(Identificator identificator)
        {
            var node = GetNodeById(identificator);
            return node.GetNewInstance();
        }

        public abstract Node GetNewInstance();
    }
}
