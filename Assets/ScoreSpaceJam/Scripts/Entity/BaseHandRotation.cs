using UnityEngine;

namespace ScoreSpaceJam.Scripts.Entity
{
    public abstract class BaseHandRotation : MonoBehaviour
    {
        [SerializeField] private Transform hand;
    
        [SerializeField] private Transform flip;
        private Vector3 _defaultScale;
    
        void Start()
        {
            _defaultScale = flip.localScale;
            SetUpControls();
        }
        
        protected virtual void SetUpControls() { }

        protected void Rotate(Vector3 target)
        {
            var dir = target - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
            bool reverse = angle > 90 || angle < -90;
        
            if (reverse) {
                angle += 180f; // Reverse the angle for the flip effect
            }
        
            hand.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            flip.localScale = new Vector3(_defaultScale.x * (reverse ? -1 : 1), _defaultScale.y, _defaultScale.z);
        }
    }
}
