using UnityEngine;

public class KillFloor : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.GetComponent<Zombie>()) {
            collider.gameObject.GetComponent<Zombie>().Die();
        }
        Destroy(collider.gameObject);
    }

}
