using System.Configuration;
using UnityEngine;

public class TradeVendorInteraction : MonoBehaviour
{
    public GameObject Settings;

    private GameObject player;
    public GameObject tradeVendorUI; // Drag your Trade Vendor Window UI GameObject here in the inspector

    private float interactionDistance = 8.0f;
    private bool isUIOpen = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Check the distance between the NPC and the player
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= interactionDistance)
            {
                // Check if T is pressed
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ToggleTradeUI();
                }
            }
            else if (isUIOpen) // If the player is further than the interaction distance and the UI is open
            {
                CloseTradeUI();
            }

            // Check for escape key to close the UI
            if (isUIOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseTradeUI();
            }

        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

    }

    private void ToggleTradeUI()
    {
        if (isUIOpen)
        {
            CloseTradeUI();
        }
        else
        {
            OpenTradeUI();
            Settings.SetActive(false);
        }
    }

    private void OpenTradeUI()
    {
        tradeVendorUI.SetActive(true);
        isUIOpen = true;
    }

    private void CloseTradeUI()
    {
        tradeVendorUI.SetActive(false);
        isUIOpen = false;
    }
}
