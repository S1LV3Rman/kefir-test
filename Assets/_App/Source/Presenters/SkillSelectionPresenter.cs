using System;
using System.Linq;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class SkillSelectionPresenter : IRunPresenter
    {
        private readonly ISelectionView _selectionView;
        private readonly SkillTreeModel _skillTreeModel;

        private Selectable _currentSelectable;

        public SkillSelectionPresenter(ISelectionView selectionView, SkillTreeModel skillTreeModel)
        {
            _selectionView = selectionView;
            _skillTreeModel = skillTreeModel;
        }

        public void Run()
        {
            if (!_selectionView.IsSelected)
                return;
            
            var selected = _selectionView.CurrentSelectable;

            if (selected != _currentSelectable)
            {
                var skillView = selected.GetComponent<SkillView>();
                if (skillView != null)
                    _skillTreeModel.SelectedSkillId = skillView.Id;

                _currentSelectable = selected;
            }
        }
    }
}