using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon {

    public override string GetName() {
        return "Rocket Launcher";
    }

    public override BulletManager.BulletType GetBulletType() {
        return BulletManager.BulletType.Rocket;
    }

    public override float GetReloadTime() {
        return 0.5f;
    }

    public override void Shoot() {
        var position = wielder.transform.position + wielder.transform.forward * 2f;
        var rotation = wielder.transform.rotation * Quaternion.Euler(90, 0, 0);
        var bullet = GameObject.Instantiate<Bullet>(this.bullet);
        bullet.damage = 100;
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<Rigidbody>().velocity = wielder.transform.forward * 40f;
    }
}
