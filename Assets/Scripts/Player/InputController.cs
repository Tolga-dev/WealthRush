using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Player
{
    [Serializable]
    public class InputController
    {
        public bool isMouseDown;
        public bool canMove;
        
#if UNITY_EDITOR
        private bool IsMouseButtonDown() => Input.GetMouseButtonDown(0);
        private bool IsMouseHeld() => Input.GetMouseButton(0);
        private bool IsMouseButtonUp() => Input.GetMouseButtonUp(0);
        public float IsMouseX() => Input.GetAxis("Mouse X");

#elif UNITY_ANDROID
        private bool IsMouseButtonDown() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        private bool IsMouseHeld() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        private bool IsMouseButtonUp() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        public float IsMouseX() => Input.touchCount > 0 ? Input.GetTouch(0).deltaPosition.x : 0f;
#endif


        public void HandleMouseInput()
        {
            if (IsPointerOverUIElement())
            {
                canMove = false;
                isMouseDown = false;
                return;
            }
            if (IsMouseButtonDown())
            {
                canMove = false;
                isMouseDown = true;
            }
            
            if (IsMouseHeld() && isMouseDown)
            {
                canMove = true;
            }
            
            if (IsMouseButtonUp() && isMouseDown)
            {
                canMove = false;
                isMouseDown = false;
            }
        }

        private bool IsPointerOverUIElement()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}