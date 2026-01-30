using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// The core contract that defines the essential behavior for any object the player can interact with.
    /// Acts as a bridge between the InteractionDetector and concrete objects (like Doors or Chests).
    /// </summary>
    public interface IInteractable
    {
        string InteractionPrompt { get; }
        float GetHoldDuration();
        void Interact(GameObject interactor);
    }
}