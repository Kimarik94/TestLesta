using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public float windForce = 0.1f;
    public Vector3[] windDirections;
    private int currentWindDirectionIndex = 0;

    public float changeDirectionInterval = 2f;
    private float timer = 0f;

    public PlayerController playerController;
    private bool isPlayerOnBlock = false;

    private void Start()
    {
        windDirections = new Vector3[]
        {
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            ChangeWindDirection();
            timer = 0f;
        }

        if (isPlayerOnBlock && playerController != null)
        {
            ApplyWindForce();
        }
    }

    private void ChangeWindDirection()
    {
        currentWindDirectionIndex = (currentWindDirectionIndex + 1) % windDirections.Length;
    }

    public void ApplyWindForce()
    {
        Vector3 windDirection = windDirections[currentWindDirectionIndex];
        playerController.ApplyExternalForce(windDirection * windForce);
    }

    public void SetPlayerOnBlock(PlayerController controller)
    {
        playerController = controller;
        isPlayerOnBlock = true;
    }

    public void ResetPlayerOnBlock()
    {
        playerController = null;
        isPlayerOnBlock = false;
    }
}
