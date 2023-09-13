using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public delegate void OnHitEnemyAction();
    public OnHitEnemyAction OnHitEnemy;

    public float speed = 15f;
    public Camera mainCamera;
    public GameObject bulletPrefab;

    Vector3 rightBound;
    Vector3 leftBound;

    void Start()
    {
        leftBound = mainCamera.ViewportToWorldPoint(new Vector3 (0, 1, -mainCamera.transform.localPosition.z));
        rightBound = mainCamera.ViewportToWorldPoint(new Vector3 (1, 0, -mainCamera.transform.localPosition.z));        
    }

    void Update()
    {
        processInput();
        KeepInBounds();
    }

    private void KeepInBounds()
    {
        if(this.transform.position.x < leftBound.x) {this.transform.position = new Vector3(leftBound.x, this.transform.position.y, this.transform.position.z);}
        if(this.transform.position.x > rightBound.x) {this.transform.position = new Vector3(rightBound.x, this.transform.position.y, this.transform.position.z);}
        if(this.transform.position.y > leftBound.y) {this.transform.position = new Vector3(this.transform.position.x, leftBound.y, this.transform.position.z);}
        if(this.transform.position.y < rightBound.y) {this.transform.position = new Vector3(this.transform.position.x, rightBound.y, this.transform.position.z);}
    }

    private void processInput()
    {
        if(Input.GetKey("down") || Input.GetKey("s")) {this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime, this.transform.position.z);}
        if(Input.GetKey("up") || Input.GetKey("w")) {this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime, this.transform.position.z);}
        if(Input.GetKey("left") || Input.GetKey("a")) {this.transform.position = new Vector3(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);}
        if(Input.GetKey("right") || Input.GetKey("d")) {this.transform.position = new Vector3(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);}

        if(Input.GetKeyDown("space") || Input.GetKeyDown("k"))
        {
            GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab);
            bullet.transform.SetParent(this.transform.parent);
            bullet.transform.position = this.transform.position; 
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<EnemyController>() != null) {if(OnHitEnemy != null) {OnHitEnemy();}}

        GameObject.Destroy(this.gameObject);
    }
}
