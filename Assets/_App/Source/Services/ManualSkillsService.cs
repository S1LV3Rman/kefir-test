using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace Source
{
    public sealed class ManualSkillsService : ISkillService
    {
        private readonly IRandomService _randomService;
        private readonly SkillTreeView _skillTreeView;

        public ManualSkillsService(IRandomService randomService, SkillTreeView skillTreeView)
        {
            _randomService = randomService;
            _skillTreeView = skillTreeView;
        }

        private ISkill[] GetAllSkills()
        {
            var allSkills = new List<ISkill>
            {
                new BaseSkill { Name = "База", Id = 1, SlotId = 1 }
            };

            for (var i = 1; i < 15; ++i)
                allSkills.Add(
                    new NormalSkill { Name = i.ToString(), Id = i + 1, SlotId = i + 1, Price = _randomService.Range(1, 5) });
            
            return allSkills.ToArray();
        }

        public SkillsGraph GetSkillsGraph()
        {
            var allSkills = GetAllSkills();
            
            var skillsGraph = new SkillsGraph();
            foreach (var skill in allSkills)
            {
                skillsGraph.Add(skill.Id, new SkillsGraph.SkillGraphInfo
                {
                    Info = skill,
                    Status = SkillStatus.Undefined,
                    ConnectedSkillsId = new HashSet<int>()
                });
            }
            
            foreach (var slot in _skillTreeView.SkillSlots)
            {
                var skill = allSkills.FirstOrDefault(s => s.SlotId == slot.Id);
                if (skill == null) continue;
                
                foreach (var connectedSlot in slot.ConnectedSlots)
                {
                    var connectedSkill = allSkills.FirstOrDefault(s => s.SlotId == connectedSlot.Id);
                    if (connectedSkill == null) continue;
                    
                    skillsGraph[skill.Id].ConnectedSkillsId.Add(connectedSkill.Id);
                }
            }

            return skillsGraph;
        }
    }
}