using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterDesigner.UI.Scripts
{
    public class DragRotator : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Transform target;

        public Vector2 minAngle;

        public Vector2 maxAngle;

        private Vector3 startEuler;
    
        private Vector2 startPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = eventData.position;
            startEuler = target.localEulerAngles;
            startEuler = new Vector3(
                startEuler.x, 
                startEuler.y, 
                startEuler.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            doDrag(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            doDrag(eventData.position);
        }

        private void doDrag(Vector2 currentPosition)
        {
            var vector = (currentPosition - startPosition) / new Vector2(Screen.width, Screen.height) * 360;
            var newEulerAngles = new Vector3(
                ClampAngle(startEuler.x + vector.y), 
                ClampAngle(startEuler.y + vector.x), 
                startEuler.z);
            newEulerAngles = new Vector3(
                Mathf.Clamp(newEulerAngles.x, minAngle.x, maxAngle.x), 
                Mathf.Clamp(newEulerAngles.y, minAngle.y, maxAngle.y), 
                startEuler.z);
            target.localEulerAngles = newEulerAngles;
            startEuler = newEulerAngles;
            startPosition = currentPosition;
        }
    
        float ClampAngle(float angle) {
            if(angle < -180f)
                return angle + 360f;
            if(angle > 180f)
                return angle - 360f;
        
            return angle;
        }
    }
}
