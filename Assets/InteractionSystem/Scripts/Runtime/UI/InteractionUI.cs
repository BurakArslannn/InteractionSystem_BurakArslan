using DG.Tweening;
using InteractionSystem.Scripts.Runtime.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks; // Async beklemeler için

namespace InteractionSystem.Scripts.Runtime.UI
{
    /// <summary>
    /// Manages the Heads-Up Display (HUD) feedback using the Observer Pattern.
    /// Listens to InteractionDetector events to trigger DOTween animations for Prompts, Progress Bars, and Status Messages.
    /// </summary>
    public class InteractionUI : MonoBehaviour
    {
        #region Fields

        [Header("UI References")] [SerializeField]
        private TextMeshProUGUI m_PromptText;

        [SerializeField] private Image m_ProgressBar;
        [SerializeField] private CanvasGroup m_CanvasGroup;

        [Header("Feedback Colors")] [SerializeField]
        private Color m_NormalColor = Color.white;

        [SerializeField] private Color m_ErrorColor = Color.red;
        [SerializeField] private Color m_SuccessColor = Color.green;

        [Header("System")] [SerializeField] private InteractionDetector m_Detector;

        private bool m_IsShowingFeedback;

        #endregion

        #region Setup

        private void Start()
        {
            if (m_Detector == null) m_Detector = FindFirstObjectByType<InteractionDetector>();

            // Event Subscription (Observer Pattern)
            if (m_Detector != null)
            {
                m_Detector.OnInteractableFound += HandleInteractableFound;
                m_Detector.OnHoldProgress += HandleHoldProgress;
                m_Detector.OnInteractionFeedback += HandleFeedback;
            }

            InitUI();
        }

        private void OnDestroy()
        {
            if (m_Detector != null)
            {
                m_Detector.OnInteractableFound -= HandleInteractableFound;
                m_Detector.OnHoldProgress -= HandleHoldProgress;
                m_Detector.OnInteractionFeedback -= HandleFeedback;
            }
        }

        private void InitUI()
        {
            if (m_CanvasGroup) m_CanvasGroup.alpha = 0f;
            if (m_ProgressBar) m_ProgressBar.fillAmount = 0f;
            if (m_PromptText) m_PromptText.color = m_NormalColor;
        }

        #endregion

        #region Event Handlers

        private void HandleInteractableFound(bool isFound, string promptText)
        {
            if (m_IsShowingFeedback) return;

            if (m_CanvasGroup == null) return;
            m_CanvasGroup.DOKill();

            if (isFound)
            {
                m_PromptText.text = promptText;
                m_PromptText.color = m_NormalColor;
                m_CanvasGroup.DOFade(1f, 0.2f);
            }
            else
            {
                m_CanvasGroup.DOFade(0f, 0.2f);
            }
        }

        private void HandleHoldProgress(float progress)
        {
            if (m_ProgressBar) m_ProgressBar.fillAmount = progress;
        }

        private async void HandleFeedback(string message, bool isError)
        {
            m_IsShowingFeedback = true;

            m_CanvasGroup.alpha = 1f;
            m_PromptText.text = message;
            m_PromptText.color = isError ? m_ErrorColor : m_SuccessColor;

            if (isError)
            {
                m_PromptText.transform.DOShakePosition(0.5f, 10f);
            }
            else
            {
                m_PromptText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
            }

            await UniTask.Delay(1500, cancellationToken: this.GetCancellationTokenOnDestroy());

            m_IsShowingFeedback = false;
            m_PromptText.color = m_NormalColor;

            if (m_CanvasGroup.alpha > 0.9f && !m_IsShowingFeedback)
            {
                m_CanvasGroup.DOFade(0f, 0.5f);
            }
        }

        #endregion
    }
}