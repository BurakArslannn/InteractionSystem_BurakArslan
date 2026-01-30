using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Represents a chest that requires a hold interaction to open.
    /// </summary>
    public class ChestInteractable : InteractableBase
    {
        #region Fields

        [Header("Chest Settings")] [Tooltip("Time in seconds required to hold the interaction key.")] [SerializeField]
        private float m_HoldDuration = 3.0f;

        [Tooltip("Has the chest been opened already?")] [SerializeField]
        private bool m_IsOpened;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the configured hold duration for this chest.
        /// </summary>
        public override float GetHoldDuration()
        {
            return m_HoldDuration;
        }

        protected override void OnInteract()
        {
            if (m_IsOpened)
            {
                Debug.Log("[Chest] Already empty.");
                return;
            }

            m_IsOpened = true;
            Debug.Log("[Chest] Opened! You found an item.");

            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.gray;
            }
        }

        #endregion
    }
}