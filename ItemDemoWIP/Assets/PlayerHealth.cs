using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int max_hp = 5;
	[SerializeField] private int hp = 3;

	public void heal(int heal_amount)
	{
		hp = Mathf.Min(hp + heal_amount, max_hp);
	}

	public void damamge(int damage)
	{
		hp -= damage;
	}
}
