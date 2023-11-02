using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectController : MonoBehaviour
{
    public GameObject mainMenuCanvasUI;
    public GameObject pickClassUI;
    public Toggle controlToggle;      // Reference to the Toggle component
    public GameObject targetObject;   // Reference to the GameObject to control
    public GameObject settingsUI;
    public bool currState = false;

    private void Start()
    {
        // Set initial state based on the toggle's value
        targetObject.SetActive(controlToggle.isOn);

        // Subscribe to the toggle's onValueChanged event
        controlToggle.onValueChanged.AddListener(ToggleObjectState);
    }

    private void ToggleObjectState(bool toggleState)
    {
        currState = toggleState;
        // Set the target object's active state based on the toggle's value
        targetObject.SetActive(toggleState);
    }

    private void Update()
    {
        if (settingsUI.activeInHierarchy || mainMenuCanvasUI.activeInHierarchy || pickClassUI.activeInHierarchy)
        {
            targetObject.SetActive(false);
        }
        else
        {
            targetObject.SetActive(currState);
        }
    }
}
