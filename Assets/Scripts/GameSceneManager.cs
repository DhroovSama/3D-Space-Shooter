using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	private float ENEMY_INTERVAL_MAX = 2.0f;
	private float ENEMY_INTERVAL_MIN = 0.2f;
	private float TIME_TO_MINIMUM_INTERVAL = 30.0f;	

	public Camera mainCamera;
	public Text scoreText;
	public Text gameOverText;
	public ShipController ship;
	public GameObject enemyPrefab;

	private Vector3 leftBound;
	private Vector3 rightBound;

	private int score;
	private float gameTimer;
	private float enemyTimer;
	private bool gameOver;

	public void Start ()
	{
		Time.timeScale = 1;
		gameOverText.enabled = false;

		enemyTimer = ENEMY_INTERVAL_MAX;

		leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.localPosition.z));
		rightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.localPosition.z));

		ship.OnHitEnemy += OnGameOver;
	}

	public void Update ()
	{
		if (gameOver)
		{
			if (Input.GetKeyDown("r"))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			scoreText.enabled = false;
			gameOverText.enabled = true;

			gameOverText.text = "Game over! Total score: " + score + "\nPress R to restart";

			return;
		}

		gameTimer += Time.deltaTime;
		enemyTimer -= Time.deltaTime;
		if (enemyTimer <= 0)
		{
		    float intervalPercentage = Mathf.Min(gameTimer / TIME_TO_MINIMUM_INTERVAL, 1);
		    enemyTimer = ENEMY_INTERVAL_MAX - (ENEMY_INTERVAL_MAX - ENEMY_INTERVAL_MIN) * intervalPercentage;

		    GameObject enemy = GameObject.Instantiate<GameObject>(enemyPrefab);
		    enemy.transform.SetParent(this.transform);
		    enemy.transform.position = new Vector3
		    (
		    	Random.Range(leftBound.x, rightBound.x),
		    	leftBound.y + 2,
		    	0
		    );

		    enemy.GetComponent<EnemyController>().OnKill += OnKillEnemy;
		}
	}

	public void OnKillEnemy ()
	{
		score += 100;
		scoreText.text = "Score: " + score;
	}

	public void OnGameOver ()
	{
		gameOver = true;

		Time.timeScale = 0;
	}
}