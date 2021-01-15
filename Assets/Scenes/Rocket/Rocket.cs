using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource shipThruster;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        shipThruster = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!shipThruster.isPlaying)
            {
                shipThruster.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up);
        }
        else
        {
            shipThruster.Stop();
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-Vector3.left);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.left);
        }
    }
}
