using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    
    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0, 2, -10);
    }
}
