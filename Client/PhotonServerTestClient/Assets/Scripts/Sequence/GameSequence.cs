using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using System;
using UniRx;
using Game.Character.Player;

namespace Game.Sequence
{
    /// <summary>
    /// ゲームシーケンス
    /// </summary>
    public class GameSequence : MonoBehaviour
    {
        void Awake()
        {
            var PlayerPrefab = Resources.Load<GameObject>("Prefabs/System/Player");
            Debug.Assert(PlayerPrefab != null, "Player Prefab Load Failed.");

            var PlayerObj = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            Debug.Assert(PlayerObj != null, "Player Instantiate Failed.");

            var Player = PlayerObj.GetComponent<Player>();

            var InputHandler = UIManager.Show<ControlStick>("Game/PlayerMoveInput", ECanvas.Middle);
            Player.SetupLocalMoveementComponent(InputHandler.UIComponent.OnInput);
        }
    }
}
