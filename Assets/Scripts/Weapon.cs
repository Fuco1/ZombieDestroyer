using UnityEngine;

public abstract class Weapon {

    public Transform wielder;
    public Bullet bullet;

    public abstract void Shoot();
    public abstract string GetName();
    public abstract BulletManager.BulletType GetBulletType();

    public virtual float GetReloadTime() {
        return 0f;
    }
}
