using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource shipThruster;
    [SerializeField] float mainThrust = 120f;
    [SerializeField] float rcsThrust = 120f;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

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
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do something
                break;
            case "Finish":
                print("Finished the level.");
                state = State.Transcending;
                Invoke("loadNextScene", 1f);
                break;
            default:
                print("Hit something deadly.");
                state = State.Dying;
                Invoke("LoadFirstScene", 1f);
                break;
        }
    }

    private void loadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
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
