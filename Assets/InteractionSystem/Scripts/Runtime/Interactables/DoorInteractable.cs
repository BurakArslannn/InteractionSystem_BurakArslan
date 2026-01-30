using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Represents a door that can be locked or unlocked.
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
            // Check lock state
            if (m_IsLocked)
            {
                Debug.Log($"[Door] Access Denied. Requires Key: {m_KeyID}");
                return;
            }

            // Toggle open state
            m_IsOpen = !m_IsOpen;

            // Visual feedback: Rotate 90 degrees
            float rotationAngle = m_IsOpen ? 90f : -90f;
            transform.Rotate(Vector3.up, rotationAngle);

            string status = m_IsOpen ? "OPENED" : "CLOSED";
            Debug.Log($"[Door] {status}");
        }

        /// <summary>
        /// Attempts to unlock the door with a given key ID.
        /// </summary>
        /// <param name="keyID">The ID of the key used.</param>
        public void Unlock(string keyID)
        {
            if (m_IsLocked && m_KeyID == keyID)
            {
                m_IsLocked = false;
                Debug.Log($"[Door] Unlocked with {keyID}");
            }
        }

        #endregion
    }
}