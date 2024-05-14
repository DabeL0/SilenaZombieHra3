using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private bool spawn = true;
    [SerializeField]
    int bodyMultiplier = 0;

    [SerializeField]
    private List<Transform> brains;

    [Header("Spawn values")]
    [SerializeField]
    private Transform spawnPointsParent;
    List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]
    private List<EnemyMovement> enemyPrefabs;

    [SerializeField]
    private List<SpawnData> enemyColors;

    [SerializeField]
    private GameObject endGameScreen;

    [SerializeField]
    private TMPro.TMP_Text timeText;

    [Range(0.25f,5)]
    [SerializeField]
    private float spawnInterval;

    private float timeAlive = 0;
    
    
    [SerializeField]
    private TMP_Text bodyText;
    public int bodiky;
    [SerializeField]
    Animator ani;
    [SerializeField]
    string levelName;

    private void Awake()
    {
        Instance = this;
       
        bodiky = PlayerPrefs.GetInt("bodiky");
        NactiBody();
        for(int i = 0; i < spawnPointsParent.childCount; i++)
        {
            spawnPoints.Add(spawnPointsParent.GetChild(i));
        }
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
    }

    public Transform GetClosestBrain(Vector3 position)
    {
        int indexOfClosestBrain = 0;
        float distance = 10000;
        for (int i = 0; i < brains.Count; i++) {
            float dist = Vector3.Distance(position, brains[i].position);
            if(dist < distance) { 
                indexOfClosestBrain = i; 
                distance = dist; 
            }
        }
        return brains[indexOfClosestBrain];
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
      
        Debug.Log(ani);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            if(spawn)
                SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(GetRandomPrefab(), GetRandomSpawnPoint(), Quaternion.identity);
        var color = GetColorForEnemy();
        var stats = enemy.GetComponent<EnemyStats>();
        stats.SetType(color.enemyColor.color, color.damage);
    }

    private EnemyMovement GetRandomPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)];
    }

    private SpawnData GetColorForEnemy()
    {
        int rnd = UnityEngine.Random.Range(0, 100);
        int lastSum = 0;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            lastSum += enemyColors[i].probability;
            if (rnd <= lastSum) return enemyColors[i];
        }

        return enemyColors.Last();
    }

    private Vector3 GetRandomSpawnPoint()
    {
        var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        return point.position;
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("bodiky", bodiky);
        NactiBody();
        Time.timeScale = 0;
        endGameScreen.SetActive(true);
        float bestTime = PlayerPrefs.GetFloat(levelName, 0f);
        if (timeAlive > bestTime)
        {
            PlayerPrefs.SetFloat(levelName, timeAlive);
            timeText.text = "New record: " + (int)timeAlive + " sec!";
        }
        else
        {
            timeText.text = "You survived: " + (int)timeAlive + " sec";
        }


    }

    public void Body()
    {
        bodiky += 1 * bodyMultiplier;
        
        NactiBody();
    }

    public void NactiBody()
    {
        bodyText.text = "Points: " + bodiky;
       ani.SetTrigger("Bop");
       
       
    }

    public void ReturnToMenu()
    {
       // SceneManager.LoadScene("Menu");
        SceneManager.LoadScene("Menu");
    }
}

[Serializable]
public class SpawnData
{
    public int probability; // normal %
    public Material enemyColor;
    public int damage;
}