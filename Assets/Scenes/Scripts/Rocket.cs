using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 120f;
    [SerializeField] float rcsThrust = 120f;

    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip completionSound;

    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem completionParticle;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            ThrustInputVerification();
            RotationInputVerification();
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
                StartNextSceneSequence();
                break;
            default:
                StartDeadSequence();
                break;
        }
    }

    private void StartDeadSequence()
    {
        print("Hit something deadly.");
        state = State.Dying;

        audioSource.Stop();
        thrustParticle.Stop();

        deathParticle.Play();
        audioSource.PlayOneShot(this.deathSound);

        Invoke("LoadFirstScene", 1f);
    }

    private void StartNextSceneSequence()
    {
        print("Finished the level.");
        state = State.Transcending;

        audioSource.Stop();
        thrustParticle.Stop();

        completionParticle.Play();
        audioSource.PlayOneShot(this.completionSound);

        Invoke("LoadNextScene", 1f);
    }

    private void LoadNextScene()
    {
        audioSource.Stop();
        completionParticle.Stop();
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        audioSource.Stop();
        deathParticle.Stop();
        SceneManager.LoadScene(0);
    }

    private void ThrustInputVerification()
    {
        rigidBody.freezeRotation = true; // Avoid sticking forces to the ship

        if (Input.GetKey(KeyCode.Space))
        {
            ThrustRocket();
        }
        else
        {
            audioSource.Stop();
            thrustParticle.Stop();
        }

        rigidBody.freezeRotation = false; // Avoid sticking forces to the ship
    }

    private void ThrustRocket()
    {
        float thrustSpeedForFrame = Time.deltaTime * mainThrust;
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeedForFrame);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
            thrustParticle.Play();
        }
    }

    private void RotationInputVerification()
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
