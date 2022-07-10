using System;

namespace Source
{
    public interface ISkillPointsPanelView
    {
        public event Action OnAddSkillPoint;
        public void SetSkillPoints(int skillPoints);
    }
}