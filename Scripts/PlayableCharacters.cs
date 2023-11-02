using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayableCharacters : MonoBehaviour
{
    public GameObject[] playableChars;
    private string temp_playerClass;
    private string current_scene;

    // Start is called before the first frame update
    void Start()
    {
        //SetCharsInactive();

        current_scene = SceneManager.GetActiveScene().name;

        if (current_scene != "IslandMainMenuHub")
        {
            temp_playerClass = PlayerPrefs.GetString("playerClass");

            if (temp_playerClass == "Necromancer")
            {
                UnityEngine.Debug.Log("Spawned player necromancer");
                playableChars[0].SetActive(true);
            }
            else if (temp_playerClass == "Warrior")
            {
                UnityEngine.Debug.Log("Spawned player warrior");
                playableChars[1].SetActive(true);
            }
            else if (temp_playerClass == "Mage")
            {
                UnityEngine.Debug.Log("Spawned player Mage");
                playableChars[2].SetActive(true);
            }
            else if (temp_playerClass == "Ravager")
            {
                UnityEngine.Debug.Log("Spawned player Ravager");
                playableChars[3].SetActive(true);
            }
            else if (temp_playerClass == "Archer")
            {
                UnityEngine.Debug.Log("Spawned player Archer");
                playableChars[4].SetActive(true);
            }
            else
            {
                UnityEngine.Debug.Log("Error spawning player");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void SetCharsInactive()
    {
        for (int i = 0; i < playableChars.Length; i++)
        {
            if(playableChars[i] != null)
                playableChars[i].SetActive(false);
        }
    }
}
