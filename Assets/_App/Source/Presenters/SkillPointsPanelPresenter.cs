namespace Source
{
    public class SkillPointsPanelPresenter : IInitPresenter
    {
        private readonly ISkillPointsPanelView _skillPointsPanelView;
        private readonly SkillTreeModel _skillTreeModel;

        public SkillPointsPanelPresenter(ISkillPointsPanelView skillPointsPanelView, SkillTreeModel skillTreeModel)
        {
            _skillPointsPanelView = skillPointsPanelView;
            _skillTreeModel = skillTreeModel;
        }

        public void Init()
        {
            _skillTreeModel.OnSkillPointsChanged += UpdateView;
            
            _skillPointsPanelView.OnAddSkillPoint += AddSkillPoint;
            
            UpdateView();
        }

        private void AddSkillPoint()
        {
            _skillTreeModel.AddSkillPoint();
        }

        public void UpdateView()
        {
            var skillPoints = _skillTreeModel.SkillPoints;
            _skillPointsPanelView.SetSkillPoints(skillPoints);
        }
    }
}