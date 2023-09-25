namespace ScoreSpaceJam.Scripts.Entity.Enemy
{
    public class EnemyGun : BaseGun
    {
        public void RegisterBulletPool(ObjectPool pool)
        {
            bulletPool = pool;
        }
    }
}
