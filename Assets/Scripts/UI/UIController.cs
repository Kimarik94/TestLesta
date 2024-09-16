using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public RectTransform WinPanel;
    public Button winRestartButton;

    public RectTransform DeathPanel;
    public Button restartButton;

    public GameObject timer;

    public GameObject currentPlayerHP;
    private int currentHP;

    public bool timerStart = false;

    private float elapsedTime = 0f;

    private void Start()
    {
        currentHP = 5;
        currentPlayerHP.GetComponent<TextMeshProUGUI>().text = currentHP.ToString();
        timer.GetComponent<TextMeshProUGUI>().text = "00:00";
        DeathPanel.localScale = Vector3.zero;
        WinPanel.localScale = Vector3.zero;
        restartButton.onClick.AddListener(GameObject.Find("GameManager").GetComponent<GameManager>().RestartGame);
        winRestartButton.onClick.AddListener(GameObject.Find("GameManager").GetComponent<GameManager>().RestartGame);
    }

    private void Update()
    {
        if (timerStart)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
        }
        if (!timerStart)
        {
            elapsedTime = 0f;
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        // Рассчитываем минуты и секунды
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Обновляем текст TextMeshPro в формате "00:00"
        timer.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TakeDamage()
    {
        if (currentHP > 0)
        {
            currentHP--;
            currentPlayerHP.GetComponent<TextMeshProUGUI>().text = currentHP.ToString();
        }
    }
}
