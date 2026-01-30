using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// Base class for all interactable objects. Handles common logic like prompts and interface implementation.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields

        [Tooltip("The text that will appear on the UI when the player looks at this object.")] [SerializeField]
        private string m_Prompt = "Interact";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the interaction prompt text.
        /// </summary>
        public string InteractionPrompt => m_Prompt;

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Explicit implementation of the IInteractable interface.
        /// Calls the abstract OnInteract method.
        /// </summary>
        /// <param name="interactor">The GameObject initiating the interaction.</param>
        void IInteractable.Interact(GameObject interactor)
        {
            OnInteract();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the player interacts with this object.
        /// Must be implemented by derived classes.
        /// </summary>
        protected abstract void OnInteract();

        #endregion
    }
}