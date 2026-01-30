using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Interface that must be implemented by any object that the player can interact with.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Gets the prompt text to be displayed on the UI (e.g., "Press E to Open").
        /// </summary>
        string InteractionPrompt { get; }

        /// <summary>
        /// Triggered when the player performs an interaction.
        /// </summary>
        /// <param name="interactor">The GameObject that initiated the interaction.</param>
        void Interact(GameObject interactor);
    }
}