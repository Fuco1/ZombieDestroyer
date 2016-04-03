using UnityEngine;
using System.Collections;

public class RocketBullet : Bullet {

    public Explosion explosion;

    protected override void KillSelf() {
        var explosion = Instantiate<Explosion>(this.explosion);
        var position = transform.position;
        position.y = 1.5f;
        explosion.transform.position = position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.Explode();
        base.KillSelf();
    }
}
