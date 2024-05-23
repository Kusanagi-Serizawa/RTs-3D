using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject unitPrefab; // Prefab de la unidad que se instanciará
    public Transform spawnPoint; // Punto de spawn donde se instanciará la unidad
    public float spawnDelay = 7f; // Tiempo de pausa entre cada aparición

    void Start()
    {
        StartCoroutine(SpawnUnitsWithDelay());
    }

    IEnumerator SpawnUnitsWithDelay()
    {
        while (true)
        {
            SpawnUnit();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnUnit()
    {
        Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
