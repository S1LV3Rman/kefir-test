using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source
{
    public class SkillTreeView : MonoBehaviour, ISkillTreeView
    {
        [SerializeField] private SkillView baseSkillPrefab;
        [SerializeField] private SkillView normalSkillPrefab;
        
        [SerializeField] private ConnectionView connectionPrefab;
        [SerializeField] private Transform connectionsHolder;
        
        [SerializeField] private List<SkillSlotView> skillSlots;

        private Dictionary<int, SkillView> _skillViews = new Dictionary<int, SkillView>();
        private Dictionary<SkillsPair, ConnectionView> _connectionViews = new Dictionary<SkillsPair, ConnectionView>();

        public List<SkillSlotView> SkillSlots => skillSlots;

        public void CreateSkillView(ISkill skill)
        {
            var slot = skillSlots.FirstOrDefault(slot => slot.Id == skill.SlotId);
            if (slot == null)
            {
                Debug.LogError($"Slot {skill.SlotId} not found for skill {skill.Name}");
                return;
            }

            SkillView skillView;
            switch (skill)
            {
                case BaseSkill _:
                    skillView = Instantiate(baseSkillPrefab, slot.transform);
                    break;
                case NormalSkill _:
                    skillView = Instantiate(normalSkillPrefab, slot.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill));
            }

            skillView.Title.text = skill.Name;
            skillView.Id = skill.Id;
            
            _skillViews.Add(skill.Id, skillView);
        }

        public void CreateConnectionView(SkillsPair skillsPair)
        {
            var connectionView = Instantiate(connectionPrefab, connectionsHolder);
            connectionView.Line.AnchorA.Transform = skillSlots.First(slot => slot.Id == skillsPair.firstSkill.SlotId).transform;
            connectionView.Line.AnchorB.Transform = skillSlots.First(slot => slot.Id == skillsPair.secondSkill.SlotId).transform;
            
            _connectionViews.Add(skillsPair, connectionView);
        }

        public void CreateConnectionView(ISkill firstSkill, ISkill secondSkill)
        {
            CreateConnectionView(new SkillsPair(firstSkill, secondSkill));
        }

        public void ClearAllSkillsViews()
        {
            foreach (var skillView in _skillViews)
                Destroy(skillView.Value.gameObject);
            
            _skillViews.Clear();
        }

        public void ClearAllConnectionsViews()
        {
            foreach (var connectionView in _connectionViews)
                Destroy(connectionView.Value.gameObject);
            
            _connectionViews.Clear();
        }

        public void SetSkillStatus(ISkill skill, SkillStatus status)
        {
            int switchState;
            switch (status)
            {
                case SkillStatus.Undefined:
                case SkillStatus.Blocked:
                    switchState = 2;
                    break;
                case SkillStatus.Learnable:
                    switchState = 1;
                    break;
                case SkillStatus.Learned:
                    switchState = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }

            _skillViews[skill.Id].Switch.State = switchState;
        }

        public void SetConnectionStatus(SkillsPair skillsPair, ConnectionStatus status)
        {
            int switchState;
            switch (status)
            {
                case ConnectionStatus.Undefined:
                case ConnectionStatus.Blocked:
                    switchState = 1;
                    break;
                case ConnectionStatus.Learnable:
                case ConnectionStatus.Learned:
                    switchState = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }

            _connectionViews[skillsPair].Switch.State = switchState;
        }

        public void SetConnectionStatus(ISkill firstSkill, ISkill secondSkill, ConnectionStatus status)
        {
            SetConnectionStatus(new SkillsPair(firstSkill, secondSkill), status);
        }
    }
}