using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// A lightweight data container component for the Player.
    /// Stores a list of collected Key IDs to determine access rights for locked interactables.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        #region Fields

        [Tooltip("List of key IDs currently held by the player.")] [SerializeField]
        private List<string> m_Keys = new List<string>();

        #endregion

        #region Public Methods

        public void AddKey(string keyID)
        {
            if (!m_Keys.Contains(keyID))
            {
                m_Keys.Add(keyID);
            }
        }

        public bool HasKey(string keyID)
        {
            return m_Keys.Contains(keyID);
        }

        #endregion
    }
}