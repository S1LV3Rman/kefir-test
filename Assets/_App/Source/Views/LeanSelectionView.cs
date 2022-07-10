using System.Linq;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class LeanSelectionView : MonoBehaviour, ISelectionView
    {
        [SerializeField] private LeanSelectionManager selectionManager;
        
        public bool IsSelected => selectionManager.SelectionHistory.Count != 0;
        public Selectable CurrentSelectable => selectionManager.SelectionHistory.Last();
    }
}