using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorSelecUnidad : MonoBehaviour
{
    public static ControladorSelecUnidad Instance {get; set; }

    public LayerMask clickable, ground;
    public GameObject groundMaker;
    private Camera cam;
    public List<GameObject> TodasUnidads = new List<GameObject>();
    public List<GameObject> UnidadesSelc =new List<GameObject>();
    private void Start() 
    {
        cam=Camera.main;
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray=cam.ScreenPointToRay(Input.mousePosition);
            
            //Si toca un objeto que se puede cliquear
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectPorClick(hit.collider.gameObject);  
                }
            }
            else//Si no lo toca
            {
                if (Input.GetKey(KeyCode.LeftShift)==false)
                {
                    DeselecAll();
                }
                
            }
        }
         if (Input.GetMouseButtonDown(1) && UnidadesSelc.Count>0)
        {
            RaycastHit hit;
            Ray ray=cam.ScreenPointToRay(Input.mousePosition);
            
            //Si toca un objeto que se puede cliquear
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, ground))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    groundMaker.transform.position=hit.point;
                    groundMaker.SetActive(false);
                    groundMaker.SetActive(true);
                }
            }
        }
    }

    public void MultiSelect(GameObject unidad)
    {
        if (UnidadesSelc.Contains(gameObject)==false)
        {
            UnidadesSelc.Add(gameObject);
            SelecUnidad(unidad,true);
        }
        else
        {
            SelecUnidad(unidad, false);
            UnidadesSelc.Remove(unidad);
        }
    }

    public void DeselecAll()
    {
        foreach (var unidad in UnidadesSelc)
        {
            SelecUnidad(unidad, false);
        }
        groundMaker.SetActive(false);
        UnidadesSelc.Clear();
    }

    public void SelectPorClick(GameObject unidad)
    {
        DeselecAll();
        UnidadesSelc.Add(unidad);
        SelecUnidad(unidad, true);
    }

    public void ActivarMovUnidad(GameObject unidad, bool debeMover)
    {
        unidad.GetComponent<UnidadMove>().enabled=debeMover;
    }
    public void TriggerSelectionIndicador(GameObject unidad, bool esVisible)
    {
        unidad.transform.GetChild(0).gameObject.SetActive(esVisible);
    }
    public void SelecUnidad(GameObject unidad, bool esSelec)
    {
        TriggerSelectionIndicador(unidad, esSelec);
        ActivarMovUnidad(unidad,esSelec);
    }
    internal void ArrastreSelect(GameObject unidad)
    {
        if (UnidadesSelc.Contains(unidad)==false)
        {
            UnidadesSelc.Add(unidad);
            SelecUnidad(unidad,true);
        }
    }
}
