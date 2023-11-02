using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABCToolkit
{

    public class LevelUpMgr : MonoBehaviour
    {
        public AudioClip audioClip;  // Drag your audio clip here in the Inspector

        private AudioSource audioSource;

        private float delayDuration = 3.0f;

        // Start is called before the first frame update
        float currentLevel;
        float currentExp;

        public GameObject levelUpFX;
        public ABC_StateManager statemgr;
        public ABC_Controller controllermgr;


        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;

            levelUpFX.SetActive(false);
            currentLevel = PlayerPrefs.GetFloat("Level");//ABCStatemanager.GetLevel(); // Replace with your actual method.
            currentExp = PlayerPrefs.GetFloat("Experience");//ABCStatemanager.GetExperience(); // Replace with your actual method.
        }

        // Update is called once per frame
        void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                levelUpFX.transform.position = playerObject.transform.position;
                controllermgr = playerObject.GetComponent<ABC_Controller>();
                statemgr = playerObject.GetComponent<ABC_StateManager>();
            }
            //SetStatValue
            currentExp = PlayerPrefs.GetFloat("Experience");
            currentLevel = PlayerPrefs.GetFloat("Level");
            if (currentExp >= CalculateExpRequiredForNextLevel(currentLevel))
            {
                PlayerPrefs.SetFloat("Level", currentLevel + 1);
                PlayAudio();
                levelUpFX.SetActive(true);
                StartCoroutine(DisableAfterDelayCoroutine());


                if (currentExp > CalculateExpRequiredForNextLevel(currentLevel))
                {
                    float tempdiff = currentExp - CalculateExpRequiredForNextLevel(currentLevel);
                    PlayerPrefs.SetFloat("Experience", tempdiff);
                }
                else
                    PlayerPrefs.SetFloat("Experience", 0);

                string temp_playerClass = PlayerPrefs.GetString("playerClass");

                // Set player preferences based on the loaded profile.

                PlayerPrefs.SetFloat("MaxHealth", PlayerPrefs.GetFloat("MaxHealth") + 2.5f);
                PlayerPrefs.SetFloat("MaxMana", PlayerPrefs.GetFloat("MaxMana") + 2.5f);

                PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("MaxHealth"));
                PlayerPrefs.SetFloat("Mana", PlayerPrefs.GetFloat("MaxMana"));

                PlayerPrefs.SetFloat("ManaRegen", PlayerPrefs.GetFloat("ManaRegen") + 0.5f);
                PlayerPrefs.SetFloat("HealthRegen", PlayerPrefs.GetFloat("HealthRegen") + 0.5f);
                PlayerPrefs.SetFloat("Agility", PlayerPrefs.GetFloat("Agility") + 0.25f);
                PlayerPrefs.SetFloat("Strength", PlayerPrefs.GetFloat("Strength") + 0.25f);
                PlayerPrefs.SetFloat("Magic", PlayerPrefs.GetFloat("Magic") + 0.25f);
                PlayerPrefs.SetFloat("Defense", PlayerPrefs.GetFloat("Defense") + 0.01f);
                PlayerPrefs.SetFloat("DefenseMitigation", PlayerPrefs.GetFloat("DefenseMitigation") + 0.01f);

                if (temp_playerClass == "Necromancer")
                {
                    UnityEngine.Debug.Log("Spawned player necromancer");

                }
                else if (temp_playerClass == "Warrior")
                {
                    UnityEngine.Debug.Log("Spawned player warrior");

                    PlayerPrefs.SetFloat("MaxHealth", PlayerPrefs.GetFloat("MaxHealth") + 2.5f);
                    PlayerPrefs.SetFloat("Strength", PlayerPrefs.GetFloat("Strength") + 0.25f);
                    PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("MaxHealth"));


                }
                else if (temp_playerClass == "Paladin")
                {
                    UnityEngine.Debug.Log("Spawned player paladin");
                    PlayerPrefs.SetFloat("MaxHealth", PlayerPrefs.GetFloat("MaxHealth") + 1f);
                    PlayerPrefs.SetFloat("MaxMana", PlayerPrefs.GetFloat("MaxMana") + 1f);

                    PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("MaxHealth"));

                }
                else if (temp_playerClass == "Mage")
                {
                    UnityEngine.Debug.Log("Spawned player mage");
                    PlayerPrefs.SetFloat("MaxMana", PlayerPrefs.GetFloat("MaxMana") + 1f);
                    PlayerPrefs.SetFloat("Magic", PlayerPrefs.GetFloat("Magic") + 1f);



                }
                else if (temp_playerClass == "Ravager")
                {
                    PlayerPrefs.SetFloat("Strength", PlayerPrefs.GetFloat("Strength") + 0.25f);
                    PlayerPrefs.SetFloat("Agility", PlayerPrefs.GetFloat("Agility") + 1.1f);

                }
                else if (temp_playerClass == "Brawler")
                {
                    UnityEngine.Debug.Log("Spawned player brawler");

                    PlayerPrefs.SetFloat("Strength", PlayerPrefs.GetFloat("Strength") + 0.5f);
                    PlayerPrefs.SetFloat("Agility", PlayerPrefs.GetFloat("Agility") + 0.5f);

                }
                else if (temp_playerClass == "Archer")
                {
                    UnityEngine.Debug.Log("Spawned player archer");

                    PlayerPrefs.SetFloat("Strength", PlayerPrefs.GetFloat("Strength") + 0.1f);
                    PlayerPrefs.SetFloat("Agility", PlayerPrefs.GetFloat("Agility") + 1.1f);

                }
                else
                {
                    UnityEngine.Debug.Log("Error spawning player");
                }

                if(controllermgr != null)
                {
                    controllermgr.currentMaxMana += PlayerPrefs.GetFloat("MaxMana");
                    controllermgr.manaABC += PlayerPrefs.GetFloat("MaxMana");
                    controllermgr.manaRegenPerSecond += PlayerPrefs.GetFloat("ManaRegen");
                }

                if (statemgr != null)
                {
                    statemgr.SetStatValue("MaxHealth", PlayerPrefs.GetFloat("MaxHealth"));
                    statemgr.healthRegenPerSecond += PlayerPrefs.GetFloat("HealthRegen");
                    statemgr.SetStatValue("Magic", PlayerPrefs.GetFloat("Magic"));
                    statemgr.SetStatValue("Agility", PlayerPrefs.GetFloat("Agility"));
                    statemgr.SetStatValue("Strength", PlayerPrefs.GetFloat("Strength"));
                    statemgr.SetStatValue("mitigateMeleeDamagePercentage", PlayerPrefs.GetFloat("DefenseMitigation"));
                }


            }

        }

        private IEnumerator DisableAfterDelayCoroutine()
        {
            yield return new WaitForSeconds(delayDuration);
            levelUpFX.SetActive(false);
        }

        private float CalculateExpRequiredForNextLevel(float currentLevel)
        {
            // Calculate the experience points required to reach the next level.
            return (float)Mathf.Pow(9, currentLevel); // Example: 9^currentLevel
        }

        public void PlayAudio()
        {
            if (audioSource != null && audioClip != null)
            {
                audioSource.Play();
            }
        }

    }

}