using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform player;

    void LateUpdate () {
        transform.position = player.position;
        transform.Translate(0, 15, -4, Space.World);
    }
}
