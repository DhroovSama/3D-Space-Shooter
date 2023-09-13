using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public delegate void OnKillAction();

	public OnKillAction OnKill;

	private float SPEED = -5;

	public void Update ()
	{
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + SPEED * Time.deltaTime, this.transform.position.z);

		if (this.transform.position.y < -10)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	public void OnTriggerEnter (Collider collider)
	{
		if (collider.GetComponent<BulletController>() != null)
		{
			if (OnKill != null)
			{
				OnKill();
			}

			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(collider.gameObject);
		}
	}
}