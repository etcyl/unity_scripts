using UnityEngine;

public class DistanceCheckPots : MonoBehaviour
{
    private GameObject player;
    public float DETECTION_DISTANCE = 7.0f;

    public AudioClip pickupSound; // Reference to the sound clip
    public GameObject visualEffectPrefab; // Reference to the visual effect prefab
    private AudioSource audioSource; // AudioSource component to play the sound

    private void Start()
    {
        // Find the player by its tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            UnityEngine.Debug.Log("Player object with the tag 'Player' not found in the scene.");
        }

        // Get the AudioSource component from this game object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && pickupSound != null) // If AudioSource is missing but a pickupSound is assigned
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        // If the player exists
        if (player != null)
        {
            // Calculate the distance between the player and this game object
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // If the distance is less than or equal to the defined detection distance
            if (distance <= DETECTION_DISTANCE)
            {
                UpdatePlayerPrefs();
            }
        }
    }

    private void UpdatePlayerPrefs()
    {
        int currentHealthPots = PlayerPrefs.GetInt("numReusableHealthPots", 0);
        int currentManaPots = PlayerPrefs.GetInt("numReusableManaPots", 0);

        // If the player already has 3 or more of any of the pots, don't do anything
        if (currentHealthPots < 3 || currentManaPots < 3)
        {
            PlayerPrefs.SetInt("numReusableHealthPots", 3);
            PlayerPrefs.SetInt("numReusableManaPots", 3);

            // Play the sound
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // Instantiate the visual effect on the player
            if (visualEffectPrefab != null)
            {
                GameObject effect = Instantiate(visualEffectPrefab, player.transform.position, Quaternion.identity);
                effect.transform.parent = player.transform; // Attach the effect to the player
            }
        }
    }
}
