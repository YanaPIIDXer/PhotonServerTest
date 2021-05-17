using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.ControlInput;
using System;
using UniRx;

namespace Game.UI
{
    /// <summary>
    /// 移動スティック
    /// </summary>
    public class MoveStick : MonoBehaviour, IMoveInput
    {
        /// <summary>
        /// 移動入力Observable
        /// </summary>
        public IObservable<Vector2> OnInputMove => InputValue;

        /// <summary>
        /// 入力値
        /// </summary>
        private ReactiveProperty<Vector2> InputValue = new ReactiveProperty<Vector2>();

        /// <summary>
        /// スティック
        /// </summary>
        private FixedJoystick Stick = null;

        void Awake()
        {
            Stick = GetComponent<FixedJoystick>();
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            float X = Input.GetAxis("Horizontal");
            float Y = Input.GetAxis("Vertical");
            Stick.ForceMoveHandle(new Vector2(X, Y).normalized);
#endif
            var InputVec = new Vector2(Stick.Horizontal, Stick.Vertical);
            InputValue.Value = InputVec;
        }
    }
}
