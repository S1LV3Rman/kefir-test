using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Source
{
    public class SkillSlotView : MonoBehaviour
    {
        public int Id;
        public List<SkillSlotView> ConnectedSlots = new List<SkillSlotView>();

        // Just to simplify connections creation
        private void OnValidate()
        {
            foreach (var connectedSlot in ConnectedSlots)
            {
                if (connectedSlot != null &&
                    connectedSlot.ConnectedSlots.All(slot => slot.Id != Id))
                {
                    connectedSlot.ConnectedSlots.Add(this);
                    EditorUtility.SetDirty(connectedSlot);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, 64);
            
            foreach (var connectedSlot in ConnectedSlots)
                Gizmos.DrawLine(transform.position, connectedSlot.transform.position);
        }
    }
}