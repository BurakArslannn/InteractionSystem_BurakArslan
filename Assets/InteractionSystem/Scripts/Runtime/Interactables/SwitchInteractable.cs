using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// A simple toggleable object that switches between ON/OFF states and reports its status via the UI feedback system.
    /// </summary>
    public class SwitchInteractable : InteractableBase
    {
        [SerializeField] private bool m_IsOn;

        protected override void OnInteract(GameObject interactor)
        {
            m_IsOn = !m_IsOn;
            
            string status = m_IsOn ? "ON" : "OFF";
            SendFeedback(interactor, $"Switch is {status}", false);
        }
    }
}