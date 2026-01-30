using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// A simple First Person Controller for testing the Interaction System.
    /// Moves the player with WASD and rotates the view with the Mouse.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class SimpleFPSController : MonoBehaviour
    {
        #region Fields

        [Header("Movement Settings")] [Tooltip("Walking speed of the player.")] [SerializeField]
        private float m_MoveSpeed = 5.0f;

        [Header("Look Settings")] [Tooltip("Mouse sensitivity for camera rotation.")] [SerializeField]
        private float m_MouseSensitivity = 2.0f;

        [Tooltip("The camera transform attached to the player.")] [SerializeField]
        private Transform m_CameraTransform;

        // Private internal fields
        private CharacterController m_CharacterController;
        private float m_VerticalRotation = 0f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (m_CameraTransform == null)
            {
                m_CameraTransform = GetComponentInChildren<Camera>()?.transform;
                if (m_CameraTransform == null)
                {
                    Debug.LogError(
                        "[SimpleFPSController] No Camera found inside Player! Please assign Camera Transform.");
                }
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        #endregion

        #region Methods

        private void HandleMovement()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            m_CharacterController.Move(move * (m_MoveSpeed * Time.deltaTime));
        }

        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            m_VerticalRotation -= mouseY;
            m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, -90f, 90f);

            if (m_CameraTransform != null)
            {
                m_CameraTransform.localRotation = Quaternion.Euler(m_VerticalRotation, 0f, 0f);
            }
        }

        #endregion
    }
}