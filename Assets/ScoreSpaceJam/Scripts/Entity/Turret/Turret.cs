using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Entity.Turret
{
    public class Turret : MonoBehaviour
    {
        [HideInInspector] public GameManager manager;
        
        [SerializeField] private TurretGun gun;
        [SerializeField] private TurretHandRotation handRot;

        public void RegisterGameManager(GameManager gm)
        {
            manager = gm;
            handRot.manager = gm;
            gun.manager = gm;
        }
    }
}
