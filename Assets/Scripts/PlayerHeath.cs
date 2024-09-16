using UnityEngine;
using TMPro;

public class PlayerHeath : MonoBehaviour
{
    public TextMeshPro playerHP;

    public int playerCurrentHP;
    private int playerMaxHP = 5;
    private int playerMinHP = 0;
    private int playerHPDamage = 1;

    public bool isPlayerDead;

    private void Start()
    {
        playerCurrentHP = playerMaxHP;
        isPlayerDead = false;
    }

    public void TakeDamage()
    {
        if (playerCurrentHP > playerMinHP && playerCurrentHP != 0)
        {
            playerCurrentHP -= playerHPDamage;
            GameObject.Find("MainPanel").GetComponent<UIController>().TakeDamage();
        }
        else if(playerCurrentHP == 0)
        {
            isPlayerDead = true;
            GameObject.Find("MainPanel").GetComponent<UIController>().timerStart = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowDeadScreen();
        }
        else
        {
            return;
        }
    }
}
