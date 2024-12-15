using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 offset;

    public bool canFollow;

    private void Start()
    {
        canFollow = true;
        offset = transform.position - playerTransform.position;
    }

    private void FixedUpdate()
    {
        if (!canFollow) { return; }

        transform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z + offset.z);
    }

}
