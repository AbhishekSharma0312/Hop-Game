
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float distance, time;


    private float speed, startSpeed, accelration;

    public bool isGameStarted;

    private void Start()
    {
        isGameStarted = false;

        startSpeed = 2 * distance / time;
        accelration = -0.995f * startSpeed / time;
        speed = startSpeed;
    }

    private void FixedUpdate()
    {
       // if (!isGameStarted) { return; }
        
        speed += accelration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        ballTransform.localPosition += temp;
        temp = ballTransform.localPosition;

        if(temp.y < 0f)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, Vector3.down, 10f))
            {
                GameManager.Instance.GameOver();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Physics.Raycast(transform.position, Vector3.down, out hit, 10f);
                GameObject tempBlock = hit.transform.gameObject;
                GameManager.Instance.UpdateScore(tempBlock);
            }

            speed = startSpeed;
            GameManager.Instance.SpawnBlock();
        }
    }
}
