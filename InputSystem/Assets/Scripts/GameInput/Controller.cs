using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour, Controls.IMainActions {

    [SerializeField] private float rotationSpeed = 90;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float bulletSpeed = 2;

    private Controls _controls;


    private void Awake() {
        _controls = new Controls();
        _controls.Main.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context) {
        var delta = context.ReadValue<Vector2>();
        transform.Translate(Time.deltaTime * movementSpeed * delta);
    }

    public void OnRotate(InputAction.CallbackContext context) {
        var rotation = context.ReadValue<float>();
        transform.Rotate(Vector3.back, rotation * Time.deltaTime * rotationSpeed);
    }

    public void OnShoot(InputAction.CallbackContext context) {
        var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.GetComponent<Collider>().enabled = false;
        var t = bullet.transform;
        t.position = transform.position;
        t.rotation = transform.rotation;
        t.localScale = Vector3.one * 0.2f;
        var r = bullet.AddComponent<Rigidbody>();
        r.useGravity = false;
        r.AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        GameObject.Destroy(bullet, 3f);
    }

    private void OnEnable() {
        _controls.Main.Enable();
    }

    private void OnDisable() {
        _controls.Main.Disable();
    }
}
