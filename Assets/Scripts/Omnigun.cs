using UnityEngine;
using System.Collections;

public class Omnigun : Weapon {

    public override string GetName() {
        return "Omnigun";
    }

    public override BulletManager.BulletType GetBulletType() {
        return BulletManager.BulletType.RifleBullet;
    }

    public override void Shoot() {
        for (int i = 0; i < 15; i++) {
            var direction = Quaternion.Euler(0, Random.Range(0, 360), 0);
            var forward = direction * Vector3.forward;
            var position = wielder.transform.position + forward * Random.Range(1.8f, 2.4f);
            var rotation = direction * Quaternion.Euler(90, 0, 0);
            var bullet = GameObject.Instantiate<Bullet>(this.bullet);
            bullet.damage = 2;
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;

            bullet.GetComponent<Rigidbody>().velocity = forward * 40f;
        }
    }
}
