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
        
        private bool IsMouseButtonDown(Touch touch) =>  touch.phase == TouchPhase.Began;
        private bool IsMouseHeld(Touch touch) => touch.phase == TouchPhase.Moved;
        private bool IsMouseButtonUp(Touch touch) => touch.phase == TouchPhase.Ended;
        public float IsMouseX(Touch touch) => touch.deltaPosition.x;
        
        public void HandleMouseInput()
        {
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);
            
            if (IsPointerOverUIElement(touch))
            {
                canMove = false;
                isMouseDown = false;
                return;
            }
            
            if (IsMouseButtonDown(touch))
            {
                canMove = false;
                isMouseDown = true;
            }
            
            if (IsMouseHeld(touch) && isMouseDown)
            {
                canMove = true;
            }
            
            if (IsMouseButtonUp(touch))
            {
                canMove = false;
                isMouseDown = false;
            }
        }

        private bool IsPointerOverUIElement(Touch touch)
        {
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }
    }
}