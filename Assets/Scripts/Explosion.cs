using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private bool isExploding = false;

    public void Explode() {
        isExploding = true;
    }

    public void Update() {
        if (isExploding) {
            transform.localScale += Vector3.one * 12f * Time.deltaTime;
            if (transform.localScale.x > 5f) {
                Destroy(gameObject);
            }
            var light = GetComponentInChildren<Light>();
            light.range += 2f;
        }
    }
}
