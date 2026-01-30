using InteractionSystem.Scripts.Runtime.Player;
using UnityEngine;

// Inventory erişimi için

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Represents a door that can be locked or unlocked using keys from Inventory.
    /// </summary>
    public class DoorInteractable : InteractableBase
    {
        #region Fields

        [Header("Door Settings")] [Tooltip("If true, the door cannot be opened without a key.")] [SerializeField]
        private bool m_IsLocked;

        [Tooltip("Current state of the door.")] [SerializeField]
        private bool m_IsOpen;

        [Tooltip("The ID of the key required to unlock this door (e.g., 'RedKey').")] [SerializeField]
        private string m_KeyID;

        #endregion

        #region Methods

        protected override void OnInteract()
        {
            // 1. Check Lock State
            if (m_IsLocked)
            {
                // Try to find inventory
                var inventory = FindObjectOfType<Inventory>();

                if (inventory != null && inventory.HasKey(m_KeyID))
                {
                    // Has Key -> Unlock and Open
                    m_IsLocked = false;
                    Debug.Log($"[Door] Unlocked using key: {m_KeyID}");

                    ToggleDoor();
                }
                else
                {
                    // No Key -> Access Denied
                    Debug.Log($"[Door] Access Denied. Requires Key: {m_KeyID}");
                }
            }
            else
            {
                // 2. Already Unlocked -> Just Toggle
                ToggleDoor();
            }
        }

        private void ToggleDoor()
        {
            m_IsOpen = !m_IsOpen;

            // Visual feedback: Rotate 90 degrees
            float rotationAngle = m_IsOpen ? 90f : -90f;
            transform.Rotate(Vector3.up, rotationAngle);

            string status = m_IsOpen ? "OPENED" : "CLOSED";
            Debug.Log($"[Door] {status}");
        }

        /// <summary>
        /// Force unlock (for debug or event usage).
        /// </summary>
        public void Unlock(string keyID)
        {
            if (m_IsLocked && m_KeyID == keyID)
            {
                m_IsLocked = false;
                Debug.Log($"[Door] Unlocked via method with {keyID}");
            }
        }

        #endregion
    }
}