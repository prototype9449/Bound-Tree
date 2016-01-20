using System;
using System.Collections.Generic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Logic.LogicLevelProviders
{
    [Serializable]
    public class BuildingTreeLogicLevelProvider : ILogicLevelProvider
    {
        private readonly Dictionary<Type, List<Type>> _validTypes;

        public BuildingTreeLogicLevelProvider()
        {
            _validTypes = new Dictionary<Type, List<Type>>
            {
                {
                    typeof (Root),
                    new List<Type>
                    {
                        typeof (Grid), typeof (Grid3D), typeof (Multi), typeof (OpenText), typeof (Single), typeof(MultiGrid)
                    }
                },
                {
                    typeof (Grid3D),
                    new List<Type>
                    {
                        typeof (Grid), typeof (MultiGrid), typeof (Multi), typeof (OpenText), typeof (Single)
                    }
                },
                {
                    typeof(Grid),
                    new List<Type>
                    {
                        typeof(Multi),typeof(OpenText), typeof(Single)
                    }
                },
                {
                    typeof(Multi),
                    new List<Type>
                    {
                        typeof(PredefinedList),typeof(OpenText),typeof(Answer), typeof(Single)
                    }
                },
                {
                    typeof(OpenText),
                    new List<Type>
                    {
                        typeof(PredefinedList), typeof(Answer)
                    }
                },
                {
                    typeof(PredefinedList),
                    new List<Type>
                    {
                        typeof(PredefinedList), typeof(Answer)
                    }
                },
                {
                    typeof(Single),
                    new List<Type>
                    {
                        typeof(PredefinedList), typeof(Answer)
                    }
                },
                {
                    typeof(Answer),
                    new List<Type>
                    {
                        typeof(PredefinedList)
                    }
                },
                {
                    typeof(MultiGrid),
                    new List<Type>
                    {
                        typeof (Grid),typeof(Multi),typeof(OpenText), typeof(Single)
                    }
                }
            };
        }


        public LogicLevel GetLogicLevel(NodeInfo nodeInfo)
        {
            throw new NotImplementedException();
        }

        public bool CanFirtsContainSecond(NodeInfo first, NodeInfo second)
        {
            if(!_validTypes.ContainsKey(first.GetType()))
                throw new TypeAccessException();

            var types = _validTypes[first.GetType()];
            return types.Contains(second.GetType());
        }
    }
}