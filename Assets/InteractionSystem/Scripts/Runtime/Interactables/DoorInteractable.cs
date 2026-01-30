using InteractionSystem.Scripts.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// A concrete interactable representing a Door.
    /// Validates if the interacting player possesses the specific KeyID in their Inventory before toggling the open state.
    /// </summary>
    public class DoorInteractable : InteractableBase
    {
        #region Self Variables

        [Header("Door Settings")] [SerializeField]
        private bool m_IsLocked;

        [SerializeField] private bool m_IsOpen;
        [SerializeField] private string m_KeyID;

        #endregion

        #region Methods

        protected override void OnInteract(GameObject interactor)
        {
            if (m_IsLocked)
            {
                if (interactor.TryGetComponent(out Inventory inventory) && inventory.HasKey(m_KeyID))
                {
                    m_IsLocked = false;
                    SendFeedback(interactor, $"Unlocked with {m_KeyID}!", false);
                    ToggleDoor();
                }
                else
                {
                    SendFeedback(interactor, $"Requires Key: {m_KeyID}", true);
                }
            }
            else
            {
                ToggleDoor();
            }
        }

        private void ToggleDoor()
        {
            m_IsOpen = !m_IsOpen;
            transform.Rotate(Vector3.up, m_IsOpen ? 90f : -90f);
        }

        #endregion
    }
}