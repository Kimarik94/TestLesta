using UnityEngine;

public class HeatBlock : MonoBehaviour
{
    private float heatTimer = 5f;
    private float standOnTimer = 0f;
    public bool playerPlacedOnBlock;

    private Material blockMaterial;
    private Color initialColor;
    public Color heatedColor = Color.red;

    public PlayerHeath playerHealth;

    private void Start()
    {
        blockMaterial = GetComponent<Renderer>().material;
        initialColor = blockMaterial.color;

        playerPlacedOnBlock = false;
    }

    private void Update()
    {
        if (playerPlacedOnBlock)
        {
            standOnTimer += Time.deltaTime;

            float lerpValue = standOnTimer / heatTimer;
            blockMaterial.color = Color.Lerp(initialColor, heatedColor, lerpValue);

            if (standOnTimer >= heatTimer)
            {
                playerHealth.TakeDamage();
                ResetBlock();
            }
        }
        else
        {
            ResetBlock();
        }
    }

    public void ResetBlock()
    {
        standOnTimer = 0f;
        blockMaterial.color = initialColor;
    }

    
}
