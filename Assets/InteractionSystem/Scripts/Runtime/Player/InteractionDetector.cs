using System;
using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// The central brain of the interaction system attached to the Player.
    /// Uses Raycasting to detect 'IInteractable' objects, manages Input logic (Instant vs Hold), 
    /// and acts as an Event Publisher (Observer Pattern) to notify the UI system.
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")] [SerializeField]
        private float m_InteractionRange = 4.0f;

        [SerializeField] private LayerMask m_InteractableLayer;
        [SerializeField] private KeyCode m_InteractionKey = KeyCode.E;

        private Camera m_Camera;
        private float m_HoldTimer;
        private IInteractable m_CurrentInteractable;

        #endregion

        #region Events (Observer Pattern)

        public event Action<bool, string> OnInteractableFound;
        public event Action<float> OnHoldProgress;
        public event Action<string, bool> OnInteractionFeedback;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Camera = Camera.main;
            if (m_Camera == null) Debug.LogError("[InteractionDetector] Main Camera missing!");
        }

        private void Update() => HandleInteractionDetection();

        #endregion

        #region Logic

        private void HandleInteractionDetection()
        {
            if (m_Camera == null) return;

            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, m_InteractionRange, m_InteractableLayer))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    HandleInteractableFound(interactable);
                    return;
                }
            }

            HandleInteractableLost();
        }

        private void HandleInteractableFound(IInteractable interactable)
        {
            m_CurrentInteractable = interactable;
            OnInteractableFound?.Invoke(true, interactable.InteractionPrompt);

            float duration = interactable.GetHoldDuration();
            if (duration <= 0f) HandleInstantInteraction(interactable);
            else HandleHoldInteraction(interactable, duration);
        }

        private void HandleInteractableLost()
        {
            if (m_CurrentInteractable != null)
            {
                m_CurrentInteractable = null;
                OnInteractableFound?.Invoke(false, null);
                ResetHold();
            }
        }

        private void HandleInstantInteraction(IInteractable interactable)
        {
            ResetHold();
            if (Input.GetKeyDown(m_InteractionKey)) interactable.Interact(gameObject);
        }

        private void HandleHoldInteraction(IInteractable interactable, float duration)
        {
            if (Input.GetKey(m_InteractionKey))
            {
                m_HoldTimer += Time.deltaTime;
                OnHoldProgress?.Invoke(Mathf.Clamp01(m_HoldTimer / duration));

                if (m_HoldTimer >= duration)
                {
                    interactable.Interact(gameObject);
                    ResetHold();
                }
            }
            else if (m_HoldTimer > 0f) ResetHold();
        }

        private void ResetHold()
        {
            m_HoldTimer = 0f;
            OnHoldProgress?.Invoke(0f);
        }

        public void ReportFeedback(string message, bool isError = false)
        {
            OnInteractionFeedback?.Invoke(message, isError);
        }

        #endregion
    }
}