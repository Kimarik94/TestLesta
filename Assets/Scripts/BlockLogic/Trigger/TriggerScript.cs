using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (gameObject.GetComponentInParent<HeatBlock>() != null)
            {
                gameObject.GetComponentInParent<HeatBlock>().playerPlacedOnBlock = true;
                gameObject.GetComponentInParent<HeatBlock>().playerHealth = other.GetComponent<PlayerHeath>();
            }

            if (gameObject.GetComponentInParent<WindBlock>() != null)
            {
                gameObject.GetComponentInParent<WindBlock>().SetPlayerOnBlock(playerController);
            }

            if(gameObject.GetComponentInParent<RoundBlock>() != null)
            {
                gameObject.GetComponentInParent<RoundBlock>().rotationAxis =
                    gameObject.GetComponentInParent<RoundBlock>().GetRandomAxis();
                gameObject.GetComponentInParent<RoundBlock>().playerOnBlock = true;
            }

            if (gameObject.GetComponentInParent<TranspBlock>() != null)
            {
                gameObject.GetComponentInParent<TranspBlock>().playerPlacedOnBlock = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.GetComponentInParent<HeatBlock>() != null)
            {
                gameObject.GetComponentInParent<HeatBlock>().playerPlacedOnBlock = false;
                gameObject.GetComponentInParent<HeatBlock>().ResetBlock();
            }

            if (gameObject.GetComponentInParent<WindBlock>() != null)
            {
                gameObject.GetComponentInParent<WindBlock>().ResetPlayerOnBlock();
            }

            if (gameObject.GetComponentInParent<RoundBlock>() != null)
            {
                gameObject.GetComponentInParent<RoundBlock>().playerOnBlock = false;
            }

            if (gameObject.GetComponentInParent<TranspBlock>() != null)
            {
                gameObject.GetComponentInParent<TranspBlock>().playerPlacedOnBlock = false;
                if(!gameObject.GetComponentInParent<TranspBlock>().stopTransformation)
                gameObject.GetComponentInParent<TranspBlock>().ResetBlock();
            }
        }
    }
}
