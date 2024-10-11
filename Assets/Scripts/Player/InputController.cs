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
        
        /*private bool IsMouseButtonDown() => Input.GetMouseButtonDown(0);
        private bool IsMouseHeld() => Input.GetMouseButton(0);
        private bool IsMouseButtonUp() => Input.GetMouseButtonUp(0);
        public float IsMouseX() => Input.GetAxis("Mouse X");*/

        private bool IsMouseButtonDown() =>   Input.GetTouch(0).phase == TouchPhase.Began;
        private bool IsMouseHeld() =>   Input.GetTouch(0).phase == TouchPhase.Moved;
        private bool IsMouseButtonUp() =>   Input.GetTouch(0).phase == TouchPhase.Ended;
        public float IsMouseX() => Input.GetTouch(0).deltaPosition.x;

        
        public void HandleMouseInput()
        {
            if (Input.touchCount == 0) return;
            
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
            
            if (IsMouseButtonUp() &&  isMouseDown)
            {
                canMove = false;
                isMouseDown = false;
            }
        }

        private bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}