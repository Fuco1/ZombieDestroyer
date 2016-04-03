using UnityEngine;

public class SMG : Weapon {

    public override string GetName() {
        return "SMG";
    }

    public override BulletManager.BulletType GetBulletType() {
        return BulletManager.BulletType.RifleBullet;
    }

    public override void Shoot() {
        var position = wielder.transform.position + wielder.transform.forward * 2f;
        var rotation = wielder.transform.rotation * Quaternion.Euler(90, 0, 0);
        var bullet = GameObject.Instantiate<Bullet>(this.bullet);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Rigidbody>().velocity = wielder.transform.forward * 40f;
    }
}
