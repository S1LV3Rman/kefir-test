namespace Source
{
    public interface ISkillTreeView
    {
        public void CreateSkillView(ISkill skill);
        public void CreateConnectionView(SkillsPair skillsPair);
        public void CreateConnectionView(ISkill firstSkill, ISkill secondSkill);
        public void ClearAllSkillsViews();
        public void ClearAllConnectionsViews();
        public void SetSkillStatus(ISkill skill, SkillStatus status);
        public void SetConnectionStatus(SkillsPair skillsPair, ConnectionStatus status);
        public void SetConnectionStatus(ISkill firstSkill, ISkill secondSkill, ConnectionStatus status);
    }
}