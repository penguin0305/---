using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Transform muzzle;
	[SerializeField] private float bulletSpeed = 20f;
	[SerializeField] private float fireCooldown = 0.15f;

	private float timer;

	private void Update()
	{
		timer -= Time.deltaTime;

		// E 키 입력 시 발사
		if (Input.GetKey(KeyCode.E) && timer <= 0f)
		{
			Fire();
			timer = fireCooldown;
		}
	}

	private void Fire()
	{
		if (!bulletPrefab || !muzzle)
			return;

		// 총알 생성 및 발사
		var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
		var rb = bullet.GetComponent<Rigidbody2D>();
		rb.linearVelocity = muzzle.right * bulletSpeed;
	}
}
