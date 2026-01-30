using InteractionSystem.Scripts.Runtime.Core;
using InteractionSystem.Scripts.Runtime.Player; // Detector erişimi için
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Abstract base class implementing common IInteractable logic.
    /// Handles interaction prompts and provides helper methods for sending UI feedback (Success/Error messages).
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Self Variables

        [SerializeField] private string m_Prompt = "Interact";
        public string InteractionPrompt => m_Prompt;

        #endregion

        #region Methods

        void IInteractable.Interact(GameObject interactor)
        {
            OnInteract(interactor);
        }

        public virtual float GetHoldDuration() => 0f;
        protected abstract void OnInteract(GameObject interactor);

        protected void SendFeedback(GameObject interactor, string message, bool isError = false)
        {
            if (interactor.TryGetComponent(out InteractionDetector detector))
            {
                detector.ReportFeedback(message, isError);
            }
        }

        #endregion
    }
}