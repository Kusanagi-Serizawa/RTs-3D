using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start ()
    {

    }
    void Update ()
    {
        // Detectar la tecla Esc para regresar al menú principal
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadSceneGame("Menu"); // Asegúrate de que "MenuPrincipal" sea el nombre correcto de tu escena de menú principal
            Debug.Log("Menu :D");
        }
    }
    public void LoadSceneGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Gracias por Jugar ;D");
    }
}
