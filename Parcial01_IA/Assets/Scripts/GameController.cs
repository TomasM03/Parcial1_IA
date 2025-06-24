using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int objetivosVivos;

    void OnEnable()
    {
        AIController.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        AIController.OnDeath -= HandleDeath;
    }

    void Start()
    {
        objetivosVivos = FindObjectsOfType<AI2Controller>().Length;
        Debug.Log(objetivosVivos);
    }

    void HandleDeath(AIController ai)
    {
        if (ai is AI2Controller)
        {
            objetivosVivos--;
            if (objetivosVivos <= 0)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }

}
