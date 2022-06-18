using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float mainRotation = 100f;
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
    ProcessTrust();
    ProcessRotation();
  }

  void ProcessTrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }
    }
    else
    {
      audioSource.Stop();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(mainRotation);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-mainRotation);
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
    rocketRigidbody.freezeRotation = true; // freezing rotation to manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rocketRigidbody.freezeRotation = false; // unfreezing rotation for the physics sytem
  }
}
