using System.Collections;
using UnityEngine;
using ABCToolkit;

public class Runestone_Controller : MonoBehaviour
{
    //assigned in Inspector
    [Header("Applied to the effects at start")]
    [SerializeField] private Color effectsColor;

    [Header("Changing these might `break` the effects")]
    [Space(20)]
    [SerializeField] private Renderer runeStoneRenderer;
    [SerializeField] private Transform rocksBaseTF;
    [SerializeField] private ParticleSystem[] effectsParticles;
    [SerializeField] private Light portalLight;
    [SerializeField] private AudioSource implodeAudio, forceFieldAudio, cracklingAudio;
    [SerializeField] private Transform[] runes = new Transform[2];
    [SerializeField] private AnimationCurve rockAnimCurve;

    private float maxVolForcefield = 1, maxVolCrackling = 1, maxIntPortalLight = 2;
    private float transitionSpeed = 0.5f;

    //assigned when Awake
    private Transform myTF, coreParticlesTF;
    private Transform[] rocks = new Transform[4];
    private SpriteRenderer[] runeRenderers = new SpriteRenderer[2];
    private bool inTransition, activated, animating;
    private Material matInstance;
    private Color runesWantedeColor;
    private float fadeFloat;
    public bool done = false;
    private Coroutine transitionCor, animateCor;

    public float rune_distance = 5f;

    private void Awake()
    {
        Setup();      
    }

    private void Update()
    {
        CheckPlayerDistanceAndToggleRunestone();
    }

    // New method to check the distance to the player and toggle the Runestone effects
    private void CheckPlayerDistanceAndToggleRunestone()
    {
        // Ensure the player game object is found using find with tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If the game object isn't found for the player, try to find them again and return
        if (player == null)
        {
            return;
        }

        // Check if the player game object is within 5 units of the game object this script is on
        if (Vector3.Distance(player.transform.position, transform.position) <= rune_distance && done == false)
        {
            done = true;
            // Toggle on the runestone
            ToggleRuneStone(true);

            ABC_Controller controllermgr = player.GetComponent<ABC_Controller>();
            ABC_StateManager statemgr = player.GetComponent<ABC_StateManager>();

            int reward_type = UnityEngine.Random.Range(0, 3);

            switch (reward_type)
            {
                case 0:
                    effectsColor = Color.blue;
                    break;
                case 1:
                    effectsColor = Color.red;
                    break;
                case 2:
                    effectsColor = Color.green;
                    break;
                case 3:
                    effectsColor = Color.yellow;
                    break;
                default:
                    effectsColor = Color.white; // Default color if something goes wrong
                    break;
            }


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
            string temp_playerClass = PlayerPrefs.GetString("playerClass");

            if (temp_playerClass == "Necromancer")
            {
                UnityEngine.Debug.Log("Spawned player necromancer");
                if(reward_type == 1)
                {
                    statemgr.SetStatValue("Magic", statemgr.GetStatValue("Magic") + 1 + PlayerPrefs.GetFloat("Level"));
                }
                else if(reward_type == 2)
                {
                    controllermgr.manaRegenPerSecond += 2f + PlayerPrefs.GetFloat("Level") * 0.5f;
                }
                else
                {
                    controllermgr.currentMaxMana += 50f + 10 * PlayerPrefs.GetFloat("Level");
                }

            }
            else if (temp_playerClass == "Warrior")
            {
                UnityEngine.Debug.Log("Spawned player warrior");

                if (reward_type == 1)
                {
                    statemgr.SetStatValue("Strength", statemgr.GetStatValue("Strength") + 1 + PlayerPrefs.GetFloat("Level"));
                }
                else if (reward_type == 2)
                {
                    statemgr.healthRegenPerSecond += 2f + PlayerPrefs.GetFloat("Level") * 0.5f;
                }
                else
                {
                    statemgr.SetStatValue("MaxHealth", statemgr.GetStatValue("MaxHealth") + 50f + 10 * PlayerPrefs.GetFloat("Level"));
                }


            }
            else if (temp_playerClass == "Mage")
            {
                UnityEngine.Debug.Log("Spawned player mage");
                if (reward_type == 1)
                {
                    statemgr.SetStatValue("Magic", statemgr.GetStatValue("Magic") + 1 + PlayerPrefs.GetFloat("Level"));
                }
                else if (reward_type == 2)
                {
                    controllermgr.manaRegenPerSecond += 2f + PlayerPrefs.GetFloat("Level") * 0.5f;
                }
                else
                {
                    controllermgr.currentMaxMana += 50f + 10 * PlayerPrefs.GetFloat("Level");
                }

            }
            else if (temp_playerClass == "Ravager")
            {
                if (reward_type == 1)
                {
                    statemgr.SetStatValue("Strength", statemgr.GetStatValue("Strength") + 0.5f + PlayerPrefs.GetFloat("Level"));
                }
                else if (reward_type == 2)
                {
                    statemgr.SetStatValue("Agility", statemgr.GetStatValue("Agility") + 0.5f + PlayerPrefs.GetFloat("Level"));
                }
                else
                {
                    statemgr.SetStatValue("MaxHealth", statemgr.GetStatValue("MaxHealth") + 50f + 10 * PlayerPrefs.GetFloat("Level"));
                }
            }
            else if (temp_playerClass == "Archer")
            {
                UnityEngine.Debug.Log("Spawned player archer");

                if (reward_type == 1)
                {
                    statemgr.SetStatValue("Strength", statemgr.GetStatValue("Strength") + 0.5f + PlayerPrefs.GetFloat("Level"));
                }
                else if (reward_type == 2)
                {
                    statemgr.SetStatValue("Agility", statemgr.GetStatValue("Agility") + 0.5f + PlayerPrefs.GetFloat("Level"));
                }
                else
                {
                    statemgr.SetStatValue("MaxHealth", statemgr.GetStatValue("MaxHealth") + 50f + 10 * PlayerPrefs.GetFloat("Level"));
                }
            }
            else
            {
                UnityEngine.Debug.Log("Error spawning player");


                if (reward_type == 1)
                {
                    statemgr.SetStatValue("Strength", statemgr.GetStatValue("Strength") + 1 + PlayerPrefs.GetFloat("Level"));
                }
                else if (reward_type == 2)
                {
                    statemgr.healthRegenPerSecond += 2f + PlayerPrefs.GetFloat("Level") * 0.5f;
                }
                else
                {
                    statemgr.SetStatValue("MaxHealth", statemgr.GetStatValue("MaxHealth") + 50f + 10 * PlayerPrefs.GetFloat("Level"));
                }
            }


        }
        else
        {
            // Toggle off the runestone (if desired behavior is to toggle off when player is beyond 5 units)
            // ToggleRuneStone(false);
        }
    }

    //Call this function to activate or deactivate the effects
    public void ToggleRuneStone(bool _activate)
    {
        if (inTransition || activated == _activate)
            return;

        if (_activate)//toggle on
        {
            implodeAudio.Play();

            activated = true;

            transitionCor = StartCoroutine(TransitionSequence());

            for (int i = 0; i < 2; i++)
            {
                effectsParticles[i].Play();
            }

            forceFieldAudio.Play();
            cracklingAudio.Play();
        }
        else if (!_activate)//toggle off
        {
            implodeAudio.Play();

            activated = false;            

            if (animating)
            {
                StopCoroutine(animateCor);
                animating = false;
            }

            transitionCor = StartCoroutine(TransitionSequence());
        }
    }

    private IEnumerator TransitionSequence()
    {
        inTransition = true;

        float rocksCurrentHeight = rocksBaseTF.localPosition.y;
        Vector3 rocksWantedPosition = rocksBaseTF.localPosition;

        while (inTransition)
        {
            if (activated)//transition to on
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 1f, Time.deltaTime * transitionSpeed);

                rocksWantedPosition.y = fadeFloat * 1.5f;

                if (fadeFloat >= 1f)//transition finished
                {
                    inTransition = false;
                    animateCor = StartCoroutine(AnimateActiveEffects());//start active "animation"
                }
            }
            else //transition to off
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 0f, Time.deltaTime * transitionSpeed);

                rocksWantedPosition.y = fadeFloat * rocksCurrentHeight;

                if (fadeFloat <= 0f)//transition finished
                {
                    inTransition = false;

                    for (int i = 0; i < 2; i++)
                    {
                        effectsParticles[i].Stop();
                    }

                    forceFieldAudio.Stop();
                    cracklingAudio.Stop();
                }
            }

            //fade in/out
            forceFieldAudio.volume = maxVolForcefield * fadeFloat;
            cracklingAudio.volume = maxVolCrackling * fadeFloat;

            if (fadeFloat <= 0.7f)
            {
                runesWantedeColor.a = fadeFloat;
                runeRenderers[0].color = runesWantedeColor;
                runeRenderers[1].color = runesWantedeColor;
            }

            runes[0].Rotate(myTF.right, Time.deltaTime * -120f, Space.World);
            runes[1].Rotate(myTF.right, Time.deltaTime * 40f, Space.World);

            matInstance.SetFloat("_EmissionStrength", fadeFloat);

            rocksBaseTF.localPosition = rocksWantedPosition;
            rocksBaseTF.Rotate(myTF.up, Time.deltaTime * (fadeFloat * -120f));

            rocks[0].Rotate(myTF.forward, Time.deltaTime * (fadeFloat *  220f), Space.World);
            rocks[1].Rotate(myTF.forward, Time.deltaTime * (fadeFloat * -280f), Space.World);
            rocks[2].Rotate(myTF.forward, Time.deltaTime * (fadeFloat *  340f), Space.World);
            rocks[3].Rotate(myTF.forward, Time.deltaTime * (fadeFloat * -300f), Space.World);

            portalLight.intensity = maxIntPortalLight * fadeFloat;

            coreParticlesTF.localScale = new Vector3(fadeFloat, fadeFloat, fadeFloat);

            yield return null;
        }
    }

    private IEnumerator AnimateActiveEffects()
    {
        animating = true;

        Vector3 rocksWantedPosition = rocksBaseTF.localPosition;
        float randIntencity = maxIntPortalLight;
        float evalFloat = 0;

        while (animating)
        {
            //animate rocks
            evalFloat = Mathf.MoveTowards(evalFloat, 5f, Time.deltaTime * transitionSpeed);

            if (evalFloat == 5)//length of the curve is 5
                evalFloat = 0;

            rocksWantedPosition.y = rockAnimCurve.Evaluate(evalFloat) + 1.5f;
            rocksBaseTF.localPosition = rocksWantedPosition;

            rocksBaseTF.Rotate(myTF.up, Time.deltaTime * -120f, Space.World);

            rocks[0].Rotate(myTF.forward, Time.deltaTime *  220f, Space.World);
            rocks[1].Rotate(myTF.forward, Time.deltaTime * -280f, Space.World);
            rocks[2].Rotate(myTF.forward, Time.deltaTime *  340f, Space.World);
            rocks[3].Rotate(myTF.forward, Time.deltaTime * -300f, Space.World);

            runes[0].Rotate(myTF.right, Time.deltaTime * -120f, Space.World);
            runes[1].Rotate(myTF.right, Time.deltaTime *   40f, Space.World);

            //flicker the light
            if (portalLight.intensity == randIntencity)
                randIntencity = Random.Range(-0.5f, 0.5f) + maxIntPortalLight;
            portalLight.intensity = Mathf.MoveTowards(portalLight.intensity, randIntencity, Time.deltaTime * 1.5f);

            yield return null;
        }
    }

    private void Setup()
    {
        //Getting al references
        myTF = transform;
        coreParticlesTF = effectsParticles[0].transform;

        foreach (ParticleSystem part in effectsParticles)
        {
            ParticleSystem.MainModule mod = part.main;
            mod.startColor = effectsColor;
        }

        matInstance = runeStoneRenderer.material;
        matInstance.SetColor("_EmissionColor", effectsColor);
        matInstance.SetFloat("_EmissionStrength", 0);

        for (int i = 0; i < rocksBaseTF.childCount; i++)
        {
            rocks[i] = rocksBaseTF.GetChild(i);
            rocksBaseTF.GetChild(i).GetComponent<MeshRenderer>().material = matInstance;
        }

        runesWantedeColor = effectsColor;
        runesWantedeColor.a = 0f;

        for (int i = 0; i < runes.Length; i++)
        {
            runeRenderers[i] = runes[i].GetComponent<SpriteRenderer>();
            runeRenderers[i].color = runesWantedeColor;
        }

        forceFieldAudio.volume = 0f;
        cracklingAudio.volume = 0f;
        portalLight.color = effectsColor;
        portalLight.intensity = 0f;
    }
}
