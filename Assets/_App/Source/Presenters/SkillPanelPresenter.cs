using System;

namespace Source
{
    public class SkillPanelPresenter : IInitPresenter
    {
        private readonly ISkillPanelView _skillPanelView;
        private readonly SkillTreeModel _skillTreeModel;
        private readonly IGraphSearchService _searchService;

        public SkillPanelPresenter(ISkillPanelView skillPanelView, SkillTreeModel skillTreeModel, IGraphSearchService searchService)
        {
            _skillPanelView = skillPanelView;
            _skillTreeModel = skillTreeModel;
            _searchService = searchService;
        }

        public void Init()
        {
            _skillTreeModel.OnSelectedSkillChanged += UpdateView;
            _skillTreeModel.OnSkillsStatusChanged += UpdateView;
            _skillTreeModel.OnSkillPointsChanged += UpdateView;

            _skillPanelView.OnLearnSkill += LearnSkill;
            _skillPanelView.OnForgetSkill += ForgetSkill;
        }

        private void UpdateView()
        {
            var selectedSkillId = _skillTreeModel.SelectedSkillId;
            var selectedSkill = _skillTreeModel.SkillsGraph[selectedSkillId].Info;
            _skillPanelView.SetSkillInfo(selectedSkill);

            bool isLearnable = false;
            bool isForgettable = false;

            if (selectedSkill is NormalSkill normalSkill)
            {
                var skillsGraph = _skillTreeModel.SkillsGraph;
                var skillPoints = _skillTreeModel.SkillPoints;

                isLearnable = normalSkill.Price <= skillPoints &&
                              _searchService.IsSkillLearnable(skillsGraph, selectedSkillId);
                
                isForgettable = _searchService.IsSkillRemovable(skillsGraph, selectedSkillId);
            }
            
            _skillPanelView.SetLearnable(isLearnable);
            _skillPanelView.SetForgettable(isForgettable);
        }

        private void LearnSkill()
        {
            _skillTreeModel.LearnSelectedSkill();
        }

        private void ForgetSkill()
        {
            _skillTreeModel.ForgetSelectedSkill();
        }
    }
}