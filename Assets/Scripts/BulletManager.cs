using UnityEngine;

public class BulletManager : MonoBehaviour {

    public enum BulletType {
        RifleBullet,
        Rocket,
    };

    public Bullet rifleBullet;
    public Bullet rocket;

    public Bullet getBullet(BulletType type) {
        switch (type) {
            case BulletType.RifleBullet:
                return rifleBullet;
            case BulletType.Rocket:
                return rocket;
        }
        return null;
    }
}
