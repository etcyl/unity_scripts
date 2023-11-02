using UnityEngine;

public class PlaySoundOnDisable : MonoBehaviour
{
    public AudioClip soundClip; // Assign this through the inspector

    private void OnDisable()
    {
        // Check if the AudioClip is assigned to prevent errors
        if (soundClip != null)
        {
            // Create a temporary game object to play the clip
            GameObject temporaryAudioHost = new GameObject("TempAudio");
            AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>();
            audioSource.clip = soundClip;
            audioSource.Play();

            // Destroy the temporary game object after the clip has finished playing
            Destroy(temporaryAudioHost, soundClip.length);
        }
    }
}
