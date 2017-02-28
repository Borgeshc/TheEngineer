﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityAI : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float stoppingDistance;

    public int damage;
    public float attackFrequency;

    Health playerHealth;
    bool attacking;
    Health myHealth;
    public Animator anim;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        myHealth = GetComponent<Health>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (myHealth.health > 0 && player != null)
            {
                if (player.activeInHierarchy && myHealth.health > 0)
                {
                    transform.LookAt(player.transform);
                    if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance)
                    {
                        anim.SetBool("Attack", false);
                        nav.SetDestination(player.transform.position);
                    }
                    else
                    {
                        if (!attacking)
                        {
                            attacking = true;
                            StartCoroutine(Attack());
                        }
                    }
                }
            }
            else
                nav.Stop();
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("Attack", true);
        player.GetComponent<Health>().TookDamage(damage);
        yield return new WaitForSeconds(attackFrequency);
        attacking = false;
    }
}
