using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Player
{
    /// <summary>
    /// Simple inventory system to hold keys and other items.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        #region Fields

        [Tooltip("List of key IDs currently held by the player.")] [SerializeField]
        private List<string> m_Keys = new List<string>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a key to the inventory.
        /// </summary>
        /// <param name="keyID">The unique ID of the key.</param>
        public void AddKey(string keyID)
        {
            if (!m_Keys.Contains(keyID))
            {
                m_Keys.Add(keyID);
                Debug.Log($"[Inventory] Picked up key: {keyID}");
            }
        }

        /// <summary>
        /// Checks if the inventory contains a specific key.
        /// </summary>
        /// <param name="keyID">The ID of the key to check.</param>
        /// <returns>True if the key is in inventory.</returns>
        public bool HasKey(string keyID)
        {
            return m_Keys.Contains(keyID);
        }

        #endregion
    }
}