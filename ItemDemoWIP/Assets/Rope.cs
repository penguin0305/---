using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleRopeMinimal : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform muzzle;        // 발사 기준(+X가 정면)
    [SerializeField] private Rigidbody2D playerRb;    // 플레이어 RB
    [SerializeField] private LayerMask groundMask;    // 붙을 표면

    [Header("Rope Settings")]
    [SerializeField] private float fireAngleDeg = 45f; // 항상 월드 기준 +45°
    [SerializeField] private float maxDistance = 25f;  // 사정거리
    [SerializeField] private float impulse = 25f;      // 한 번만 줄 임펄스 힘
    [SerializeField] private float ropeWidth = 0.05f;  // 로프 굵기
    [SerializeField] private float controlLockTime = 0.25f; // 임펄스 직후 입력 잠금 시간

    private LineRenderer line;
    private Vector2 anchor;
    private bool attached;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.startWidth = ropeWidth;
        line.endWidth = ropeWidth;
        line.enabled = false;
        line.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!attached) Fire();
            else PullOnceAndDetach();
        }

        // 로프 시각화
        if (attached)
        {
            line.SetPosition(0, muzzle ? muzzle.position : (Vector3)playerRb.position);
            line.SetPosition(1, anchor);
        }
    }

    private void Fire()
    {
        if (!muzzle || !playerRb) return;

        // 월드 기준 오른쪽 + fireAngleDeg (기본 45°) 방향
        Vector2 dir = Rotate(Vector2.right, fireAngleDeg).normalized;

        // groundMask에 첫 히트 지점 찾기
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, dir, maxDistance, groundMask);
        if (!hit) return;

        anchor = hit.point;
        attached = true;

        // 로프 표시
        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, muzzle.position);
        line.SetPosition(1, anchor);
    }

    void PullOnceAndDetach()
    {
        if (!attached) return;

        var pc = playerRb.GetComponent<PlayerController>();
        if (pc) pc.canControl = false;

        Vector2 from = playerRb.worldCenterOfMass;
        Vector2 dir = (anchor - from).normalized;
        playerRb.AddForce(dir * impulse, ForceMode2D.Impulse);

        attached = false;
        line.enabled = false;

        // delay초 후 자동 실행
        if (pc) Invoke(nameof(RestoreControl), controlLockTime);
    }

    void RestoreControl()
    {
        var pc = playerRb.GetComponent<PlayerController>();
        if (pc) pc.canControl = true;
    }

    private static Vector2 Rotate(Vector2 v, float deg)
    {
        float r = deg * Mathf.Deg2Rad;
        float c = Mathf.Cos(r), s = Mathf.Sin(r);
        return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
    }
}