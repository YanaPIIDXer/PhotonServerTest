using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UI用Component
    /// </summary>
    public abstract class UIComponent : MonoBehaviour
    {
        /// <summary>
        /// ZOrder
        /// </summary>
        public abstract EZOrder ZOrder { get; }
    }
}
