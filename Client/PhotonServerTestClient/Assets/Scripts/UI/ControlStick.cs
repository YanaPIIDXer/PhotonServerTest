using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Game.UI
{
    /// <summary>
    /// 制御スティック
    /// </summary>
    public class ControlStick : MonoBehaviour
    {
        /// <summary>
        /// バーチャルスティック
        /// </summary>
        private FixedJoystick VirtualStick = null;

        /// <summary>
        /// 入力量プロパティ
        /// </summary>
        private ReactiveProperty<Vector2> InputProperty = new ReactiveProperty<Vector2>(Vector2.zero);

        /// <summary>
        /// 入力
        /// </summary>
        public IObservable<Vector2> OnInput { get { return InputProperty; } }

        void Awake()
        {
            VirtualStick = GetComponent<FixedJoystick>();
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            Vector2 KeyInput = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                KeyInput.y = 1.0f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                KeyInput.y = -1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                KeyInput.x = 1.0f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                KeyInput.x = -1.0f;
            }
            VirtualStick.ForceMoveHandle(KeyInput.normalized);
#endif
            Vector2 StickInput = new Vector2(VirtualStick.Horizontal, VirtualStick.Vertical);
            InputProperty.Value = StickInput;
        }
    }
}
