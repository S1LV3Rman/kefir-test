using Lean.Gui;
using TMPro;
using UnityEngine;

namespace Source
{
    public class SkillView : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public LeanSwitch Switch;

        [HideInInspector] public int Id;
    }
}