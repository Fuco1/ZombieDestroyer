using UnityEngine;

public class Minigun : Weapon {

    public override string GetName() {
        return "Minigun";
    }

    public override BulletManager.BulletType GetBulletType() {
        return BulletManager.BulletType.RifleBullet;
    }

    public override void Shoot() {
        for (int i = 0; i < 5; i++) {
            var position = wielder.transform.position +
                wielder.transform.forward * Random.Range(1.8f, 2.4f);
            var rotation = wielder.transform.rotation * Quaternion.Euler(90, 0, 0);
            var bullet = GameObject.Instantiate<Bullet>(this.bullet);
            bullet.damage = 5;
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            var forward = Quaternion.Euler(0, Random.Range(-3, 3), 0) * wielder.transform.forward;
            bullet.GetComponent<Rigidbody>().velocity = forward * 40f;
        }
    }
}
