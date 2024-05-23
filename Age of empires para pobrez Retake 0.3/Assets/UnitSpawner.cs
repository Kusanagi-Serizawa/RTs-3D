
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab; // Prefab de la unidad a crear
    public Transform spawnPoint; // Punto donde la unidad aparecerá
    public float spawnInterval = 5f; // Intervalo entre la creación de unidades
    private float spawnTimer = 0f;
    public NeutralBase.Faction baseFaction; // Facción que controla esta base
    //public UnitCreationUi unitCreationUi;
    private bool canSpawn = true;

    void Update()
    {
        if (!canSpawn)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                canSpawn = true;
                spawnTimer = 0f;
            }
        }

        if (baseFaction == NeutralBase.Faction.Player)
        {
            // Aquí podrías tener lógica específica para la creación de unidades por el jugador
            // por ejemplo, a través de un botón de la interfaz de usuario (UI)
        }
        else if (baseFaction == NeutralBase.Faction.Enemy && canSpawn)
        {
            TrySpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        if (canSpawn && UnitManager.Instance.CanCreateUnit())
        {
            Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
            UnitManager.Instance.RegisterUnit();
            Debug.Log("Unit created at: " + transform.position + " by " + baseFaction);
            canSpawn = false;
        }
    }

    public void TrySpawnUnit()
    {
        if (UnitManager.Instance.CanCreateUnit())
        {
            SpawnUnit();
        }
    }

    public void SetFaction(NeutralBase.Faction newFaction)
    {
        baseFaction = newFaction;
    }
}
