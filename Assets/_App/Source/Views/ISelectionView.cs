using UnityEngine.UI;

namespace Source
{
    public interface ISelectionView
    {
        public bool IsSelected { get; }
        public Selectable CurrentSelectable { get; }
    }
}