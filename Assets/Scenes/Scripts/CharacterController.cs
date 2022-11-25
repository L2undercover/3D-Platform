using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour

{

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float maxSpeed;
    public float normalSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    public float rotation = 0.0f;
    public float camRotation = 0.0f;
    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f;
    GameObject cam;
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;

    void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = sprintSpeed;
        } else
        {
            maxSpeed = normalSpeed;
        }

        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation - Input.GetAxis("Mouse Y") * camRotationSpeed;
        camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
    }
}
