using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 10f;
        [SerializeField] private PlayerInput input;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Transform flip;
        private Vector3 _previousInput;
        private Vector3 _defaultScale;

        void Start()
        {
            if (input == null)
            {
                Debug.Log("[Player Movement]".Bold() +" Missing Dependencies", this);
                return;
            }
            
            input.actions["Movement"].performed += SetMovement;
            input.actions["Movement"].canceled += ResetMovement;

            _defaultScale = flip.localScale;
        }

        private void OnDisable()
        {
            input.actions["Movement"].performed -= SetMovement;
            input.actions["Movement"].canceled -= ResetMovement;
        }

        private void SetMovement(InputAction.CallbackContext ctx)
        {
            _previousInput = ctx.ReadValue<Vector2>();
        
            // Flip player
            if (_previousInput.x == 0) return;
            
            flip.localScale = new Vector3(_defaultScale.x * _previousInput.x > 0 ? -1 : 1, _defaultScale.y, _defaultScale.z);
        }

        void FixedUpdate()
        {
            rb2d.velocity = _previousInput * speedMultiplier;
        }

        private void ResetMovement(InputAction.CallbackContext obj)
        {
            _previousInput = Vector3.zero;
            Vector3 vel = rb2d.velocity;

            vel = new Vector2(vel.x * 0f, vel.y * 0f);

            rb2d.velocity = vel;
        }
    }
}
