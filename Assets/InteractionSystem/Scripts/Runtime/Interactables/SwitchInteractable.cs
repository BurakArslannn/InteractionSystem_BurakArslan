using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    /// <summary>
    /// An interactable object that toggles between two states (On/Off).
    /// </summary>
    public class SwitchInteractable : InteractableBase
    {
        #region Fields

        [Tooltip("Current state of the switch.")] [SerializeField]
        private bool m_IsOn;

        #endregion

        #region Methods

        protected override void OnInteract()
        {
            m_IsOn = !m_IsOn;
            string status = m_IsOn ? "ON" : "OFF";

            Debug.Log($"Switch {gameObject.name} is now {status}");
        }

        #endregion
    }
}