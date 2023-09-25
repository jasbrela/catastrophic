namespace ScoreSpaceJam.Scripts.Entity.Turret
{
    public class TurretGun : BaseGun
    {
        public void RegisterGunBulletPool(ObjectPool pool)
        {
            bulletPool = pool;
        }
    }
}
