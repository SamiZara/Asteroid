using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingDamageText : MonoBehaviour {

    private SpriteRenderer sprite;

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y + 50 * Time.deltaTime, transform.position.z);
        sprite.color = new Color(1, 1, 1, sprite.color.a - Time.deltaTime);
        if (sprite.color.a <= 0)
            Destroy(gameObject);
    }
}
