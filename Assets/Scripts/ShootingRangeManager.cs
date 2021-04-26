using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRangeManager : MonoBehaviour
{
    private int numOftargets = 30;
    private bool gameStarting = false;
    private bool gamePlaying = false;
    private Vector3 spawnPos;
    private Quaternion objectRotation = new Quaternion(0, 0, 0, 0);
    private GameObject spawnedTarget;
    private float x, y = -0.3f, z;

    public GameObject targetToSpawn;
    public Button startButton;
    public static bool shotRegistered = false;
    public Text targetsRemainingText;

    // Start is called before the first frame update
    void Start()
    {
        // set number of targets if not set
        if (numOftargets == 0)
            numOftargets = PlayerPrefs.GetInt("TargetNum");
        else
            PlayerPrefs.SetInt("TargetNum", numOftargets);

        targetsRemainingText.text = numOftargets.ToString();
    }

    // FixedUpdate is called once per fixed update frame
    void FixedUpdate()
    {
        // Start game when boolean value changes
        if (gameStarting)
        {
            // set preferred number of targets
            PlayerPrefs.SetInt("TargetNum", numOftargets);
            targetsRemainingText.text = numOftargets.ToString();
            RandomSpawn();
            // spawn first target
            spawnedTarget = Instantiate(targetToSpawn, spawnPos, objectRotation);
            gameStarting = false;
            gamePlaying = true;
        }
        else if(gamePlaying)
        {
            // wait for target to be hit
            if (shotRegistered && numOftargets >= 0)
            {
                Destroy(spawnedTarget.gameObject);
                numOftargets--;
                // Spawn a new target at a random location
                RandomSpawn();
                spawnedTarget = Instantiate(targetToSpawn, spawnPos, objectRotation);

                targetsRemainingText.text = numOftargets.ToString();
                shotRegistered = false; // exit if
            }
            else if (numOftargets <= 0)
            {
                GameOver();
            }
        }
    }

    void RandomSpawn()
    {
        // Spawn a target at a random location between
        // co-oords x66,y-0.3,z100 & x-6,y-0.3,z100
        x = Random.Range(66, 100);
        // y is constant -0.3
        z = Random.Range(-6, -34);
        spawnPos = new Vector3(x, y, z);
    }

    // Method used by start button
    public void StartGame()
    {
        gameStarting = true;
    }

    void GameOver()
    {
        startButton.interactable = true;

        Destroy(spawnedTarget.gameObject);
        Debug.Log("All enemies Killed, Well done");

        gamePlaying = false;
    }

    public void SetTargets(int t)
    {
        // Sets the number of targets
        numOftargets = t;
        targetsRemainingText.text = numOftargets.ToString();
        PlayerPrefs.SetInt("TargetNum", numOftargets);
    }

    public void Method()
    {
        throw new System.NotImplementedException();
    }
}
