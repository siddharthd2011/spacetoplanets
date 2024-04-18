using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem Thrusting;
    [SerializeField] ParticleSystem FirstThrusting;
    [SerializeField] ParticleSystem SecondThrusting;
    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        if (!Thrusting.isPlaying)
        {
            Thrusting.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftThrusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightThrusing();

        }
        else
        {
            StopRotate();
        }

    }

    void ApplyRotation(float rataionThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rataionThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StartRightThrusing()
    {
        ApplyRotation(-rotationThrust);
        if (!SecondThrusting.isPlaying)
        {
            SecondThrusting.Play();
        }
    }

    void StartLeftThrusting()
    {
        ApplyRotation(rotationThrust);
        if (!FirstThrusting.isPlaying)
        {
            FirstThrusting.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        Thrusting.Stop();
    }

    void StopRotate()
    {
        FirstThrusting.Stop();
        SecondThrusting.Stop();
    }

}

