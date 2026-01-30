using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// Handles detecting interactable objects via Raycast and processing player input.
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [Tooltip("The maximum distance (in units) the player can interact with objects.")]
        [SerializeField]
        private float m_InteractionRange = 3.0f;

        [Tooltip("The physics layers that should be checked for interactable objects.")] [SerializeField]
        private LayerMask m_InteractableLayer;

        private Camera m_Camera;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Camera = Camera.main;
            if (m_Camera == null)
            {
                Debug.LogError(
                    $"[InteractionDetector] Main Camera not found on {gameObject.name}. Interaction will not work.");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PerformInteraction();
            }
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

        /// <summary>
        /// Casts a ray to detect an IInteractable and triggers the interaction if found.
        /// </summary>
        private void PerformInteraction()
        {
            if (m_Camera == null) return;

            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractionRange, m_InteractableLayer))
            {
                if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this.gameObject);
                }
            }
        }

        #endregion
    }
}