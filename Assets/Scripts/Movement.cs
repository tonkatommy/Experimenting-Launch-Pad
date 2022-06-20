using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float mainRotation = 100f;
  [SerializeField] AudioClip thrustSFX;
  [SerializeField] ParticleSystem mainThrustFront;
  [SerializeField] ParticleSystem mainThrustBack;
  [SerializeField] ParticleSystem mainThrustLeft;
  [SerializeField] ParticleSystem mainThrustRight;

  bool thrusting = false;
  Rigidbody rocketRigidbody;
  AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
    rocketRigidbody = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();
    ProcessAudio();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      StartMainThrusters();
    }
    else
    {
      StopMainThrusters();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      RotateLeft();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      RotateRight();
    }
    else
    {
      StopRotation();
    }
  }

  private void ProcessAudio()
  {
    if (thrusting)
    {
      PlayThrustAudio();
    }
    else
    {
      audioSource.Stop();
    }
  }

  void StartMainThrusters()
  {
    thrusting = true;
    rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

    if (!mainThrustFront.isPlaying)
    {
      mainThrustFront.Play();
    }
    if (!mainThrustBack.isPlaying)
    {
      mainThrustBack.Play();
    }
  }

  private void StopMainThrusters()
  {
    thrusting = false;
    mainThrustFront.Stop();
    mainThrustBack.Stop();
    // mainThrustLeft.Stop();
    // mainThrustRight.Stop();
  }

  private void RotateLeft()
  {
    thrusting = true;
    ApplyRotation(mainRotation);
    if (!mainThrustRight.isPlaying)
    {
      mainThrustRight.Play();
    }
  }

  private void RotateRight()
  {
    thrusting = true;
    ApplyRotation(-mainRotation);
    if (!mainThrustLeft.isPlaying)
    {
      mainThrustLeft.Play();

    }
  }

  private void StopRotation()
  {
    if (!Input.GetKey(KeyCode.Space))
    {
      thrusting = false;
    }
    mainThrustLeft.Stop();
    mainThrustRight.Stop();
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rocketRigidbody.freezeRotation = true; // freezing rotation to manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rocketRigidbody.freezeRotation = false; // unfreezing rotation for the physics sytem
  }

  private void PlayThrustAudio()
  {
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(thrustSFX);
    }
  }
}
