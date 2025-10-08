using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8f; // 너무 멀리 가면 자동 파괴

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}