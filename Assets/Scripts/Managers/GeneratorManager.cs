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
        readText();
        sendWave();
    }

    void Update()
    {
        if (asteroids.Count <= 4 || Time.time >= nextWaveTime)
        {
            sendWave();
        }
    }

    void sendWave()
    {
        nextWaveTime = Time.time + 60;
        //Debug.Log("1");
        string waveData;
        if (currentWave <= 15)
            waveData = waves[currentWave];
        else
            waveData = waves[15];
        currentWave++;
        //Debug.Log("2");
        string[] datas = waveData.Split(' ');
        //Debug.Log("3");
        for (int i = 0; i < datas.Length; i += 2)
        {
            //Debug.Log("4");
            int generateCount = Int32.Parse(datas[i]);

            //Debug.Log("5");
            GameObject asteroid = ResourceManager.Instance.storedAllocations[datas[i + 1].Trim()];
            //Debug.Log("6");
            for (int h = 0; h < generateCount; h++)
            {
                float asteroidXPos = asteroid.GetComponent<SpriteRenderer>().sprite.rect.width / 100 + GlobalsManager.Instance.screenPos.x;
                float asteroidYPos = asteroid.GetComponent<SpriteRenderer>().sprite.rect.height / 100 + GlobalsManager.Instance.screenPos.y;
                int random1 = (UnityEngine.Random.Range(0, 2) * 2) - 1;
                int random2 = (UnityEngine.Random.Range(0, 2) * 2) - 1;
                if (random1 == 1)
                    asteroidXPos = UnityEngine.Random.Range(0, asteroidXPos);
                else
                    asteroidYPos = UnityEngine.Random.Range(0, asteroidYPos);
                GameObject temp = (GameObject)Instantiate(asteroid, new Vector3(random1 * asteroidXPos, random2 * asteroidYPos, 0), Quaternion.identity);
                temp.GetComponent<Obstacle>().score *= Mathf.Sqrt(currentWave - 1);
                temp.GetComponent<Obstacle>().hp *= Mathf.Pow(1.045f, currentWave - 1);
                temp.GetComponent<Obstacle>().money *= Mathf.Pow(1.1f, currentWave - 1);
                asteroids.Add(temp);
            }
        }

    }

    void readText()
    {

        TextAsset waveFile = (TextAsset)Resources.Load("TextFiles/Waves", typeof(TextAsset));
        string[] lines = waveFile.text.Split('\n');
        //using (StreamReader sr = new StreamReader(Application.persistentDataPath+"/TextFiles/Waves.txt"))
        //{
        int counter = 1;
        foreach (string line in lines)
        {
            // Read the stream to a string, and write the string to the console.
            waves.Add(counter, line);
            counter++;
        }
        //}
    }
}
