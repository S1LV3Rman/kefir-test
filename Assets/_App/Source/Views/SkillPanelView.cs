using System;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source
{
    public class SkillPanelView : MonoBehaviour, ISkillPanelView
    {
        [SerializeField] private TextMeshProUGUI PriceLabel;
        [SerializeField] private LeanButton LearnButton;
        [SerializeField] private LeanButton ForgetButton;
        
        public event Action OnLearnSkill;
        public event Action OnForgetSkill;

        private void Start()
        {
            LearnButton.OnClick.AddListener(LearnButtonClicked);
            ForgetButton.OnClick.AddListener(ForgetButtonClicked);
        }

        public void SetSkillInfo(ISkill skill)
        {
            switch (skill)
            {
                case BaseSkill _:
                    PriceLabel.text = $"Стоимость: нет";
                    break;
                case NormalSkill normalSkill:
                    PriceLabel.text = $"Стоимость: {normalSkill.Price}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill));
            }
        }

        public void SetLearnable(bool isLearnable)
        {
            LearnButton.interactable = isLearnable;
        }

        public void SetForgettable(bool isForgettable)
        {
            ForgetButton.interactable = isForgettable;
        }

        private void LearnButtonClicked()
        {
            OnLearnSkill?.Invoke();
        }

        private void ForgetButtonClicked()
        {
            OnForgetSkill?.Invoke();
        }
    }
}