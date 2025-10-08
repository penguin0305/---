using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public enum ItemType
	{
		Coin,
		Heal
	}

	[SerializeField] private ItemType type;
	[SerializeField] private int amount = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player"))
			return;

		switch (type)
		{
			case ItemType.Coin:
				PlayerInventory inv = other.GetComponent<PlayerInventory>();
				if (inv)
					inv.add_coins(amount);
				break;

			case ItemType.Heal:
				PlayerHealth hp = other.GetComponent<PlayerHealth>();
				if (hp)
					hp.heal(amount);
				break;
		}
		
		Destroy(gameObject);
	}
}
