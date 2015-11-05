using System;

namespace BoundTree.NodeInfo.Answers
{
     [Serializable]
    public class OpenTextInfo : INodeInfo
    {
         public OpenTextInfo()
         {
             LogicLevel = 3;
             Type = "OpenText";
         }

         public int LogicLevel { get; private set; }
         public string Type { get; private set; }
    }
}
