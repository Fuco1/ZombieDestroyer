using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public delegate bool UpdatePlayer(Player player);
    public UpdatePlayer updater;
    /// after how many seconds should the powerup disappear
    public float timeout = 60f;

    IEnumerator Start() {
        yield return new WaitForSeconds(timeout);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.GetComponent<Player>()) {
            var player = collider.gameObject.GetComponent<Player>();
            if (updater(player)) {
                Destroy(gameObject);
            }
        }
    }
}
