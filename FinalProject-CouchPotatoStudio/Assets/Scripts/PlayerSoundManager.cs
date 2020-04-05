using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioClip swordSwing, dashNoise; //these sounds will be played from the sword source
    public EffortSound[] jumpEfforts, dashEfforts, attackEfforts, damageEfforts, victoryEfforts, deathEfforts; //categories of sounds to play from the player source
    public int[] failRates = new int[6]; //higher values increase the chance to not play an effort sounds for each category. victory and death should have 0 here.
    public AudioSource playerEffortSource, swordSource;
    private float effortCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 0 = jump, 1 = dash, 2 = attack, 3 = damage, 4 = victory, 5 = death
    /// </summary>
    /// <param name="category">the category of sounds to play</param>
    public void PlayEffortSound(int category)
    {
        EffortSound[] cat; //the target category of effort sounds to use
        switch (category)
        {
            case 0:
                cat = jumpEfforts;
                break;
            case 1:
                cat = dashEfforts;
                break;
            case 2:
                cat = attackEfforts;
                break;
            case 3:
                cat = damageEfforts;
                break;
            case 4:
                cat = victoryEfforts;
                break;
            case 5:
                cat = deathEfforts;
                break;
            default:
                cat = new EffortSound[0];
                break;
        }
        int sound = Random.Range(0, cat.Length + failRates[category]); //choose the target sound to play from the selected category
        if (sound > cat.Length - 1) //if the value is greater than the number of sounds for that category, do not play a sound
        {
            return;
        }
        if (effortCooldown > Time.time && (category != 4 && category != 5)) //if there is cooldown do not play a sound. victory and death sounds ignore this
        {
            return;
        }
        if (category == 0) //the pitch for jump efforts gets a small amount of variance
        {
            playerEffortSource.pitch = Random.Range(0.9f, 1.1f);
        }
        else
        {
            playerEffortSource.pitch = 1;
        }
        playerEffortSource.clip = cat[sound].sound; //assign the sound
        playerEffortSource.Play(); //play the sound
        if (cat[sound].cooldown != 0)
        {
            effortCooldown = Time.time + cat[sound].cooldown;
        }
        else
        {
            effortCooldown = Time.time + playerEffortSource.clip.length; //set the sound cooldown to the length of the sound by default
        }
    }

    public void PlaySwordSound(bool dash)
    {
        if (dash)
        {
            swordSource.clip = dashNoise;
        }
        else
        {
            swordSource.clip = swordSwing;
        }
        swordSource.pitch = Random.Range(0.85f, 1.15f);
        swordSource.Play();
    }
}

[System.Serializable]
public class EffortSound //this class allows different sounds to add cooldown times that differ from their sound length
{
    public AudioClip sound;
    public float cooldown;
}
