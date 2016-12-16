using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class GeneratorManager : MonoBehaviour
{

    public static GeneratorManager Instance;
    public List<GameObject> asteroids = new List<GameObject>();
    private int currentWave = 1;
    private Dictionary<int, string> waves = new Dictionary<int, string>();
    private float nextWaveTime;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Allocation part
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/NormalSmall", "NormalSmall");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/NormalMedium", "NormalMedium");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/NormalBig", "NormalBig");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/Exploding", "Exploding");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/Pulling", "Pulling");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/Pushing", "Pushing");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/Steel", "Steel");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Asteroids/Tracking", "Tracking");
        //
        GameManager.Instance.ScoreMultiplier = 1;
        readText();
        sendWave();
    }

    void Update()
    {
        if (asteroids.Count <= 4 || Time.time >= nextWaveTime)
        {
            if (nextWaveTime - 35 > Time.time)
            {
                currentWave++;
                GameManager.Instance.ScoreMultiplier += 0.2f;
            }
            sendWave();
        }
        GlobalsManager.Instance.comboTimer.value += Time.deltaTime / 25;
        if (GlobalsManager.Instance.comboTimer.value >= 1)
            GameManager.Instance.ScoreMultiplier = 1;
        GlobalsManager.Instance.asteroidCountText.text = (asteroids.Count - 4).ToString();
    }

    void sendWave()
    {
        GlobalsManager.Instance.comboTimer.value = 0;
        nextWaveTime = Time.time + 60;
        string waveData;
        if (currentWave <= 15)
            waveData = waves[currentWave];
        else
            waveData = waves[15];
        GlobalsManager.Instance.waveText.text = "Wave." + currentWave;
        currentWave++;
        string[] datas = waveData.Split(' ');
        for (int i = 0; i < datas.Length; i += 2)
        {
            int generateCount = Int32.Parse(datas[i]);
            GameObject asteroid = ResourceManager.Instance.storedAllocations[datas[i + 1].Trim()];
            for (int h = 0; h < generateCount; h++)
            {
                float asteroidXPos = 0;
                float asteroidYPos = 0;
                int random1 = 0;
                int randomSide = (UnityEngine.Random.Range(0, 2) * 2) - 1;
                if(randomSide == -1)
                {
                    random1 = (UnityEngine.Random.Range(0, 2) * 2) - 1;
                    asteroidXPos = ((GlobalsManager.Instance.screenPos.x) + 2) * random1 ;
                    asteroidYPos = UnityEngine.Random.Range(-GlobalsManager.Instance.screenPos.y, GlobalsManager.Instance.screenPos.y);
                }
                else
                {
                    random1 = (UnityEngine.Random.Range(0, 2) * 2) - 1;
                    asteroidXPos = UnityEngine.Random.Range(-GlobalsManager.Instance.screenPos.x, GlobalsManager.Instance.screenPos.x);
                    asteroidYPos = ((GlobalsManager.Instance.screenPos.y) + 2) * random1;
                }

                GameObject temp = Instantiate(asteroid, new Vector3(asteroidXPos, asteroidYPos, 0), Quaternion.identity);
                temp.GetComponent<Obstacle>().score *= Mathf.Sqrt(currentWave - 1);
                temp.GetComponent<Obstacle>().hp *= Mathf.Pow(1.045f, currentWave - 2);
                if (currentWave == 2)
                    temp.GetComponent<Obstacle>().hp *= 0.5f;
                asteroids.Add(temp);
            }
        }

    }

    void readText()
    {

        TextAsset waveFile = (TextAsset)Resources.Load("TextFiles/Waves", typeof(TextAsset));
        string[] lines = waveFile.text.Split('\n');
        int counter = 1;
        foreach (string line in lines)
        {
            waves.Add(counter, line);
            counter++;
        }
    }
}
