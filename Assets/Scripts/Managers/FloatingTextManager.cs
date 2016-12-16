using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{

    public static FloatingTextManager Instance;

    void Start()
    {
        Instance = this;
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/0", "Number0");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/1", "Number1");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/2", "Number2");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/3", "Number3");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/4", "Number4");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/5", "Number5");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/6", "Number6");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/7", "Number7");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/8", "Number8");
        ResourceManager.Instance.AllocateAndStore("Prefabs/Numbers/9", "Number9");
    }

    public void SpawnText(Vector3 pos, string numberText)
    {
        GameObject temp = new GameObject("FloatingText");
        char[] characters = numberText.ToCharArray();
        float count = 0;
        foreach (char character in characters)
        {
            GameObject number = null;
            number = Instantiate(ResourceManager.Instance.storedAllocations["Number" + character], temp.transform);
            number.transform.localPosition = new Vector2(count, 0);
            count += 0.1f;
        }
        pos += new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f));
        temp.transform.position = pos;
        temp.AddComponent<DesroyerWithNoChild>();
        Rigidbody2D rb = temp.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0.5f);
    }
}
