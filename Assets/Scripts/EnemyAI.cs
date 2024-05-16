
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public float chaseRange = 5f;
    [SerializeField] float returnRange = 20f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] TextMeshProUGUI zombiesKilled;
    public AudioClip deathSound;
    public AudioClip shoutClip;

    private Transform target;
    NavMeshAgent navMeshAgent;
    new Vector3 startPosition;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    AudioSource audioSource;

    void Start()
    {
        zombiesKilled = GameObject.FindGameObjectWithTag("KillNumber").GetComponent<TextMeshProUGUI>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        GetComponent<Animator>().SetTrigger("idle");
    }

    void Update()
    {
        SphereAction();
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {   
        if (!health.IsDead())
        {
            FaceTarget();
            if(distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }
            if(distanceToTarget <= navMeshAgent.stoppingDistance + 0.5f)
            {
                AttackTarget();
            }
            else if(distanceToTarget >= returnRange)
            {
                isProvoked = false;
                navMeshAgent.SetDestination(startPosition);
            }
        }
    }

    void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void SphereAction()
    {
        if (health.IsDead())
        {
            ZombieKilledText();
            audioSource.Stop();
            audioSource.clip = deathSound;
            audioSource.PlayOneShot(deathSound, 1.5f);
            transform.GetChild(2).GetComponent<CapsuleCollider>().enabled = false;
            transform.GetChild(3).GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            navMeshAgent.enabled = false;
            this.enabled = false;
            StartCoroutine(DestroyZombie());
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);
            
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            audioSource.Play();
            audioSource.PlayOneShot(shoutClip, 1f);
            isProvoked = true;
        }

        if(transform.position.x <= startPosition.x +2 
        && transform.position.x >= startPosition.x -2 && !isProvoked)
        {
            GetComponent<Animator>().SetTrigger("idle");
            audioSource.Stop();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, returnRange);
    }

    void ZombieKilledText()
    {
        int currentValue = int.Parse(zombiesKilled.text);
        int newValue = currentValue + 1;
        zombiesKilled.text = newValue.ToString();
    }

    IEnumerator DestroyZombie()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
