using System.Collections.Generic;

namespace Source
{
    public class SkillsGraph : Dictionary<int, SkillsGraph.SkillGraphInfo>
    {
        public ConnectionsGraph<T> ToConnectionsGraph<T>(T defaultValue = default) where T : struct
        {
            var result = new ConnectionsGraph<T>();
            foreach (var node in this)
            {
                result.Add(node.Key, new Dictionary<int, T>());
                foreach (var connectedNode in node.Value.ConnectedSkillsId)
                    result[node.Key].Add(connectedNode, defaultValue);
            }

            return result;
        }
        
        public class SkillGraphInfo
        {
            public ISkill Info;
            public SkillStatus Status;
            public HashSet<int> ConnectedSkillsId = new HashSet<int>();
        }
    }
}