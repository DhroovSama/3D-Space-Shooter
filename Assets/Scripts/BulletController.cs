using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	private float SPEED = 20;

	public void Update ()
	{
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + SPEED * Time.deltaTime, this.transform.position.z);

		if (this.transform.position.y > 10)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}