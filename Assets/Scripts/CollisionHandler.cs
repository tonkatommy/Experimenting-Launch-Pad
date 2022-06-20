using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float reloadDelay = 1f;
  [SerializeField] float nextLevelDelay = 1f;
  [SerializeField] AudioClip crashSFX;
  [SerializeField] AudioClip successSFX;
  AudioSource audioSource;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;
  bool isTransitioning = false;
  bool collisionDisabled = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    RespondToDebugKeys();
  }

  void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.R))
    {
      ReloadLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionDisabled = !collisionDisabled;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionDisabled) return;

    switch (other.gameObject.tag)
    {
      case "Friendly":
        {
          // Debug.Log("Friendly thing!");
          break;
        }

      case "Finish":
        {
          FinishSequence();
          break;
        }

      default:
        {
          StartCrashSequence();
          break;
        }
    }
  }

  void StartCrashSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crashSFX);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", reloadDelay);
  }

  void FinishSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(successSFX);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", nextLevelDelay);
  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}
