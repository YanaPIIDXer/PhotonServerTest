using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
