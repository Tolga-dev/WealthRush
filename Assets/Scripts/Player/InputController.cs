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
        
        private bool IsMouseButtonDown() => Input.GetMouseButtonDown(0);
        private bool IsMouseHeld() => Input.GetMouseButton(0);
        private bool IsMouseButtonUp() => Input.GetMouseButtonUp(0);
        public float IsMouseX() => Input.GetAxis("Mouse X");

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