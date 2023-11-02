using UnityEngine;

public class MuteOnFocusLost : MonoBehaviour
{
    private float initialVolume = 1.0f; // Store the initial volume
    private bool audioWasMuted = false; // Store whether audio was muted before losing focus

    void Start()
    {
        // Get the initial main audio volume
        initialVolume = AudioListener.volume;
    }

    // This method is called when the application loses focus
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // Application lost focus, mute the audio
            AudioListener.volume = 0f;
            audioWasMuted = AudioListener.pause; // Store whether audio was muted
            AudioListener.pause = true; // Pause audio playback
        }
        else
        {
            // Application regained focus
            if (!audioWasMuted)
            {
                // If audio wasn't muted before losing focus, restore the audio volume
                AudioListener.volume = initialVolume;
                AudioListener.pause = false; // Resume audio playback
            }
        }
    }
}
