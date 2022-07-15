using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xInput, yInput;
    [SerializeField] float speed, rotationPower, lerpToCameraPositionSpeed;
    [SerializeField] GameObject followTransform;
    Rigidbody rb;
    Vector3 look, forwardDirection, sidewaysDirection;
    bool isInput;
    [SerializeField] float angleA, angleB, angleC;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnGUI()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");
        followTransform.transform.rotation *= Quaternion.AngleAxis(look.x * rotationPower, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > angleA && angle < angleB)
        {
            angles.x = angleB;
        }
        else if (angle < angleA && angle > angleC)
        {
            angles.x = angleC;
        }


        followTransform.transform.localEulerAngles = angles;
        

        float yInputRaw = Input.GetAxisRaw("Vertical");
        isInput = yInputRaw == 0 ? false : true;

    }
    private void FixedUpdate()
    {
        print(rb.velocity.magnitude);
        forwardDirection = transform.forward * yInput;
        sidewaysDirection = transform.right * xInput;
        rb.velocity = Vector3.ClampMagnitude((forwardDirection + sidewaysDirection), 1f) * speed;
        Vector3 rotateTowards = followTransform.transform.rotation.eulerAngles;
        Vector3 rotateTowards2 = new Vector3(0f, rotateTowards.y, 0f);
        
        if (isInput)
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.Euler(rotateTowards2), lerpToCameraPositionSpeed);

    }
}
