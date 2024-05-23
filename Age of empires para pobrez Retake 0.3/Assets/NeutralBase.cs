using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralBase : MonoBehaviour
{
    public enum Faction { Neutral, Player, Enemy }
    public Faction currentFaction = Faction.Neutral;
    public int playerUnitsNearby = 0;
    public int enemyUnitsNearby = 0;
    public float checkInterval = 1f;
    private float checkTimer = 0f;
    private UnitSpawner unitSpawner;

    void Start()
    {
        unitSpawner = GetComponent<UnitSpawner>();
        if (unitSpawner != null)
        {
            unitSpawner.SetFaction(currentFaction);
        }
    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            CheckConquest();
            checkTimer = 0f;
        }
    }

    void CheckConquest()
    {
        if (playerUnitsNearby > enemyUnitsNearby)
        {
            ChangeFaction(Faction.Player);
        }
        else if (enemyUnitsNearby > playerUnitsNearby)
        {
            ChangeFaction(Faction.Enemy);
        }
    }

    void ChangeFaction(Faction newFaction)
    {
        if (currentFaction != newFaction)
        {
            currentFaction = newFaction;
            Debug.Log("Base conquered by: " + newFaction);
            if (unitSpawner != null)
            {
                unitSpawner.SetFaction(newFaction);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            playerUnitsNearby++;
        }
        else if (other.CompareTag("EnemyUnit"))
        {
            enemyUnitsNearby++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            playerUnitsNearby--;
        }
        else if (other.CompareTag("EnemyUnit"))
        {
            enemyUnitsNearby--;
        }
    }
}
