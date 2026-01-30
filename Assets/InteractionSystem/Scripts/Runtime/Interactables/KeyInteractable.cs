using InteractionSystem.Scripts.Runtime.Player;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// A collectible item that grants access permissions.
    /// Uses UniTask to handle asynchronous pickup logic (simulating delay/effects) before adding the key to the Inventory.
    /// </summary>
    public class KeyInteractable : InteractableBase
    {
        #region Self Variables

        [Header("Key Settings")] [SerializeField]
        private string m_KeyID;

        #endregion

        #region Methods

        protected override async void OnInteract(GameObject interactor)
        {
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                if (TryGetComponent(out Collider col)) col.enabled = false;
                if (TryGetComponent(out Renderer rend)) rend.enabled = false;

                SendFeedback(interactor, $"Picked up: {m_KeyID}", false);

                await UniTask.Delay(100, cancellationToken: this.GetCancellationTokenOnDestroy());

                inventory.AddKey(m_KeyID);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}