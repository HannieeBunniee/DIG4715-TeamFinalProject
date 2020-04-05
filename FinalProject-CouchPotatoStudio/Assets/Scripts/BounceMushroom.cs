using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMushroom : MonoBehaviour
{
    public AudioSource bounceSound;
    public Animator mushroomAnimator;
    public float bounce;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject candidate in playerObjects)
        {
            if (candidate.GetComponent<PlayerMovement>() != null)
            {
                player = candidate.GetComponent<PlayerMovement>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.velocity.y = bounce;
            bounceSound.Play();
            mushroomAnimator.SetTrigger("Bounce");
        }
    }
}
