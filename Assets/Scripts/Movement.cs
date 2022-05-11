using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform orientation;
    [SerializeField] float moveSpeed;
    [SerializeField] float swimSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float speedAcceleration = 10;
    [SerializeField] float speedMultiplier;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform mouseCam;
    [SerializeField] float sensX;
    [SerializeField] public static float goalFishesEaten = 10;
    [SerializeField] public static float currFishEaten;
    Vector3 moveDirection;
    float groundDistance = 0.2f;
    float horizontalMovement;
    float verticalMovement;
    float mouseX;
    float yRotation;
    string fishTag = "Fish";

    void Update() {
        if (!MainMenu.isPaused) {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            ControlDrag();
            MyInput();
            MouseInput();
            Moving();
        }else {
            moveSpeed = 0;
        }
    }

    private void ControlDrag() {
        if (isGrounded) {
            rb.drag = 6;
        }else {
            rb.drag = 1;
        }
    }

    private void MyInput() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    private void Moving() {
        if (isGrounded) {
            if (Input.GetKey(sprintKey) && Input.GetKey(forwardKey)) {
                Sprint();
            }else {
                Swim();
            }
        }
    }

    private void Swim() {
        moveSpeed = swimSpeed * speedMultiplier;
    }

    private void Sprint() {
        moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed * speedMultiplier, speedAcceleration * Time.deltaTime);
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        if (isGrounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Acceleration);
        }else {
            rb.AddForce(moveDirection.normalized * moveSpeed * 0.4f, ForceMode.Acceleration);
        }
    }

    void MouseInput() {
        mouseX = Input.GetAxisRaw("Mouse X");
        yRotation += mouseX * sensX * 0.01f;
        mouseCam.rotation = Quaternion.Lerp(mouseCam.rotation, Quaternion.Euler(0, yRotation, 0), Time.deltaTime * 50);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
