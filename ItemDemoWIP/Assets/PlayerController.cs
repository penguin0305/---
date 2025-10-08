using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Move")]
	[SerializeField] private float move_speed = 5f;

	[Header("Jump")]
	[SerializeField] private float jump_force = 5f;
	[SerializeField] private int max_air_jump = 0;
	[SerializeField] private LayerMask ground_layer;

	private Rigidbody2D rb;
	private Collider2D coll;
	private float input_x;
	private bool jump_queued;

	// 🚀 Grapple이 쏠 때 이걸 false로 바꿔서 이동/점프 잠금
	[HideInInspector] public bool canControl = true;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}

	private void Update()
	{
		if (!canControl) return; // 컨트롤 잠겨있으면 입력 무시

		input_x = Input.GetAxisRaw("Horizontal");

		if (Input.GetButtonDown("Jump") && is_grounded())
		{
			jump_queued = true;
		}
	}

	private void FixedUpdate()
	{
		if (!canControl) return; // 로프에 당겨지는 동안 이동 불가

		// X축 속도만 제어 (Y축은 AddForce 등 외부 영향 유지)
		Vector2 vel = rb.linearVelocity;
		vel.x = input_x * move_speed;
		rb.linearVelocity = vel;

		if (jump_queued)
		{
			rb.linearVelocityY = jump_force;
			jump_queued = false;
		}
	}

	private bool is_grounded()
	{
		return coll.IsTouchingLayers(ground_layer);
	}
}