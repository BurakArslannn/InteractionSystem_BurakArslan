using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Represents a container that requires a 'Hold' interaction to open.
    /// caches the Renderer component for optimized material color changes upon interaction.
    /// </summary>
    public class ChestInteractable : InteractableBase
    {
        #region Self

        [SerializeField] private float m_HoldDuration = 3.0f;
        [SerializeField] private bool m_IsOpened;
        private Renderer m_Renderer;

        #endregion

        #region Methods

        private void Start() => m_Renderer = GetComponent<Renderer>();

        public override float GetHoldDuration() => m_HoldDuration;

        protected override void OnInteract(GameObject interactor)
        {
            if (m_IsOpened)
            {
                SendFeedback(interactor, "Already Empty", true);
                return;
            }

            m_IsOpened = true;
            if (m_Renderer) m_Renderer.material.color = Color.gray;

            SendFeedback(interactor, "Chest Opened!", false);
        }

        #endregion
    }
}