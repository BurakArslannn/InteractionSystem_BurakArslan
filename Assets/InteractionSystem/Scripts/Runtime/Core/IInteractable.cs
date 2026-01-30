using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Interface that must be implemented by any object that the player can interact with.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Gets the prompt text to be displayed on the UI.
        /// </summary>
        string InteractionPrompt { get; }

        /// <summary>
        /// Gets the hold duration required for interaction (0 for instant).
        /// </summary>
        float GetHoldDuration(); // <--- BU SATIRI EKLEDİK

        /// <summary>
        /// Triggered when the player performs an interaction.
        /// </summary>
        void Interact(GameObject interactor);
    }
}