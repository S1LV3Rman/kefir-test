using System;
using Lean.Gui;
using UnityEngine;

namespace Source
{
    public class App : MonoBehaviour
    {
        [SerializeField] private SkillTreeView skillTreeView;
        [SerializeField] private SkillPanelView skillPanelView;
        [SerializeField] private SkillPointsPanelView skillPointsPanelView;
        [SerializeField] private GeneralPanelView generalPanelView;
        [SerializeField] private LeanSelectionView leanSelectionView;
        
        private AppPresenters presenters = new AppPresenters();
        
        private void Start()
        {
            var randomService = new SystemRandomService();
            var skillsService = new ManualSkillsService(randomService, skillTreeView);
            var graphSearchService = new BreadthSearchService();

            var skillTreeModel = new SkillTreeModel(skillsService);

            presenters
                .Add(new SkillTreePresenter(skillTreeView, skillTreeModel))
                .Add(new SkillSelectionPresenter(leanSelectionView, skillTreeModel))
                .Add(new SkillPanelPresenter(skillPanelView, skillTreeModel, graphSearchService))
                .Add(new SkillPointsPanelPresenter(skillPointsPanelView, skillTreeModel))
                .Add(new GeneralPanelPresenter(generalPanelView, skillTreeModel));
            
            presenters.Init();
        }

        private void Update()
        {
            presenters.Run();
        }
    }
}