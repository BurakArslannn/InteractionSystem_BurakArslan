using UnityEngine;
using Cysharp.Threading.Tasks;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// A basic one-time use object.
    /// Demonstrates safe asynchronous destruction using UniTask and CancellationTokens.
    /// </summary>
    public class SimpleInteractable : InteractableBase
    {
        protected override async void OnInteract(GameObject interactor)
        {
            if (TryGetComponent(out Collider col)) col.enabled = false;

            SendFeedback(interactor, "Item Collected!", false);

            await UniTask.Delay(100, cancellationToken: this.GetCancellationTokenOnDestroy());
            Destroy(gameObject);
        }
    }
}