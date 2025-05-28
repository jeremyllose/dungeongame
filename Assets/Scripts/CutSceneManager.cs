using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [Header("Scene Images in Order")]
    public Image[] scenes; // Assign your cutscene images in order in the Inspector

    [Header("SFX Clips in Order")]
    public AudioClip[] sfxClips; // Assign your SFX in the same order as scenes

    [Header("Audio Source")]
    public AudioSource audioSource; // Assign a dedicated AudioSource for SFX

    [Header("Cutscene Settings")]
    public float sceneDuration = 3f; // Duration for each scene

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            ShowScene(i);

            // Play SFX if assigned
            if (i < sfxClips.Length && sfxClips[i] != null)
            {
                audioSource.Stop(); // Prevent overlap
                audioSource.PlayOneShot(sfxClips[i]);
            }

            yield return new WaitForSeconds(sceneDuration);
        }

        // All scenes done – load the game
        SceneManager.LoadScene("Level 1"); // Replace with your actual gameplay scene name
    }

    private void ShowScene(int index)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].gameObject.SetActive(i == index);
        }
    }
}
