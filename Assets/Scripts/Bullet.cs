using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool CanHit { get; set; }
    public int damage = 1;

    IEnumerator Start () {
        CanHit = true;
        yield return new WaitForSeconds(3);
        KillSelf();
    }

    void OnTriggerEnter(Collider collider) {
        if (!collider.gameObject.GetComponent<Bullet>() &&
            !collider.gameObject.GetComponent<Pickup>()) {
            KillSelf();
        }
    }

    protected virtual void KillSelf() {
        Destroy(gameObject);
    }
}
