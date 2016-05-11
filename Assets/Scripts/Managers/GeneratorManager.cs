using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

    public static GeneratorManager Instance;
    public List<GameObject> asteroids = new List<GameObject>();
	void Awake () {
        Instance = this;
	}

	void Start()
	{
		ResourceManager.Instance.AllocateAndStore ("Prefabs/Asteroids/Tracking", "NormalAsteroid");
        GameObject asteroid = ResourceManager.Instance.storedAllocations["NormalAsteroid"];
        for (int i = 0; i < 3; i++)
        {
            float asteroidXPos = asteroid.GetComponent<SpriteRenderer>().sprite.rect.width / 100 + GlobalsManager.Instance.screenPos.x;
            float asteroidYPos = asteroid.GetComponent<SpriteRenderer>().sprite.rect.height / 100 + GlobalsManager.Instance.screenPos.y;
            int random1 = (Random.Range(0, 2) * 2) - 1;
            int random2 = (Random.Range(0, 2) * 2) - 1;
            if (random1 == 1)
                asteroidXPos = Random.Range(0, asteroidXPos);
            else
                asteroidYPos = Random.Range(0, asteroidYPos);
            GameObject temp = (GameObject)Instantiate(asteroid, new Vector3(random1 * asteroidXPos, random2 * asteroidYPos, 0), Quaternion.identity);
            asteroids.Add(temp);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
