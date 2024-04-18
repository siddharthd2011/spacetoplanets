using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Collsionhander : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Success;

    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem SuccessParticle;

    AudioSource audioSource;

    bool isTranistioning = false;
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
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTranistioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }

    void StartSuccessSequence()
    {
        isTranistioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", levelLoadDelay);

    }

    void StartCrashSequence()
    {
        isTranistioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        DeathParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }



    void loadNextLevel()
    {
        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScenceIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScenceIndex);
    }


}
