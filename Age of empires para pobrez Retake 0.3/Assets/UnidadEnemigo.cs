using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnidadEnemigo : MonoBehaviour
{
    public float detectionRadius = 10f; // Radio de detección para las unidades del jugador y la base
    public float attackDistance = 2f; // Distancia para comenzar el ataque
    public float attackCooldown = 1f; // Tiempo entre ataques
    public int maxHealth = 100; // Vida máxima de la unidad enemiga
    public int attackDamage = 10; // Daño del ataque

    private NavMeshAgent navMeshAgent;
    private GameObject currentTargetUnit;
    private GameObject currentTargetBase;
    private int currentHealth;
    private float lastAttackTime;
    private string playerUnitTag = "PlayerUnit"; // Etiqueta para las unidades del jugador
    private string baseTag = "Base"; // Etiqueta para la base del jugador

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown; // Permite atacar inmediatamente
    }

    void Update()
    {
        if (currentTargetUnit == null && currentTargetBase == null)
        {
            FindTargets();
        }
        else
        {
            ChaseAndAttackTarget();
        }
    }

    void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerUnitTag))
            {
                currentTargetUnit = hitCollider.gameObject;
                break;
            }
            else if (hitCollider.CompareTag(baseTag))
            {
                currentTargetBase = hitCollider.gameObject;
                break;
            }
        }
    }

    void ChaseAndAttackTarget()
    {
        if (currentTargetUnit != null)
        {
            ChaseAndAttack(currentTargetUnit);
        }
        else if (currentTargetBase != null)
        {
            ChaseAndAttack(currentTargetBase);
        }
    }

    void ChaseAndAttack(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackDistance)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                AttackTarget(target);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            navMeshAgent.destination = target.transform.position;
        }
    }

    void AttackTarget(GameObject target)
    {
        if (target.CompareTag(playerUnitTag))
        {
            Unit playerUnit = target.GetComponent<Unit>();
            if (playerUnit != null)
            {
                playerUnit.TakeDamage(attackDamage);
                Debug.Log("Atacando a la unidad del jugador.");
            }
        }
        else if (target.CompareTag(baseTag))
        {
            Base playerBase = target.GetComponent<Base>();
            if (playerBase != null)
            {
                playerBase.TakeDamage(attackDamage);
                Debug.Log("Atacando la base del jugador.");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Unidad enemiga destruida.");
    }
}
