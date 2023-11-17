using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour {
    void Start() {
        if(SteamManager.Initialized) {
            string name = SteamFriends.GetPersonaName();
            Debug.Log("Your Steam name is: " + name);
        }
        else{
            Debug.Log("Error with Steam not initialized");
        }
    }
}
