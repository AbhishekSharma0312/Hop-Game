using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float distance, time, xPos;

    private float speed;

    public bool isGameStarted;

    private void Start()
    {
        isGameStarted = false;
        speed = distance / time;
    }

    private void FixedUpdate()
    {
        if (!isGameStarted) { return; }


        Vector3 temp = new Vector3(Input.GetAxis("Horizontal") * speed * 2f, 0, speed) * Time.fixedDeltaTime;
        playerTransform.Translate(temp);
        temp = playerTransform.position;

        if(temp.x > xPos) temp.x = xPos;
        if(temp.x < -xPos) temp.x = -xPos;

        playerTransform.position = temp;
    }
}
