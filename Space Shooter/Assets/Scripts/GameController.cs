using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private int score;
    private bool gameOver;
    private bool restart;

    // Use this for initialization
    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (var i = 0; i < hazardCount; i++)
            {
                bool flag = (Random.value > 0.5f);
                Debug.Log(Random.value);
                Debug.Log(flag);
                if (flag)
                {
                    //spawnValues.z = -spawnValues.z;
                }

                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                GameObject clone = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;

                if (flag)
                {
                    //ReverseDiretion(clone);
                }

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    private void ReverseDiretion(GameObject clone)
    {
        //clone.transform.Rotate(new Vector3(0, 180, 0));
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        Mover mover = clone.GetComponent<Mover>();

        rb.rotation = Quaternion.Euler(0.0f, rb.velocity.x * -10, 0.0f);
        mover.speed = -mover.speed;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
