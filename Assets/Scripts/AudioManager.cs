using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [Header("Audio Music")]
    public AudioSource mainMenuMusic; // Assign in Inspector
    public AudioSource gameMusic;     // Assign in Inspector

    [Header("Audio SFX")]
    [SerializeField] AudioSource SFXSource;

    public AudioClip menuSFX;
    public AudioClip CarSFX;
    public AudioClip ExplosionSFX;
    public AudioClip PowerUpSFX;
    public AudioClip VictorySFX;
    public AudioClip DefeatSFX;

    public AudioClip ThankYouSFX;

    [System.Obsolete]
    private void Awake()
    {
        // Prevent multiple instances of AudioManager in the scene
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Ensure the correct music plays when the scene starts
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene load event when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void PlayMainMenuMusic()
    {
        // Unmute main menu music
        mainMenuMusic.mute = false;

        // Mute the game music
        gameMusic.mute = true;

        // Ensure main menu music is playing
        if (!mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Play();
            Debug.Log("Playing Main Menu Music");
        }
    }

    private void PlayGameMusic()
    {
        // Unmute game music
        gameMusic.mute = false;

        // Mute the main menu music
        mainMenuMusic.mute = true;

        // Ensure game music is playing
        if (!gameMusic.isPlaying)
        {
            gameMusic.Play();
            Debug.Log("Playing Game Music");
        }
    }

    private IEnumerator TransitionMusicAfterDelay(float delay)
    {
        // Wait for a specified delay to ensure the scene loads
        yield return new WaitForSeconds(delay);

        // After the delay, play the appropriate music
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            PlayMainMenuMusic();
        }
        else if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Scenes/Level 1")
        {
            PlayGameMusic();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debugging the scene name to ensure it's correct
        Debug.LogWarning("Scene loaded: " + scene.name);

        // Start music transition with a small delay after the scene is loaded
        StartCoroutine(TransitionMusicAfterDelay(0.5f)); // Adjust delay if needed
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);

    }

}