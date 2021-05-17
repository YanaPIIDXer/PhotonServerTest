using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.ControlInput;

namespace Game.UI
{
    /// <summary>
    /// プレイヤー操作インタフェース
    /// </summary>
    public class ControlInputs : UIComponent
    {
        /// <summary>
        /// ZOrder
        /// </summary>
        public override EZOrder ZOrder => EZOrder.MainHUD;

        /// <summary>
        /// 移動用Input
        /// </summary>
        public IMoveInput MoveInput { get { return Stick; } }

        /// <summary>
        /// 移動スティック
        /// </summary>
        [SerializeField]
        private MoveStick Stick = null;
    }
}
