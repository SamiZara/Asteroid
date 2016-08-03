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
        if(asteroids.Count == 0 || Time.time >= nextWaveTime)
        {
            sendWave();
        }
    }

    void sendWave()
    {
        
        string waveData = waves[currentWave++];
        if (currentWave > 15)
            currentWave = 15;
        string[] datas = waveData.Split(' ');
        for(int i = 0; i < datas.Length; i+=2)
        {
            int generateCount = Int32.Parse(datas[i]);
            GameObject asteroid = ResourceManager.Instance.storedAllocations[datas[i + 1]];
            for(int h = 0; h < generateCount; h++)
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
                asteroids.Add(temp);
            }
        }
        nextWaveTime = Time.time + 60;
    }

    void readText()
    {
        using (StreamReader sr = new StreamReader("Assets/TextFiles/Waves.txt"))
        {
            // Read the stream to a string, and write the string to the console.
            string line = "";
            int counter = 1;
            while((line = sr.ReadLine()) != null){
                waves.Add(counter, line);
                counter++;
            }
        }
    }
}
