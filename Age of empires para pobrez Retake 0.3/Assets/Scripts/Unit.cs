using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public float detectionRadius = 10f; // Radio de detección para las unidades enemigas
    public float attackDistance = 2f; // Distancia para comenzar el ataque
    public float attackCooldown = 1f; // Tiempo entre ataques
    public int maxHealth = 100; // Vida máxima de la unidad del jugador
    public int attackDamage = 10; // Daño del ataque
    public string enemyUnitTag = "EnemyUnit"; // Etiqueta de las unidades enemigas
    public string enemyBaseTag = "EnemyBase"; // Etiqueta de las bases enemigas

    private NavMeshAgent navMeshAgent;
    private GameObject currentTargetUnit;
    private GameObject selectedTargetBase;
    private int currentHealth;
    private float lastAttackTime;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown; // Permite atacar inmediatamente
    }

    void Update()
    {
        if (selectedTargetBase != null)
        {
            ChaseAndAttack(selectedTargetBase);
        }
        else if (currentTargetUnit == null)
        {
            FindEnemyUnits();
        }
        else
        {
            ChaseAndAttack(currentTargetUnit);
        }
    }

    void FindEnemyUnits()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyUnitTag))
            {
                currentTargetUnit = hitCollider.gameObject;
                break;
            }
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
        if (target.CompareTag(enemyUnitTag))
        {
            UnidadEnemigo enemyUnit = target.GetComponent<UnidadEnemigo>();
            if (enemyUnit != null)
            {
                enemyUnit.TakeDamage(attackDamage);
                Debug.Log("Atacando a la unidad enemiga.");
            }
        }
        else if (target.CompareTag(enemyBaseTag))
        {
            EnemyBase enemyBase = target.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.TakeDamage(attackDamage);
                Debug.Log("Atacando la base enemiga.");
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
        Debug.Log("Unidad del jugador destruida.");
    }

    // Método para seleccionar una base enemiga como objetivo
    public void SelectTargetBase(GameObject targetBase)
    {
        if (targetBase.CompareTag(enemyBaseTag))
        {
            selectedTargetBase = targetBase;
            currentTargetUnit = null; // Descartar cualquier unidad enemiga actual como objetivo
        }
    }
    public void SelectEnemyBase(GameObject enemyBase)
{
    Unit[] playerUnits = FindObjectsOfType<Unit>();
    foreach (var unit in playerUnits)
    {
        unit.SelectTargetBase(enemyBase);
    }
}
}
