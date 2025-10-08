using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;    // 총알 속도
    [SerializeField] private float lifeTime = 2f;  // 자동 파괴 시간

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 앞으로 (local right 방향) 발사
        rb.linearVelocity = transform.right * speed;
        // lifeTime 후 자동 파괴
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player는 무시 (자기 총알이 자기에게 닿는 일 방지)
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Item")) return;

        // 부딪히면 바로 파괴
        Destroy(gameObject);
    }
}
