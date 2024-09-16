using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public GameObject ui;

    public static bool isGameStarted = false;

    private void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        ui = GameObject.Find("MainPanel");

        Vector3 spawnPos = spawnPoint.transform.position;
        Quaternion spawnRot = spawnPoint.transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        if(GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPos, spawnRot);
            isGameStarted = true;
        }
    }

    private void Update()
    {
        if(spawnPoint == null && ui == null)
        {
            spawnPoint = GameObject.Find("SpawnPoint");
            ui = GameObject.Find("MainPanel");
        }
    }

    public void RestartGame()
    {
        Scene Scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Scene.name);

        if (GameObject.FindWithTag("Player") != null) Destroy(GameObject.FindWithTag("Player"));

        Vector3 spawnPos = spawnPoint.transform.position;
        Quaternion spawnRot = spawnPoint.transform.rotation;

        Instantiate(playerPrefab, spawnPos, spawnRot);
        isGameStarted = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowDeadScreen()
    {
        GameObject.Find("MainPanel").GetComponent<UIController>().timerStart = false;
        Cursor.lockState = CursorLockMode.None;
        if (GameObject.FindWithTag("Player") != null) Destroy(GameObject.FindWithTag("Player"));
        ui.GetComponent<UIController>().DeathPanel.localScale = Vector3.one;
    }

    public void ShowWinScreen()
    {
        GameObject.Find("MainPanel").GetComponent<UIController>().timerStart = false;
        Cursor.lockState = CursorLockMode.None;
        if (GameObject.FindWithTag("Player") != null) Destroy(GameObject.FindWithTag("Player"));
        ui.GetComponent<UIController>().WinPanel.localScale = Vector3.one;
    }
}
