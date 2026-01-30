using InteractionSystem.Scripts.Runtime.Player;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Represents a key that can be picked up and added to the player's inventory.
    /// </summary>
    public class KeyInteractable : InteractableBase
    {
        #region Fields

        [Header("Key Settings")]
        [Tooltip("The unique ID for this key (e.g., 'RedKey'). This must match the Door's KeyID.")]
        [SerializeField]
        private string m_KeyID;

        #endregion

        #region Methods

        protected override void OnInteract()
        {
            var inventory = FindObjectOfType<Inventory>();

            if (inventory != null)
            {
                inventory.AddKey(m_KeyID);

                // Visual feedback (Optional: Play sound here)
                Debug.Log($"[Key] Collected {m_KeyID}");

                // Destroy the object
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("[KeyInteractable] No Inventory found in scene!");
            }
        }

        #endregion
    }
}