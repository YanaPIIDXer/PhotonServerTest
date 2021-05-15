using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using System;
using UniRx;

namespace Game.Sequence
{
    /// <summary>
    /// ゲームシーケンス
    /// </summary>
    public class GameSequence : MonoBehaviour
    {
        void Awake()
        {
            var Handler = UIManager.Show<ControlStick>("Game/PlayerMoveInput", ECanvas.Middle);
            Handler.UIComponent.OnInput.Subscribe((Value) => Debug.Log(Value));
        }
    }
}
