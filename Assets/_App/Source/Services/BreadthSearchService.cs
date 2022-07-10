using System;
using System.Collections.Generic;

namespace Source
{
    public class BreadthSearchService : IGraphSearchService
    {
        public bool IsSkillLearnable(SkillsGraph skillsGraph, int skillId)
        {
            var skill = skillsGraph[skillId];
            if (skill.Status != SkillStatus.Learnable)
                return false;
            
            // В данной ситуации можно было просто использовать SkillStatus,
            // но по требованию тестовго задания дальше приведён алгоритм
            
            var checkedSkillsId = new HashSet<int> { skillId };
            var skillsIdQueue = new Queue<int>();
            skillsIdQueue.Enqueue(skillId);

            while (skillsIdQueue.Count != 0)
            {
                var currentSkillId = skillsIdQueue.Dequeue();
                var currentSkill = skillsGraph[currentSkillId];
                foreach (var connectedSkillId in currentSkill.ConnectedSkillsId)
                {
                    var connectedSkill = skillsGraph[connectedSkillId];
                    if (checkedSkillsId.Contains(connectedSkillId) ||
                        connectedSkill.Status != SkillStatus.Learned)
                        continue;
                
                    if (connectedSkill.Info is BaseSkill)
                        return true;

                    checkedSkillsId.Add(connectedSkillId);
                    skillsIdQueue.Enqueue(connectedSkillId);
                }
            }

            return false;
        }

        public bool IsSkillRemovable(SkillsGraph skillsGraph, int skillId)
        {
            var skill = skillsGraph[skillId];
            if (skill.Status != SkillStatus.Learned)
                return false;

            switch (skill.Info)
            {
                case BaseSkill _:
                    return false;
                case NormalSkill normalSkill:
                    return IsAllNeighborsReachable(skillsGraph, skillId);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsAllNeighborsReachable(SkillsGraph skillsGraph, int skillId)
        {
            var skill = skillsGraph[skillId];
            foreach (var neighborSkillId in skill.ConnectedSkillsId)
            {
                var neighborSkill = skillsGraph[neighborSkillId];
                if (neighborSkill.Status != SkillStatus.Learned ||
                    neighborSkill.Info is BaseSkill)
                    continue;
                
                var checkedSkillsId = new HashSet<int> { skillId, neighborSkillId };
                var skillsIdQueue = new Queue<int>();
                skillsIdQueue.Enqueue(neighborSkillId);

                var baseReached = false;
                
                while (!baseReached &&
                       skillsIdQueue.Count != 0)
                {
                    var currentSkillId = skillsIdQueue.Dequeue();
                    var currentSkill = skillsGraph[currentSkillId];
                    
                    foreach (var connectedSkillId in currentSkill.ConnectedSkillsId)
                    {
                        var connectedSkill = skillsGraph[connectedSkillId];
                        if (checkedSkillsId.Contains(connectedSkillId) ||
                            connectedSkill.Status != SkillStatus.Learned)
                            continue;
                
                        if (connectedSkill.Info is BaseSkill)
                        {
                            baseReached = true;
                            break;
                        }

                        checkedSkillsId.Add(connectedSkillId);
                        skillsIdQueue.Enqueue(connectedSkillId);
                    }
                }

                if (!baseReached)
                    return false;
            }

            return true;
        }
    }
}