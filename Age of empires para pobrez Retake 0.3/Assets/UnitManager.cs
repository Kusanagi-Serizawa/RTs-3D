using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public int maxUnits = 100; // Número máximo de unidades permitidas
    private int currentUnitCount = 0;

    void Awake()
    {
        // Asegura que solo haya una instancia del UnitManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanCreateUnit()
    {
        return currentUnitCount < maxUnits;
    }

    public void RegisterUnit()
    {
        currentUnitCount++;
    }

    public void UnregisterUnit()
    {
        currentUnitCount--;
    }
}
