using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// A simple interactable object that is destroyed when interacted with (e.g., a collectible item).
    /// </summary>
    public class SimpleInteractable : InteractableBase
    {
        #region Methods

        protected override void OnInteract()
        {
            Debug.Log($"Interacted with simple object: {gameObject.name}");
            Destroy(gameObject);
        }

        #endregion
    }
}