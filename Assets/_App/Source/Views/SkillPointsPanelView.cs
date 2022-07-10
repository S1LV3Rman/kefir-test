using System;
using Lean.Gui;
using TMPro;
using UnityEngine;

namespace Source
{
    public class SkillPointsPanelView : MonoBehaviour, ISkillPointsPanelView
    {
        [SerializeField] private TextMeshProUGUI SkillPointsLabel;
        [SerializeField] private LeanButton AddButton;
        
        public event Action OnAddSkillPoint;

        private void Start()
        {
            AddButton.OnClick.AddListener(AddButtonClicked);
        }

        public void SetSkillPoints(int skillPoints)
        {
            SkillPointsLabel.text = $"Доступно очков: {skillPoints}";
        }

        private void AddButtonClicked()
        {
            OnAddSkillPoint?.Invoke();
        }
    }
}