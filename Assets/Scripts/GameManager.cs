using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Rect spawnRect;

    public Zombie zombie;
    public Transform player;
    public GameObject bloodSplatter;

    public Pickup weaponPickup;
    public Pickup healthPickup;
    public Pickup megaHealthPickup;

    private int wave = 0;
    private int remaining = 0;
    private Text waveText;

    private int kills;

    void Start() {
        var bounds = GameObject.Find("Ground").GetComponent<Renderer>().bounds;
        spawnRect = new Rect(bounds.min.x, bounds.min.z,
                             bounds.size.x, bounds.size.z);
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        StartNextWave();
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 0, 100, 20), "Killcount: " + kills);
        GUI.Label(new Rect(10, 10, 100, 20), "Remaining: " + remaining);
    }

    private void StartNextWave() {
        wave++;
        for (int i = 0; i < NumberOfZombiesForWave(wave); i++) {
            SpawnNewZombie();
        }
        remaining = NumberOfZombiesForWave(wave);

        waveText.text = "Wave " + wave + "!";
        waveText.enabled = true;
        Invoke("DisableText", 3);
    }

    private int NumberOfZombiesForWave(int wave) {
        return wave * wave * 15;
    }

    private void DisableText() {
        waveText.enabled = false;
    }

    public void OnZombieDie(Zombie deadZombie) {
        kills++;
        remaining--;
        if (remaining == 1) {
            if (wave == 1) {
                SpawnWeaponPickup(deadZombie.transform, new Minigun());
            } else {
                SpawnWeaponPickup(deadZombie.transform, new Omnigun());
            }
        }
        if (Random.value <= 0.01) {
            SpawnMegahealthPickup(deadZombie.transform);
        }
        if (Random.value <= 0.05) {
            SpawnHealthPickup(deadZombie.transform);
        }
        if (remaining == 0) {
            StartNextWave();
        }
    }

    private Pickup SpawnPickup(Transform target, Pickup pickupPrefab) {
        var pickup = Instantiate<Pickup>(pickupPrefab);
        var position = target.position;
        position.y = 1.8f;
        pickup.transform.position = position;
        pickup.GetComponent<HingeJoint>().connectedAnchor = position;
        return pickup;
    }

    private void SpawnWeaponPickup(Transform target, Weapon weapon) {
        var pickup = SpawnPickup(target, weaponPickup);
        pickup.GetComponent<Pickup>().updater = delegate(Player p) {
            p.EquipWith(weapon);
            return true;
        };
        pickup.GetComponentInChildren<TextMesh>().text = weapon.GetName();
    }

    private void SpawnHealthPickup(Transform target) {
        var pickup = SpawnPickup(target, healthPickup);
        pickup.GetComponent<Pickup>().updater = delegate(Player p) {
            if (p.life < 100) {
                p.life = System.Math.Min(p.life + 25, 100);
                return true;
            }
            return false;
        };
    }

    private void SpawnMegahealthPickup(Transform target) {
        var pickup = SpawnPickup(target, megaHealthPickup);
        pickup.GetComponent<Pickup>().updater = delegate(Player p) {
            p.life = System.Math.Min(p.life + 100, 200);
            return true;
        };
    }

    public void SpawnNewZombie() {
        var newPosition = new Vector3(0,10,0);//zombie.transform.position;
        newPosition.x = Random.Range(spawnRect.xMin, spawnRect.xMax);
        newPosition.z = Random.Range(spawnRect.yMin, spawnRect.yMax);
        var newZombie = Instantiate<Zombie>(zombie);
        newZombie.transform.position = newPosition;
        newZombie.target = player;
        newZombie.bloodSplatter = bloodSplatter;
        newZombie.enabled = true;
    }
}
