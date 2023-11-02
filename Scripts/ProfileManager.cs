using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
// settings mgr finish load game 
namespace ABCToolkit
{
    public class ProfileManager : MonoBehaviour
    {
        public PlayableCharacters chars;

        public string classSelected;

        public TMP_InputField YourInputField;

        public SettingsMgr settingsMgr;

        private const int MAX_PROFILES = 10;
        private const string FILE_EXTENSION = ".json";
        private List<PlayerProfile> profiles = new List<PlayerProfile>();


        public List<PlayerProfile> profilesLUT = new List<PlayerProfile>();

        public List<PlayerProfile> CreatePlayerProfiles()
        {
            List<PlayerProfile> playerProfiles = new List<PlayerProfile>();

            for (int i = 0; i < 10; i++)
            {
                PlayerProfile profile = new PlayerProfile
                {
                    playerName = null,
                    health = 0.0f,
                    mana = 0.0f,
                    manaRegen = 0.0f,
                    healthRegen = 0.0f,
                    agility = 0.0f,
                    strength = 0.0f,
                    magic = 0.0f,
                    defense = 0.0f,
                    defenseMitigation = 0.0f,
                    level = 0.0f,
                    experience = 0.0f,
                    playerClass = null,
                    profilePath = null,
                    numReusableHealthPots = 0,
                    numReusableManaPots = 0,
                    profileIndex = i,
                    coins = 0
                };

                playerProfiles.Add(profile);
            }

            return playerProfiles;
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, fileName + FILE_EXTENSION);
        }


        public bool SaveProfile(PlayerProfile profile)
        {

            string path = GetFilePath("Name");
            if (File.Exists(path))
            {
                UnityEngine.Debug.Log("A profile with this name already exists.");
                return false;
            }

            string json = JsonUtility.ToJson(profile);
            File.WriteAllText(path, json);
            profiles.Add(profile);

            UnityEngine.Debug.Log($"Profile '{profile.playerName}' successfully saved!");
            return true;
        }

        // Function for saving the current profile
        public void SaveCurrentProfile()
        {
            // Check if we have a profile to save
            if (PlayerPrefs.GetString("profilePath") == null)
            {
                UnityEngine.Debug.LogWarning("No profile data to save.");
                return;
            }

            // Create a new PlayerProfile instance with the current PlayerPrefs data
            PlayerProfile profileToSave = new PlayerProfile
            {
                playerName = PlayerPrefs.GetString("PlayerName"),
                numReusableHealthPots = PlayerPrefs.GetInt("numReusableHealthPots", 0),
                numReusableManaPots = PlayerPrefs.GetInt("numReusableManaPots", 0),
                health = PlayerPrefs.GetFloat("Health", 25f),
                maxhealth = PlayerPrefs.GetFloat("MaxHealth", 25f),
                maxmana = PlayerPrefs.GetFloat("MaxMana"),
                mana = PlayerPrefs.GetFloat("Mana"),
                manaRegen = PlayerPrefs.GetFloat("ManaRegen"),
                healthRegen = PlayerPrefs.GetFloat("HealthRegen"),
                agility = PlayerPrefs.GetFloat("Agility"),
                strength = PlayerPrefs.GetFloat("Strength"),
                magic = PlayerPrefs.GetFloat("Magic"),
                defense = PlayerPrefs.GetFloat("Defense"),
                defenseMitigation = PlayerPrefs.GetFloat("DefenseMitigation"),
                level = PlayerPrefs.GetFloat("Level"),
                experience = PlayerPrefs.GetFloat("Experience"),
                playerClass = PlayerPrefs.GetString("playerClass"),
                profileIndex = PlayerPrefs.GetInt("currentIndex"),
                profilePath = PlayerPrefs.GetString("profilePath"),
                coins = PlayerPrefs.GetInt("coins")
            };

            // Convert the PlayerProfile to JSON and save it to the file
            string jsonProfile = JsonUtility.ToJson(profileToSave);
            File.WriteAllText(profileToSave.profilePath, jsonProfile);

            UnityEngine.Debug.Log("Profile saved successfully.");

            profileToSave = null;
        }

        public PlayerProfile LoadProfile(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                UnityEngine.Debug.Log("Invalid player name provided for loading.");
                return null;
            }

            string path = GetFilePath("Name");
            if (!File.Exists(path))
            {
                UnityEngine.Debug.Log($"Profile '{playerName}' not found.");
                return null;
            }

            string json = File.ReadAllText(path);
            PlayerProfile profile = JsonUtility.FromJson<PlayerProfile>(json);
            UnityEngine.Debug.Log($"Profile '{playerName}' successfully loaded!");
            return profile;
        }


        public void CreateContinue()
        {
            PlayerPrefs.SetString("playerClass", classSelected);
            string temp_playerClass = classSelected;

            if (temp_playerClass == "Necromancer")
            {
                UnityEngine.Debug.Log("Spawned player necromancer");
                chars.playableChars[0].SetActive(true);
            }
            else if (temp_playerClass == "Warrior")
            {
                UnityEngine.Debug.Log("Spawned player warrior");
                chars.playableChars[1].SetActive(true);
            }
            else if (temp_playerClass == "Mage")
            {
                UnityEngine.Debug.Log("Spawned player Mage");
                chars.playableChars[2].SetActive(true);
            }
            else if (temp_playerClass == "Ravager")
            {
                UnityEngine.Debug.Log("Spawned player Ravager");
                chars.playableChars[3].SetActive(true);
            }
            else if (temp_playerClass == "Archer")
            {
                UnityEngine.Debug.Log("Spawned player Archer");
                chars.playableChars[4].SetActive(true);
            }
            else
            {
                UnityEngine.Debug.Log("Error spawning player");
            }

            string file_path = GetFilePath(temp_playerClass);
            // Use file_path to check if file exists
            // if true, then check for data to set player prefs
            // if not, create a profile with stats and write to the file
            if (!File.Exists(file_path))
            {
                UnityEngine.Debug.Log($"Profile '{temp_playerClass}' not found. Creating new profile.");
                //return null;
                PlayerProfile profile = new PlayerProfile
                {
                    playerName = null,
                    health = 20.0f,
                    mana = 20.0f,
                    maxhealth = 20.0f,
                    maxmana = 50.0f,
                    manaRegen = 2.5f,
                    healthRegen = 2.5f,
                    agility = 2.0f,
                    strength = 2.0f,
                    magic = 2.0f,
                    defense = 1.0f,
                    defenseMitigation = 0.2f,
                    level = 1.0f,
                    experience = 0.0f,
                    playerClass = temp_playerClass,
                    profilePath = file_path,
                    numReusableHealthPots = 0,
                    numReusableManaPots = 0,
                    profileIndex = 0,
                    coins = 0
                };

                string json = JsonUtility.ToJson(profile);
                File.WriteAllText(file_path, json);
                profiles.Add(profile);

                UnityEngine.Debug.Log($"Profile '{temp_playerClass}' successfully saved!");
                PlayerPrefs.SetString("PlayerName", profile.playerName);
                PlayerPrefs.SetString("playerClass", profile.playerClass);
                PlayerPrefs.GetInt("numReusableHealthPots", profile.numReusableHealthPots);
                PlayerPrefs.GetInt("numReusableManaPots", profile.numReusableManaPots);
                PlayerPrefs.SetFloat("Health", profile.health);
                PlayerPrefs.SetFloat("Mana", profile.mana);
                PlayerPrefs.SetFloat("MaxHealth", profile.maxhealth);
                PlayerPrefs.SetFloat("MaxMana", profile.maxmana);
                PlayerPrefs.SetFloat("ManaRegen", profile.manaRegen);
                PlayerPrefs.SetFloat("HealthRegen", profile.healthRegen);
                PlayerPrefs.SetFloat("Agility", profile.agility);
                PlayerPrefs.SetFloat("Strength", profile.strength);
                PlayerPrefs.SetFloat("Magic", profile.magic);
                PlayerPrefs.SetFloat("Defense", profile.defense);
                PlayerPrefs.SetFloat("DefenseMitigation", profile.defenseMitigation);
                PlayerPrefs.SetFloat("Level", profile.level);
                PlayerPrefs.SetFloat("Experience", profile.experience);
                PlayerPrefs.SetInt("currentIndex", profile.profileIndex);
                PlayerPrefs.SetString("profilePath", file_path);
                PlayerPrefs.SetInt("coins", profile.coins);


            }
            else
            {

                string json = File.ReadAllText(file_path);
                PlayerProfile profile = JsonUtility.FromJson<PlayerProfile>(json);

                UnityEngine.Debug.Log($"Profile '{temp_playerClass}' successfully saved!");
                PlayerPrefs.SetString("PlayerName", profile.playerName);
                PlayerPrefs.SetString("playerClass", profile.playerClass);
                PlayerPrefs.GetInt("numReusableHealthPots", profile.numReusableHealthPots);
                PlayerPrefs.GetInt("numReusableManaPots", profile.numReusableManaPots);
                PlayerPrefs.SetFloat("Health", profile.health);
                PlayerPrefs.SetFloat("Mana", profile.mana);
                PlayerPrefs.SetFloat("MaxHealth", profile.maxhealth);
                PlayerPrefs.SetFloat("MaxMana", profile.maxmana);
                PlayerPrefs.SetFloat("ManaRegen", profile.manaRegen);
                PlayerPrefs.SetFloat("HealthRegen", profile.healthRegen);
                PlayerPrefs.SetFloat("Agility", profile.agility);
                PlayerPrefs.SetFloat("Strength", profile.strength);
                PlayerPrefs.SetFloat("Magic", profile.magic);
                PlayerPrefs.SetFloat("Defense", profile.defense);
                PlayerPrefs.SetFloat("DefenseMitigation", profile.defenseMitigation);
                PlayerPrefs.SetFloat("Level", profile.level);
                PlayerPrefs.SetFloat("Experience", profile.experience);
                PlayerPrefs.SetInt("currentIndex", profile.profileIndex);
                PlayerPrefs.SetString("profilePath", file_path);
                PlayerPrefs.SetInt("coins", profile.coins);
            }
            // Set ABC stats
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                ABC_Controller controllermgr = playerObject.GetComponent<ABC_Controller>();
                ABC_StateManager statemgr = playerObject.GetComponent<ABC_StateManager>();

                if (controllermgr != null)
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


            finishCreate();

        }

        public void PressedContinue()
        {
            if (profiles.Count == 0)
                return;
            else if (PlayerPrefs.GetString("PlayerName", "Warrior") == null)
            {
                return;
            }
            else
            {
                string temp_playerClass = PlayerPrefs.GetString("playerClass");

                if (temp_playerClass == "Necromancer")
                {
                    UnityEngine.Debug.Log("Spawned player necromancer");
                    chars.playableChars[0].SetActive(true);
                }
                else if (temp_playerClass == "Warrior")
                {
                    UnityEngine.Debug.Log("Spawned player warrior");
                    chars.playableChars[1].SetActive(true);
                }
                else if (temp_playerClass == "Mage")
                {
                    UnityEngine.Debug.Log("Spawned player Mage");
                    chars.playableChars[2].SetActive(true);
                }
                else if (temp_playerClass == "Ravager")
                {
                    UnityEngine.Debug.Log("Spawned player Ravager");
                    chars.playableChars[3].SetActive(true);
                }
                else if (temp_playerClass == "Archer")
                {
                    UnityEngine.Debug.Log("Spawned player Archer");
                    chars.playableChars[4].SetActive(true);
                }
                else
                {
                    UnityEngine.Debug.Log("Error spawning player");
                }

                finishCreate();
            }
        }

        public void OnProfileButtonClick(int index)
        {
            if (index >= profiles.Count)
            {
                UnityEngine.Debug.Log("Empty slot clicked.");
                return;
            }

            PlayerProfile clickedProfile = profiles[index];

            // Here, instantiate the player prefab based on the profile class/type.
            // Update the instantiated prefab with the stats from clickedProfile.
            // Disable the profile UI and enable the player manager.

            UnityEngine.Debug.Log($"Loaded profile for {clickedProfile.playerName}.");
        }

        private void Awake()
        {
            classSelected = "Warrior";

            profilesLUT = CreatePlayerProfiles();

            // Load all profiles at the start
            for (int i = 0; i < MAX_PROFILES; i++)
            {
                string path = GetFilePath("Name");
                UnityEngine.Debug.Log("Save profile path is: " + path);
                if (File.Exists(path))
                {
                    UnityEngine.Debug.Log("profile path exists" + i);

                    string json = File.ReadAllText(path);
                    PlayerProfile profile = JsonUtility.FromJson<PlayerProfile>(json);
                    profiles.Add(profile);
                    profile.profilePath = path;

                    profilesLUT[profile.profileIndex] = profile;
                }
            }

        }

        public void OpenProfileFolder()
        {
            // Replace "Application.dataPath" with the path to your C# files folder
            string folderPath = Path.Combine(UnityEngine.Application.persistentDataPath);

            // Make sure you have a valid folder path before opening it
            if (Directory.Exists(folderPath))
            {
                Process.Start(folderPath);
            }
            else
            {
                UnityEngine.Debug.Log("Folder not found: " + folderPath);
            }
        }

        public void ClickedCreateProfile()
        {
            // Get the player name from the input field (assuming you have a TMP input field).
            string playerName = GetPlayerNameFromInputField();

            // Create a new player profile.
            PlayerProfile newProfile = new PlayerProfile();
            newProfile.playerName = playerName;
            newProfile.level = 1;
            newProfile.experience = 0;
            newProfile.playerClass = classSelected;

            // You can set initial values for other profile properties here if needed.

            // Set player preferences based on the loaded profile.
            PlayerPrefs.SetString("PlayerName", newProfile.playerName);
            PlayerPrefs.SetString("playerClass", newProfile.playerClass);
            PlayerPrefs.GetInt("numReusableHealthPots", newProfile.numReusableHealthPots);
            PlayerPrefs.GetInt("numReusableManaPots", newProfile.numReusableManaPots);
            PlayerPrefs.SetFloat("Health", newProfile.health);
            PlayerPrefs.SetFloat("Mana", newProfile.mana);
            PlayerPrefs.SetFloat("MaxHealth", newProfile.maxhealth);
            PlayerPrefs.SetFloat("MaxMana", newProfile.maxmana);
            PlayerPrefs.SetFloat("ManaRegen", newProfile.manaRegen);
            PlayerPrefs.SetFloat("HealthRegen", newProfile.healthRegen);
            PlayerPrefs.SetFloat("Agility", newProfile.agility);
            PlayerPrefs.SetFloat("Strength", newProfile.strength);
            PlayerPrefs.SetFloat("Magic", newProfile.magic);
            PlayerPrefs.SetFloat("Defense", newProfile.defense);
            PlayerPrefs.SetFloat("DefenseMitigation", newProfile.defenseMitigation);
            PlayerPrefs.SetFloat("Level", newProfile.level);
            PlayerPrefs.SetFloat("Experience", newProfile.experience);
            PlayerPrefs.SetInt("currentIndex", newProfile.profileIndex);
            PlayerPrefs.SetString("profilePath", GetFilePath(classSelected));
            PlayerPrefs.SetInt("coins", newProfile.coins);

            // Save the new profile.
            bool saveSuccessful = SaveProfile(newProfile);

            if (saveSuccessful)
            {
                UnityEngine.Debug.Log($"Profile '{playerName}' successfully created!");

                if (newProfile.playerClass == "Necromancer")
                {
                    UnityEngine.Debug.Log("Spawned player necromancer");
                    chars.playableChars[0].SetActive(true);
                }
                else if (newProfile.playerClass == "Warrior")
                {
                    UnityEngine.Debug.Log("Spawned player warrior");
                    chars.playableChars[1].SetActive(true);
                }
                else if (newProfile.playerClass == "Paladin")
                {
                    UnityEngine.Debug.Log("Spawned player paladin");
                    chars.playableChars[2].SetActive(true);
                }
                else if (newProfile.playerClass == "Mage")
                {
                    UnityEngine.Debug.Log("Spawned player mage");
                    chars.playableChars[3].SetActive(true);
                }
                else if (newProfile.playerClass == "Ravager")
                {
                    UnityEngine.Debug.Log("Spawned player ravager");
                    chars.playableChars[4].SetActive(true);
                }
                else if (newProfile.playerClass == "Brawler")
                {
                    UnityEngine.Debug.Log("Spawned player brawler");
                    chars.playableChars[5].SetActive(true);
                }
                else if (newProfile.playerClass == "Archer")
                {
                    UnityEngine.Debug.Log("Spawned player archer");
                    chars.playableChars[6].SetActive(true);
                }
                else
                {
                    UnityEngine.Debug.Log("Error spawning player");
                }

                finishCreate();
                // Optionally, you can load this newly created profile or perform any other actions.
            }
            else
            {
                UnityEngine.Debug.Log("Failed to create the profile.");
            }
        }

        private string GetPlayerNameFromInputField()
        {
            // Replace "YourInputField" with the actual name of your TMP input field.
            // This function should return the text entered by the player in the input field.
            return YourInputField.text;
        }

        private bool ProfileExists(string playerName)
        {
            // Check if a profile with the given playerName already exists.
            return profiles.Exists(profile => profile.playerName == playerName);
        }

        public void SelectWarrior()
        {
            classSelected = "Warrior";
        }

        public void SelectMage()
        {
            classSelected = "Mage";
        }

        public void SelectRavager()
        {
            classSelected = "Ravager";
        }

        public void SelectArcher()
        {
            classSelected = "Archer";
        }

        public void SelectBrawler()
        {
            classSelected = "Brawler";
        }

        public void SelectPaladin()
        {
            classSelected = "Paladin";
        }

        public void SelectNecromancer()
        {
            classSelected = "Necromancer";
        }


        private void LoadAndSetSettings(int index)
        {
            if (index >= 0 && index < profilesLUT.Count)
            {

                PlayerProfile clickedProfile = profilesLUT[index];

                if (clickedProfile.playerName == null)
                {
                    UnityEngine.Debug.Log("Clicked profile was null: " + index);
                    return;
                }
                else if (clickedProfile.profileIndex != index)
                {
                    UnityEngine.Debug.Log("Clicked profile does not match index: " + index);
                    return;
                }
                else
                {

                    // Set player preferences based on the loaded profile.
                    PlayerPrefs.SetString("PlayerName", clickedProfile.playerName);
                    PlayerPrefs.SetString("playerClass", clickedProfile.playerClass);
                    PlayerPrefs.SetFloat("Health", clickedProfile.health);
                    PlayerPrefs.SetFloat("Mana", clickedProfile.mana);
                    PlayerPrefs.SetFloat("MaxHealth", clickedProfile.maxhealth);
                    PlayerPrefs.SetFloat("MaxMana", clickedProfile.maxmana);
                    PlayerPrefs.SetFloat("ManaRegen", clickedProfile.manaRegen);
                    PlayerPrefs.SetFloat("HealthRegen", clickedProfile.healthRegen);
                    PlayerPrefs.SetFloat("Agility", clickedProfile.agility);
                    PlayerPrefs.SetFloat("Strength", clickedProfile.strength);
                    PlayerPrefs.SetFloat("Magic", clickedProfile.magic);
                    PlayerPrefs.SetFloat("Defense", clickedProfile.defense);
                    PlayerPrefs.SetFloat("DefenseMitigation", clickedProfile.defenseMitigation);
                    PlayerPrefs.SetFloat("Level", clickedProfile.level);
                    PlayerPrefs.SetFloat("Experience", clickedProfile.experience);
                    PlayerPrefs.SetInt("currentIndex", clickedProfile.profileIndex);
                    PlayerPrefs.SetInt("coins", clickedProfile.coins);

                    // Here, instantiate the player prefab based on the profile class/type.
                    // Update the instantiated prefab with the stats from clickedProfile.
                    // Disable the profile UI and enable the player manager.

                    UnityEngine.Debug.Log($"Loaded profile for {clickedProfile.playerName}.");

                    if (clickedProfile.playerClass == "Necromancer")
                    {
                        UnityEngine.Debug.Log("Spawned player necromancer");
                        chars.playableChars[0].SetActive(true);
                    }
                    else if (clickedProfile.playerClass == "Warrior")
                    {
                        UnityEngine.Debug.Log("Spawned player warrior");
                        chars.playableChars[1].SetActive(true);
                    }
                    else if (clickedProfile.playerClass == "Mage")
                    {
                        UnityEngine.Debug.Log("Spawned player Mage");
                        chars.playableChars[2].SetActive(true);
                    }
                    else if (clickedProfile.playerClass == "Ravager")
                    {
                        UnityEngine.Debug.Log("Spawned player Ravager");
                        chars.playableChars[3].SetActive(true);
                    }
                    else if (clickedProfile.playerClass == "Archer")
                    {
                        UnityEngine.Debug.Log("Spawned player Archer");
                        chars.playableChars[4].SetActive(true);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Error spawning player");
                    }

                    setSettingsScript();
                }
            }
            else
            {
                UnityEngine.Debug.Log("No profile found for this button.");
            }
        }

        // ... (remaining code)

        public void finishCreate()
        {
            if (settingsMgr != null)
            {
                settingsMgr.FinishCreateGame();
                UnityEngine.Debug.Log("finished loading finish load game");
            }
            else
            {
                UnityEngine.Debug.Log("Error, could not finish loading finish load game");
            }
        }


        public void setSettingsScript()
        {
            if (settingsMgr != null)
            {
                settingsMgr.FinishLoadGame();
                UnityEngine.Debug.Log("finished loading finish load game");
            }
            else
            {
                UnityEngine.Debug.Log("Error, could not finish loading finish load game");
            }
        }
    }
}