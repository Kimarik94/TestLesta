using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspBlock : MonoBehaviour
{
    private float transTimer = 2f;
    private float standOnTimer = 0f;
    public bool playerPlacedOnBlock;
    public bool stopTransformation;

    private Vector3 initialScale;

    private Material blockMaterial;
    private Color initialColor;

    public Color transColor;

    private void Start()
    {
        blockMaterial = GetComponent<Renderer>().material;
        initialColor = blockMaterial.color;
        initialScale = gameObject.transform.localScale;

        transColor = new Color(initialColor.r,initialColor.g,initialColor.b, 0f);

        playerPlacedOnBlock = false;
        stopTransformation = false;
    }

    private void Update()
    {
        if (playerPlacedOnBlock && !stopTransformation)
        {
            standOnTimer += Time.deltaTime;

            float lerpValue = standOnTimer / transTimer;
            blockMaterial.color = Color.Lerp(initialColor, transColor, lerpValue);

            if (standOnTimer >= transTimer - 0.5f)
            {
                blockMaterial.color = transColor;
                transform.localScale = Vector3.zero;
                stopTransformation = true;
            }
        }
    }


    public void ResetBlock()
    {
        standOnTimer = 0f;
        blockMaterial.color = initialColor;
        gameObject.transform.localScale = initialScale;
        stopTransformation = false;
    }
}
