using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FashionMenu : MonoBehaviour //this is not finished
{
    public SkinnedMeshRenderer fox, haori, pants;
    public MeshRenderer waki;
    public FashionSet[] outfits;
    private bool demo = false;
    private float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && Input.GetButtonDown("Attack") && cooldown < Time.time)
        {
            if (demo)
            {
                fox.material = outfits[0].fox;
                haori.material = outfits[0].haori;
                pants.material = outfits[0].pants;
                waki.material = outfits[0].waki;
                demo = false;
            }
            else
            {
                fox.material = outfits[1].fox;
                haori.material = outfits[1].haori;
                pants.material = outfits[1].pants;
                waki.material = outfits[1].waki;
                demo = true;
            }
            ParticleSystem flames = waki.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = flames.main;
            main.startColor = new ParticleSystem.MinMaxGradient(waki.material.color, waki.material.GetColor("_EmissionColor"));
            cooldown = Time.time + 0.1f;
        }
    }
}

[System.Serializable]
public class FashionSet //this class allows different sounds to add cooldown times that differ from their sound length
{
    public Material fox, haori, pants, waki;
}
