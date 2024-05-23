using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ControladorSelecUnidad.Instance.TodasUnidads.Add(gameObject);
    }

    public void OnDestroy() 
    {
        ControladorSelecUnidad.Instance.TodasUnidads.Remove(gameObject);
    }
}
