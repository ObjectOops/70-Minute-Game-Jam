using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Keyboard kb;
    private Mouse mouse;
    private Rigidbody rigidBody;
    private Animator firearmAnimator;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Vector3 projectileSpawnOffset;

    private float xDir = 0, yDir = 0;
    private Vector3 velocity;

    public float moveSpeed, mouseSensitivity, mouseYMax, mouseYMin;
    public float jumpForce, onGroundDistance, projectileForce;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
        firearmAnimator = transform.GetChild(0).GetChild(2).GetComponent<Animator>();
    }

    void Update()
    {
        kb = Keyboard.current;
        mouse = Mouse.current;

        Vector3 moveDir = Vector3.zero;

        if (kb.wKey.isPressed) moveDir += Vector3.forward;
        if (kb.sKey.isPressed) moveDir -= Vector3.forward;
        if (kb.aKey.isPressed) moveDir -= Vector3.right;
        if (kb.dKey.isPressed) moveDir += Vector3.right;

        moveDir = Quaternion.AngleAxis(yDir, Vector3.up) * moveDir;

        velocity = -1 * moveSpeed * moveDir;
        velocity.y = rigidBody.velocity.y;

        if (kb.escapeKey.wasPressedThisFrame)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        Vector2 mouseInput = mouse.delta.ReadValue();

        xDir -= mouseInput.y * mouseSensitivity;
        yDir += mouseInput.x * mouseSensitivity;

        xDir = Mathf.Clamp(xDir, mouseYMin, mouseYMax);

        transform.rotation = Quaternion.Euler(xDir, yDir, 0);

        if (kb.spaceKey.wasPressedThisFrame)
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = velocity;
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        Vector3 angles = transform.rotation.eulerAngles * Mathf.Deg2Rad;
        Vector3 force = new(Mathf.Sin(angles[1]),
                            Mathf.Sin(angles[0] * -1),
                            Mathf.Cos(angles[1]));
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = transform.position + new Vector3(projectileSpawnOffset.x * force[0],
                                                                     projectileSpawnOffset.y,
                                                                     projectileSpawnOffset.z * force[2]);
        force *= projectileForce;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        firearmAnimator.SetTrigger("fireNow");
        StartCoroutine(StopFirearmAnimation());
    }

    private IEnumerator StopFirearmAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        firearmAnimator.SetTrigger("fireStop");
    }
}
