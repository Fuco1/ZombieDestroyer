using System;
using UnityEngine;

public class Player : MonoBehaviour {

    public CharacterController controller;
    public Camera MainCamera;

    public float speed = 5f;
    public float gravity = 9.8f;
    public float jumpSpeed = 10f;
    public int life = 100;

    public Vector3 velocity = Vector3.zero;
    public bool canJump = false;

    public bool canShoot = true;

    private Weapon weapon;

    void OnGUI() {
        GUI.Label(new Rect(10, 20, 100, 20), "Life: " + life);
    }

    void Start() {
        EquipWith(new RocketLauncher());
    }

    public void EquipWith(Weapon weapon) {
        this.weapon = weapon;
        weapon.wielder = transform;
        weapon.bullet = FindObjectOfType<BulletManager>().getBullet(weapon.GetBulletType());
    }

    void Update() {
        var lookDirection = RotateToCamera();
        var moveVelocity = GetMoveVelocity(lookDirection);
        moveVelocity *= speed;

        if (Input.GetButtonDown("Jump") && canJump) {
            moveVelocity.y += jumpSpeed;
        }

        velocity.x = moveVelocity.x;
        velocity.y = moveVelocity.y + velocity.y - gravity * Time.deltaTime;
        velocity.z = moveVelocity.z;

        var flags = controller.Move(velocity * Time.deltaTime);

        if ((flags & (CollisionFlags.Below)) != 0) {
            canJump = true;
            velocity.y = -3f;
        } else {
            canJump = false;
        }

        if (Input.GetButton("Fire1")) {
            FireWeapon();
        }
    }

    private void FireWeapon() {
        if (!canShoot) return;
        weapon.Shoot();
        var reload = weapon.GetReloadTime();
        if (reload >= 0.001) {
            canShoot = false;
            Invoke("ToggleShooting", reload);
        }
    }

    private void ToggleShooting() {
        canShoot = true;
    }

    private Vector3 GetMoveVelocity(Vector3 lookDirection) {
        var vector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        vector = Vector3.ClampMagnitude(vector, 1f);
        vector = GetCameraRotation() * vector;

        // add speed penalty for going backwards
        var penalty = ((Vector3.Dot(lookDirection.normalized, vector.normalized) + 1f) / 4f) + 0.5f;
        vector *= penalty;
        return vector;
    }

    private Quaternion GetCameraRotation() {
        var onPlane = Vector3.ProjectOnPlane(MainCamera.transform.forward, Vector3.up);
        return Quaternion.LookRotation(onPlane, Vector3.up);
    }

    private Vector3 RotateToCamera() {
        var mouse = new Vector3(0, 0, 0);
        mouse.x = Input.mousePosition.x;
        mouse.z = Input.mousePosition.y;
        var wp = MainCamera.WorldToScreenPoint(transform.position);
        wp.z = wp.y;
        wp.y = 0;
        var lookDirection = mouse - wp;
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        return lookDirection;
    }
}
