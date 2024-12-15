using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject comboPrefab;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if(playerTransform == null) { return; }

        float playerPosZ = playerTransform.position.z;
        float currentZ = transform.position.z;

        if(playerPosZ -  currentZ > 15f)
        {
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        GameManager.Instance.updateComboAnimation -= UpdateComboAnimation;
    }

    public void SubscribeToMethod()
    {
        GameManager.Instance.updateComboAnimation += UpdateComboAnimation;
    }

    void UpdateComboAnimation(bool isCombo)
    {
        comboPrefab.SetActive(isCombo);
    }
}
