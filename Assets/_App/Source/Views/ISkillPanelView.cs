using System;

namespace Source
{
    public interface ISkillPanelView
    {
        public event Action OnLearnSkill;
        public event Action OnForgetSkill;
        public void SetSkillInfo(ISkill skill);
        public void SetLearnable(bool isLearnable);
        public void SetForgettable(bool isForgettable);
    }
}