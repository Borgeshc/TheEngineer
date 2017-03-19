using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sarathos : MonoBehaviour
{
    public static bool attackPlayer;
    public float speed;
    public float animationWaitTime;
    public float attackFrequency;
    GameObject player;
    Animator anim;
    NavMeshAgent nav;
    bool attacking;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(attackPlayer)
        {
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

            if (nav.remainingDistance > nav.stoppingDistance)
            {
                anim.SetBool("IsMoving", true);
                nav.SetDestination(player.transform.position);
            }
            else
            {
                anim.SetBool("IsMoving", false);
                if(!attacking)
                {
                    attacking = true;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(animationWaitTime);
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackFrequency);
        attacking = false;
    }
}
