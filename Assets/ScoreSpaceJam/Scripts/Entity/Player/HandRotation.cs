using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Entity.Player
{
    public class HandRotation : BaseHandRotation
    {
        [SerializeField] private PlayerInput input;

        protected override void Initialize()
        {
            input.actions["Rotation"].performed += OnRotate;
        }

        private void OnRotate(InputAction.CallbackContext ctx)
        {
            Vector3 mousePos = ctx.ReadValue<Vector2>();
        
            Rotate(mousePos);
        }
    }
}
