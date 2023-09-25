using System;
using System.Collections;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Bullets;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSpaceJam.Scripts.Entity.Enemy
{
    public class RangedEnemy : Enemy
    {
        [HideInInspector] public GameManager manager;

        [SerializeField] private EnemyGun gun;
        [SerializeField] private EnemyHandRotation handRot;

        public override void RegisterGameManager(GameManager gm)
        {
            base.RegisterGameManager(gm);
            handRot.manager = gm;
            gun.manager = gm;
        }

        public void RegisterBulletPool(ObjectPool bulletPool)
        {
            gun.RegisterBulletPool(bulletPool);
        }

        protected override Vector3 GetClosestTarget()
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos;

            if (Vector3.Distance(currentPos, _player.position) >
                Vector3.Distance(currentPos, _base.position))
            {
                targetPos = _base.position;
            }
            else targetPos = _player.position;

            return targetPos - ((targetPos - currentPos).normalized * handRot.Radius);
        }
    }
}
