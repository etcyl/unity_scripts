using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public string mob_type;
    public float base_exp;
    public float exp2Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDisable()
    {
        exp2Player += base_exp;

        if(mob_type == "Elite")
        {
            exp2Player = (float)exp2Player* 1.5f;
        }
        else if(mob_type == "Boss")
        {
            exp2Player *= 2f;
        }
        UnityEngine.Debug.Log("Set player exp to gain: " + exp2Player.ToString());
        PlayerPrefs.SetFloat("Experience", exp2Player);
    }
}
