using UnityEngine;
using System.Collections;

public class Asteroid : Obstacle
{

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
        degreeToMiddle = Random.Range(degreeToMiddle - 20, degreeToMiddle + 20);
        rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Time.fixedDeltaTime * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Time.deltaTime * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
    }
}
