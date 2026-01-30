using DG.Tweening;
using InteractionSystem.Scripts.Runtime.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// DOTween namespace

// InteractionDetector'a erişim için

namespace InteractionSystem.Scripts.Runtime.UI
{
    /// <summary>
    /// Manages the Interaction UI feedback using DOTween for animations.
    /// Listens to events from the InteractionDetector.
    /// </summary>
    public class InteractionUI : MonoBehaviour
    {
        #region Fields

        [Header("UI References")]
        [Tooltip("The text component displaying the interaction prompt (e.g., 'Press E').")]
        [SerializeField]
        private TextMeshProUGUI m_PromptText;

        [Tooltip("The progress bar image (filled type) for hold interactions.")] [SerializeField]
        private Image m_ProgressBar;

        [Tooltip("CanvasGroup for fading the UI in/out.")] [SerializeField]
        private CanvasGroup m_CanvasGroup;

        [Header("System References")]
        [Tooltip("Reference to the player's InteractionDetector script.")]
        [SerializeField]
        private InteractionDetector m_Detector;

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Auto-find detector if not assigned
            if (m_Detector == null)
            {
                m_Detector = FindObjectOfType<InteractionDetector>();
                if (m_Detector == null)
                {
                    Debug.LogError("[InteractionUI] InteractionDetector not found in scene!");
                    return;
                }
            }

            // Subscribe to events
            m_Detector.OnInteractableFound += HandleInteractableFound;
            m_Detector.OnHoldProgress += HandleHoldProgress;

            // Initialize UI state (Hidden)
            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = 0f;
            }

            if (m_ProgressBar != null)
            {
                m_ProgressBar.fillAmount = 0f;
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe to prevent memory leaks
            if (m_Detector != null)
            {
                m_Detector.OnInteractableFound -= HandleInteractableFound;
                m_Detector.OnHoldProgress -= HandleHoldProgress;
            }
        }

        #endregion

        #region Event Handlers

        private void HandleInteractableFound(bool isFound, string promptText)
        {
            if (m_CanvasGroup == null) return;

            // Kill any active tweens on this object to prevent conflicts
            m_CanvasGroup.DOKill();

            if (isFound)
            {
                if (m_PromptText != null) m_PromptText.text = promptText;

                // Fade In
                m_CanvasGroup.DOFade(1f, 0.2f);
            }
            else
            {
                // Fade Out
                m_CanvasGroup.DOFade(0f, 0.2f);
            }
        }

        private void HandleHoldProgress(float progress)
        {
            if (m_ProgressBar != null)
            {
                m_ProgressBar.fillAmount = progress;
            }
        }

        #endregion
    }
}