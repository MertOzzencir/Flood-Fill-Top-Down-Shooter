using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;
    NavMeshAgent agent;
    Transform player;
    LivingEntity targetEntity;
    Material enemyMaterial;
    Color originialColor;
    public float refreshRate;
    float attackDistanceRef = 1.5f;
    float nextAttackTime;
    float timeBetweenAttacks = 1f;
    float collisionRadiusPlayer;
    float collisionRadiusEnemy;
    float damage = 1f;
    bool hasTarget;
    protected override void Start()
    {


        base.Start();
        agent = GetComponent<NavMeshAgent>();
        enemyMaterial = GetComponent<Renderer>().material;
        originialColor = enemyMaterial.color;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = player.GetComponent<LivingEntity>();

            targetEntity.OnDeath += OnTargetDeath;
            currentState = State.Chasing;
            collisionRadiusPlayer = player.GetComponent<CapsuleCollider>().radius;
            collisionRadiusEnemy = GetComponent<CapsuleCollider>().radius;

            StartCoroutine(PlayerPositionUpdate());
        }


    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    public void Update()
    {

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrtMagnitude = (player.position - transform.position).sqrMagnitude;
                if (sqrtMagnitude < Mathf.Pow(attackDistanceRef + collisionRadiusPlayer + collisionRadiusEnemy, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;


        Vector3 startPosition = transform.position;
        Vector3 dirToTarget = (player.position - transform.position).normalized;

        Vector3 targetPosition = player.position - dirToTarget * (collisionRadiusPlayer);

        float attackSpeed = 3f;

        float period = 0;
        bool hasApplied = false;

        while (period < 1f)
        {
            if (period >= .5f && !hasApplied)
            {
                hasApplied = true;
                targetEntity.TakeDamage(damage);
            }
            period += Time.deltaTime * attackSpeed;
            float interpolation = (-period * period + period) * 4;
            transform.position = Vector3.Lerp(startPosition, targetPosition, interpolation);
            enemyMaterial.color = Color.Lerp(originialColor, Color.yellow, interpolation);
            yield return null;
        }
        enemyMaterial.color = originialColor;
        currentState = State.Chasing;

    }
    IEnumerator PlayerPositionUpdate()
    {
        
        while (hasTarget)
        {
            if (player != null)
            {
                if (currentState == State.Chasing)
                {
                    Vector3 dirToTarget = (player.position - transform.position).normalized;

                    Vector3 playerPosition = player.position - dirToTarget * (collisionRadiusEnemy + collisionRadiusPlayer + attackDistanceRef / 2f);
                    if (!dead)
                    {
                        agent.SetDestination(playerPosition);
                    }

                }
                yield return new WaitForSeconds(refreshRate);

            }


        }


    }
}
