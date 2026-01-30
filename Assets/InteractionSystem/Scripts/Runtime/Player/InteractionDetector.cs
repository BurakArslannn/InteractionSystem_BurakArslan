using System;
using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;
// Action eventleri için gerekli

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// Handles detecting interactable objects via Raycast and processing player input.
    /// Supports both Instant and Hold interactions.
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [Tooltip("The maximum distance (in units) the player can interact with objects.")]
        [SerializeField]
        private float m_InteractionRange = 4.0f;

        [Tooltip("The physics layers that should be checked for interactable objects.")] [SerializeField]
        private LayerMask m_InteractableLayer;

        [Header("Input Settings")] [SerializeField]
        private KeyCode m_InteractionKey = KeyCode.E;

        // Internal State
        private Camera m_Camera;
        private float m_HoldTimer;
        private IInteractable m_CurrentInteractable;

        #endregion

        #region Events

        /// <summary>
        /// Fired when an interactable object is found or lost.
        /// Param 1: IsFound (bool)
        /// Param 2: Prompt Text (string)
        /// </summary>
        public event Action<bool, string> OnInteractableFound;

        /// <summary>
        /// Fired during a hold interaction.
        /// Param 1: Progress (0.0 to 1.0)
        /// </summary>
        public event Action<float> OnHoldProgress;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Camera = Camera.main;
            if (m_Camera == null)
            {
                Debug.LogError("[InteractionDetector] Main Camera not found!");
            }
        }

        private void Update()
        {
            HandleInteractionDetection();
        }

        private void OnDrawGizmos()
        {
            if (m_Camera != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * m_InteractionRange);
            }
        }

        #endregion

        #region Methods

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

            if (duration <= 0f)
            {
                // INSTANT INTERACTION
                HandleInstantInteraction(interactable);
            }
            else
            {
                // HOLD INTERACTION
                HandleHoldInteraction(interactable, duration);
            }
        }

        private void HandleInteractableLost()
        {
            if (m_CurrentInteractable != null)
            {
                m_CurrentInteractable = null;
                OnInteractableFound?.Invoke(false, null);

                m_HoldTimer = 0f;
                OnHoldProgress?.Invoke(0f);
            }
        }

        private void HandleInstantInteraction(IInteractable interactable)
        {
            m_HoldTimer = 0f;
            OnHoldProgress?.Invoke(0f);

            if (Input.GetKeyDown(m_InteractionKey))
            {
                interactable.Interact(gameObject);
            }
        }

        private void HandleHoldInteraction(IInteractable interactable, float duration)
        {
            if (Input.GetKey(m_InteractionKey))
            {
                m_HoldTimer += Time.deltaTime;

                float progress = Mathf.Clamp01(m_HoldTimer / duration);
                OnHoldProgress?.Invoke(progress);

                // Check completion
                if (m_HoldTimer >= duration)
                {
                    interactable.Interact(gameObject);

                    // Reset after successful interaction
                    m_HoldTimer = 0f;
                    OnHoldProgress?.Invoke(0f);
                }
            }
            else
            {
                if (m_HoldTimer > 0f)
                {
                    m_HoldTimer = 0f;
                    OnHoldProgress?.Invoke(0f);
                }
            }
        }

        #endregion
    }
}