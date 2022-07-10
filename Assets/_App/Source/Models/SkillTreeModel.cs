using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class SkillTreeModel
    {
        public event Action OnSkillPointsChanged;
        public event Action OnSelectedSkillChanged;
        public event Action OnSkillsStatusChanged;
        
        private SkillsGraph _skillsGraph;
        private HashSet<int> _learnedSkills;
        private int _skillPoints;
        private int _selectedSkillId;

        public SkillsGraph SkillsGraph => _skillsGraph;
        public int SkillPoints => _skillPoints;
        
        public SkillTreeModel(ISkillService skillService)
        {
            _skillsGraph = skillService.GetSkillsGraph();

            var baseSkill = _skillsGraph.First(skill => skill.Value.Info is BaseSkill).Value.Info;
            _learnedSkills = new HashSet<int> { baseSkill.Id };
            UpdateSkillsStatus();
            
            _skillPoints = 10;
        }

        public void LearnSelectedSkill()
        {
            LearnSkill(_skillsGraph[_selectedSkillId].Info);
        }

        public void LearnSkill(ISkill skill)
        {
            if (skill is NormalSkill normalSkill)
            {
                _skillPoints -= normalSkill.Price;
                OnSkillPointsChanged?.Invoke();
                
                _learnedSkills.Add(skill.Id);
                UpdateSkillsStatus();
            }
        }

        public void ForgetSelectedSkill()
        {
            ForgetSkill(_skillsGraph[_selectedSkillId].Info);
        }

        public void ForgetSkill(ISkill skill)
        {
            if (skill is NormalSkill normalSkill)
            {
                _skillPoints += normalSkill.Price;
                OnSkillPointsChanged?.Invoke();
                
                _learnedSkills.Remove(skill.Id);
                UpdateSkillsStatus();
            }
        }

        public void ForgetAllSkills()
        {
            var baseSkills = new HashSet<int>();

            foreach (var learnedSkillId in _learnedSkills)
            {
                var learnedSkill = _skillsGraph[learnedSkillId];
                switch (learnedSkill.Info)
                {
                    case BaseSkill baseSkill:
                        baseSkills.Add(baseSkill.Id);
                        break;
                    case NormalSkill normalSkill:
                        _skillPoints += normalSkill.Price;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            OnSkillPointsChanged?.Invoke();
            
            _learnedSkills = baseSkills;
            UpdateSkillsStatus();
        }

        public void AddSkillPoint()
        {
            _skillPoints++;
            OnSkillPointsChanged?.Invoke();
        }

        public int SelectedSkillId
        {
            get => _selectedSkillId;
            set
            {
                _selectedSkillId = value;
                OnSelectedSkillChanged?.Invoke();
            }
        }

        private void UpdateSkillsStatus()
        {
            foreach (var skill in _skillsGraph)
                skill.Value.Status = SkillStatus.Blocked;
            
            foreach (var learnedSkill in _learnedSkills)
            {
                _skillsGraph[learnedSkill].Status = SkillStatus.Learned;
                foreach (var connectedSkillId in _skillsGraph[learnedSkill].ConnectedSkillsId)
                {
                    if (_skillsGraph[connectedSkillId].Status != SkillStatus.Learned)
                        _skillsGraph[connectedSkillId].Status = SkillStatus.Learnable;
                }
            }
            
            OnSkillsStatusChanged?.Invoke();
        }
    }
}