using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] private int coins = 0;
	
	public void add_coins(int amount)
	{
		coins += amount;
	}
}
