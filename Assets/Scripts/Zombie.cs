using UnityEngine;

public class Zombie : MonoBehaviour {

    public Transform target;
    public float speed = 0.5f;
    public GameObject bloodSplatter;

    private GameManager gm;

    private int life = 5;
    private int damage = 1;
    private bool processed = false;
    private bool canAttack = true;

    void Start() {
        gm = GameObject.FindObjectOfType<GameManager>();
        float toughness = Random.value;
        Color color = Color.white;
        if (toughness <= 0.01) {
            color = Color.red;
            life = 100;
            damage = 4;
            speed = 3;
        } else if (toughness <= 0.05) {
            color = new Color(1,0.5f,0,1);
            life = 50;
            damage = 2;
            speed = 2;
        } else if (toughness <= 0.15) {
            color = Color.yellow;
            life = 20;
            speed = 1;
        }

        foreach (var renderer in GetComponentsInChildren<Renderer>()){
            renderer.material.color = color;
        }
    }

    void Update () {
        var point = target.position - transform.position;
        point.y = 0;
        transform.rotation = Quaternion.LookRotation(point, Vector3.up);
        transform.Translate(Vector3.forward.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider) {
        var collidingObject = collider.gameObject;
        if (collidingObject.GetComponent<Bullet>()) {
            var bullet = collidingObject.GetComponent<Bullet>();
            if (bullet.CanHit) {
                life -= bullet.damage;
                bullet.CanHit = false;
            }
        }

        if (collidingObject.GetComponent<Explosion>()) {
            life -= 100;
        }

        if (life <= 0 && !processed) {
            processed = true;
            Die();
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.GetComponent<Player>() && canAttack) {
            var player = collision.gameObject.GetComponent<Player>();
            // TODO: call a "hit" method on Player
            player.life -= damage;
            canAttack = false;
            Invoke("ToggleCanAttack", 1);
        }
    }

    private void ToggleCanAttack() {
        canAttack = true;
    }

    public void Die() {
        gm.OnZombieDie(this);
        var bloodPosition = new Vector3(transform.position.x, 1.18f, transform.position.z);
        var bloodRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0) * Quaternion.Euler(90f, 0, 0);
        Instantiate(bloodSplatter, bloodPosition, bloodRotation);
        Destroy(gameObject);
    }
}
