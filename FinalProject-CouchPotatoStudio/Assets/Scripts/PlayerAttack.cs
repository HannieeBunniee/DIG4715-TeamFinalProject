using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cam;
    public GameObject Hand;
    public Weapon myWeapon;

    //===Start====
    void Start()
    {
        myWeapon = Hand.GetComponentInChildren<Weapon>();
    }

    //===Update====
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            DoAttack();
        }
    }

    private void DoAttack()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, myWeapon.attackRange))
        {
            if(hit.collider.tag == "Enemy")
            {
                EnemyController eHealth = hit.collider.GetComponent<EnemyController>();
                eHealth.TakeDamage(myWeapon.attackDamage);
            }
        }
    }
}
