using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource shipThruster;
    [SerializeField] float mainThrust = 120f;
    [SerializeField] float rcsThrust = 120f;

    // Previous position
    Vector3 originalPosition;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        shipThruster = GetComponent<AudioSource>();
        this.originalPosition = this.transform.position;
        this.originalRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do something
                break;
            default:
                transform.SetPositionAndRotation(originalPosition, originalRotation);
                break;
        }
    }

    private void Thrust()
    {
        rigidBody.freezeRotation = true; // Avoid sticking forces to the ship

        if (Input.GetKey(KeyCode.Space))
        {
            if (!shipThruster.isPlaying)
            {
                shipThruster.Play();
            }
            float thrustSpeedForFrame = Time.deltaTime * mainThrust;
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeedForFrame);
        }
        else
        {
            shipThruster.Stop();
        }

        rigidBody.freezeRotation = false; // Avoid sticking forces to the ship
    }

    private void Rotate()
    {
        float rotationSpeedForFrame = Time.deltaTime * rcsThrust;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeedForFrame);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeedForFrame);
        }
    }
}
