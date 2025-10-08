using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform muzzle;        // 화살이 나갈 위치/방향(+X)
    [SerializeField] private GameObject arrowPrefab;  // 화살 프리팹 (Rigidbody2D 필요)

    [Header("Charge")]
    [SerializeField] private float minLaunchSpeed = 6f;   // 살짝 눌렀을 때 속도
    [SerializeField] private float maxLaunchSpeed = 22f;  // 풀차지 속도
    [SerializeField] private float maxChargeTime = 1.0f;  // 풀차지까지 걸리는 시간(초)

    [Header("Fire")]
    [SerializeField] private float fireCooldown = 0.05f;  // 연속 발사 보호

    private bool charging;
    private float chargeT;      // 0..1
    private float cooldownT;

    private void Update()
    {
        if (cooldownT > 0f)
            cooldownT -= Time.deltaTime;

        // E키를 누르면 충전 시작
        if (Input.GetKeyDown(KeyCode.E) && cooldownT <= 0f)
        {
            charging = true;
            chargeT = 0f;
        }

        // E키를 누르고 있는 동안 충전
        if (charging && Input.GetKey(KeyCode.E))
        {
            chargeT += Time.deltaTime / maxChargeTime;
            if (chargeT > 1f) chargeT = 1f; // 풀차지 제한
        }

        // E키를 떼면 발사
        if (charging && Input.GetKeyUp(KeyCode.E))
        {
            Fire(chargeT);
            charging = false;
            cooldownT = fireCooldown;
        }
    }

    private void Fire(float charge01)
    {
        if (!arrowPrefab || !muzzle) return;

        // 충전량 → 발사 속도
        float speed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, charge01);

        // 화살 생성 및 초기 속도 부여
        var go = Instantiate(arrowPrefab, muzzle.position, muzzle.rotation);
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = (Vector2)(muzzle.right * speed);
        }
    }
}
