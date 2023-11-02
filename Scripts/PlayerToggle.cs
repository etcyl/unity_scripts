using UnityEngine;

public class PlayerToggle : MonoBehaviour
{
    public Camera fpsCamera;              // Assign the FPS Camera in the Inspector
    public Camera actionCamera;           // Assign the Action (Third Person) Camera in the Inspector

    private bool isActionMode = true;     // Let's assume we start in Action (Third Person) mode

    void Update()
    {
        // Listen for the "P" key press
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleMode();
        }
        // Set the player's forward direction to match the FPS camera's forward direction
        if(isActionMode)
            fpsCamera.transform.position = actionCamera.transform.position;
    }

    void ToggleMode()
    {
        isActionMode = !isActionMode;

        //if (!isActionMode)
        //{
        //    // Set the player's forward direction to match the FPS camera's forward direction
         //   transform.forward = fpsCamera.transform.forward;

        //}
       // else
       // {
            // Optionally, you can adjust orientation when switching back to third person here, if needed
       // }

        // Toggle the cameras based on the mode
        fpsCamera.gameObject.SetActive(!isActionMode);
        actionCamera.gameObject.SetActive(isActionMode);
    }
}
