namespace Source
{
    public class GeneralPanelPresenter : IInitPresenter
    {
        private readonly IGeneralPanelView _generalPanelView;
        private readonly SkillTreeModel _skillTreeModel;

        public GeneralPanelPresenter(IGeneralPanelView generalPanelView, SkillTreeModel skillTreeModel)
        {
            _generalPanelView = generalPanelView;
            _skillTreeModel = skillTreeModel;
        }

        public void Init()
        {
            _generalPanelView.OnForgetAll += _skillTreeModel.ForgetAllSkills;
        }
    }
}