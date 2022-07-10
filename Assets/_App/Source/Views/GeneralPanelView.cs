using System;
using Lean.Gui;
using UnityEngine;

namespace Source
{
    public class GeneralPanelView : MonoBehaviour, IGeneralPanelView
    {
        [SerializeField] private LeanButton ForgetAllButton;
        
        public event Action OnForgetAll;

        private void Start()
        {
            ForgetAllButton.OnClick.AddListener(ForgetAllButtonClicked);
        }

        private void ForgetAllButtonClicked()
        {
            OnForgetAll?.Invoke();
        }
    }
}