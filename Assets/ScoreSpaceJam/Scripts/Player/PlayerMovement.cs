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
        private Vector3 _previousInput;

        void Start()
        {
            if (input == null)
            {
                Debug.Log("[PlayerMovement]".Bold() +" Missing Dependencies", this);
                return;
            }
            
            input.actions["Movement"].performed += SetMovement;
            input.actions["Movement"].canceled += ResetMovement;
        }

        private void OnDisable()
        {
            input.actions["Movement"].performed -= SetMovement;
            input.actions["Movement"].canceled -= ResetMovement;
        }

        private void SetMovement(InputAction.CallbackContext ctx)
        {
            _previousInput = ctx.ReadValue<Vector2>();
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
