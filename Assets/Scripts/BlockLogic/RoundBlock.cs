using UnityEngine;

public class RoundBlock : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float rotationAngle = 5f;

    public bool playerOnBlock;

    public Vector3 rotationAxis;
    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private void Start()
    {
        defaultPos = gameObject.transform.position;
        defaultRot = gameObject.transform.rotation;

        playerOnBlock = false;
    }

    private void Update()
    {
        if (playerOnBlock)
        {
            RotateBlock();
        }
        if(!playerOnBlock)
        {
            transform.position = Vector3.Lerp(transform.position, defaultPos, rotationSpeed/2 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, rotationSpeed/2 * Time.deltaTime);
        }
    }

    public void RotateBlock()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetRandomAxis()
    {
        float x = Random.Range(-rotationAngle, rotationAngle);
        float y = Random.Range(-rotationAngle, rotationAngle);
        float z = Random.Range(-rotationAngle, rotationAngle);

        return new Vector3(x, y, z);
    }
}