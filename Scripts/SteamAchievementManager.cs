using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;
using System.Text;

public class SteamAchievementManager : MonoBehaviour
{
    private const int MaxRichPresenceKeyLength = 256;
    private const int MaxRichPresenceValueLength = 256;

    public bool completed = false;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Steam API
        SteamAPI.Init();

        if (!SteamManager.Initialized)
            return;
        else
            Debug.Log("steam manager is initialized");

        SetRichPresence("status", "In Main Menu");
            

    }

    // Update is called once per frame
    void Update()
    {
        //Scene the_scene = SceneManager.GetActiveScene();
        //if(the_scene.name.Contains("menu") || the_scene.name.Contains("loading") || the_scene.name.Contains("tutorial") || the_scene.name.Contains("char"))
        //{
            //return;
        //}

        if (!SteamManager.Initialized)
            return;
        else
            Debug.Log("steam manager is initialized");

        Scene scene1 = SceneManager.GetActiveScene();


        if(scene1.name == "vincent_splash")
        {
            SetRichPresence("In Main Menu", "Character Selection");
            return;
        }
        else if(scene1.name == "darkelf_scene1")
        {
            SetRichPresence("status", "In The Conduit");
        }
        else
        {
            SetRichPresence("status", "In A Dungeon");
        }

        //NEW_ACHIEVEMENT_2_1_IceDungeonBoss

        //SaveManager.instance.bothGolemBosses

        //NEW_ACHIEVEMENT_2_7_garnetStoneRing

        if (SaveManager.instance.GarnetStoneRing == 1)
        {
            Debug.Log("garnetStoneRing achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_7_garnetStoneRing");
            SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.bothGolemBosses)
        {
            Debug.Log("bothGolemBosses achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_6_bothGolemBosses");
            SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.desertDemonBoss)
        {
            Debug.Log("desertDemonBoss achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_5_desertDemonBoss");
            SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.wyrmBoss)
        {
            Debug.Log("Dungeon wyrm boss achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_4_Wrym");
            SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.qarinahAch == 1)
        {
            //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
            Debug.Log("Dungeon Qarinah boss achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_Qarinah");
            SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Level2IceDungeonBoss == 1)
        {
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Dungeon ice boss achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_2_1_IceDungeonBoss");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Level1DungeonBoss == 1)
        {
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Dungeon Level 1 boss achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_Level1DungeonBoss");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Level1DungeonBoss == 1 && SaveManager.instance.Dungeon4 == 1 && SaveManager.instance.Dungeon3 == 1 && SaveManager.instance.Dungeon2 == 1 && SaveManager.instance.Dungeon1 == 1)
        {
            //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
            Debug.Log("Cleared all Tower Dungeons achievement unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_ALL_TOWER_DUNGEONS");
            SteamUserStats.StoreStats();
        }


        if (SaveManager.instance.SerpentsTears == 1)
        {
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Serpent's Tears achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_SERPENTS_TEARS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.TalismanOfVersatility == 1)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("TalismanOfVersatility achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_TALISMAN_OF_VERSATILITY");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.TalismanOfFort == 1)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("TalismanOfFort achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_TALISMAN_OF_FORTITUDE");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.ExperienceItem == 1)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("ExperienceItem achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_SERPENTS_TEARS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.TomeofTeleportation == 1)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("TomeofTeleportation achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_TOME_OF_TELEPORTATION");
                SteamUserStats.StoreStats();
        }


        if (PlayerPrefs.GetInt("TutorialAchievement") == 1)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Tutorial complete achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_COMPLETE_TUTORIAL");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Vincent >= 1)
        {
      
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Vincent achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_VINCENT");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Skeletons >= 10)
        {

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Skeleton achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_10_SKEL");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.playerDeaths >= 50)
        {
            //Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_DIE_50_TIMES", out completed);

                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Die 50 times unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_DIE_50_TIMES");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.enemiesKilled >= 1)
        {
            //Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_KILL_1_ENEMY", out completed);
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Enemies killed greater than 1 achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_1_ENEMY");
                SteamUserStats.StoreStats();
        }
        //Level2IceDungeonBoss
        if (SaveManager.instance.IceDungeon1 == 1)
        {
            //Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_VISIT_SNOWBIOME", out completed);
                //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Visit Snow Biome unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_VISIT_SNOWBIOME");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Dungeon4 == 1 && SaveManager.instance.Dungeon3 == 1 && SaveManager.instance.Dungeon2 == 1 && SaveManager.instance.Dungeon1 == 1 && SaveManager.instance.Level1DungeonBoss == 1)
        {
                Debug.Log("Clear 5 dungeons unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_CLEAR_5_DUNGEONS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.Level2IceDungeonBoss == 1 && SaveManager.instance.IceDungeon3 == 1 && SaveManager.instance.IceDungeon2 == 1 && SaveManager.instance.IceDungeon1 == 1 && SaveManager.instance.IceDungeon0 == 1)
        {
            //Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_CLEAR_SNOWBIOME_DUNGEONS", out completed);
            //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
            Debug.Log("Clear all Snow Biome dungeons unlocked");
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_CLEAR_SNOWBIOME_DUNGEONS");
            SteamUserStats.StoreStats();

        }



        if (SaveManager.instance.finalDung == 1)
        {
            //Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_KILL_DENWEN", out completed);
               //m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Defeat Denwen unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_DENWEN");
                SteamUserStats.StoreStats();

               // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_CLEAR_ALL_DUNGEONS", out completed);
               // m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Clear all dungeons unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_CLEAR_ALL_DUNGEONS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.trollsKilled >= 50)
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_KILL_50_GOBLINS", out completed);
             //   m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Trolls killed greater than 50 achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_50_GOBLINS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.knightsKilled >= 10)
        {
            // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_KILL_50_GOBLINS", out completed);

                //   m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Knights killed greater than 10 achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_10_KNIGHTS");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.cliffsDived >= 10)
        {
          //  Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_CLIFF_DIVER", out completed);
              //  m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Cliffs dived greater than 10 achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_CLIFF_DIVER");
                SteamUserStats.StoreStats();
        }


        if (SaveManager.instance.enemiesKilled >= 1000)
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_KILL_1000_ENEMIES", out completed);
               // m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Enemies killed greater than 1000 achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_KILL_1000_ENEMIES");
                SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
          //  Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_TOGGLE_NAVHUD", out completed);
               // m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Toggle Nav HUD achievement unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_TOGGLE_NAVHUD");
                SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_OPEN_INVENTORY", out completed);

              //  m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Toggle Inventory Hud unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_OPEN_INVENTORY");
                SteamUserStats.StoreStats();
        }


        if (SaveManager.instance.coin >= 500)
        {
          //  Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_500_COIN", out completed);
             //   m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Pickup 500 coins unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_500_COIN");
                SteamUserStats.StoreStats();
        }

        if (SaveManager.instance.DungeonsCleared >= 10)
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_CLEAR_10_DUNGEONS", out completed);
              //  m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Clear 10 dungeons unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_CLEAR_10_DUNGEONS");
                SteamUserStats.StoreStats();
        }

        if (PlayerPrefs.GetInt("selectedCharacter") == 1)
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_PLAY_KNIGHT", out completed);
              //  m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Play as a knight unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_PLAY_KNIGHT");
                SteamUserStats.StoreStats();
        }
        //	NEW_ACHIEVEMENT_1_PLAY_KNIGHT

        if (PlayerPrefs.GetInt("Myrddin_Unlocked") == 3)
        {
          //  Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_MAGE_UNLOCK", out completed);
             //   m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Unlock Myrddin unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_MAGE_UNLOCK");
                SteamUserStats.StoreStats();
        }

        if (PlayerPrefs.GetInt("selectedCharacter") == 2)
        {

               // m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Play as Fallen Knight unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_FALLENKNIGHT_UNLOCK");
                SteamUserStats.StoreStats();
        }

        //NEW_ACHIEVEMENT_1_VISIT_DESERTCITY
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "desert_town_1")
        {
           // Steamworks.SteamUserStats.GetAchievement("NEW_ACHIEVEMENT_1_VISIT_DESERTCITY", out completed);
               // m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
                Debug.Log("Visit the desert city unlocked");
                SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_VISIT_DESERTCITY");
                SteamUserStats.StoreStats();
        }

    }

    void OnAchievementStored(UserAchievementStored_t pCallback)
    {

    }

    private void SetRichPresence(string key, string value)
    {
        // Check if the key or value is empty or exceeds the maximum length
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value) ||
            key.Length > MaxRichPresenceKeyLength || value.Length > MaxRichPresenceValueLength)
        {
            Debug.LogWarning("Invalid Rich Presence data.");
            return;
        }

        // Set the Rich Presence key-value pair using Steamworks.NET
        if(SteamFriends.SetRichPresence(key, value))
            Debug.Log("SetRichPresence successful");
        else
            Debug.Log("SetRichPresence not successful");

        // Update the Steam callbacks
        SteamAPI.RunCallbacks();
    }
}
