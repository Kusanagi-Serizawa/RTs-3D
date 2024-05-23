using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string victorySceneName = "VictoryScene"; // Nombre de la escena de victoria
    public string defeatSceneName = "DefeatScene"; // Nombre de la escena de derrota
    public GameObject[] playerUnits; // Array de unidades del jugador
    public GameObject[] enemyUnits; // Array de unidades enemigas

    void Update()
    {
        CheckVictoryCondition();
    }

    void CheckVictoryCondition()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");

        if (playerUnits.Length == 0)
        {
            LoadScene(defeatSceneName);
        }
        else if (enemyUnits.Length == 0)
        {
            LoadScene(victorySceneName);
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
