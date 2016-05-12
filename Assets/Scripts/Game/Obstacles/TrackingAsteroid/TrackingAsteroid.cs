using UnityEngine;
using System.Collections;

public class TrackingAsteroid : Obstacle
{

    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
        degreeToMiddle = Random.Range(degreeToMiddle - 20, degreeToMiddle + 20);
        rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Time.fixedDeltaTime * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Time.deltaTime * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
        player = GlobalsManager.Instance.player;
    }

    void FixedUpdate()
    {
        float degree = MathHelper.degreeBetween2Points(player.transform.position, transform.position);
        rb.AddForce(new Vector2(-1000 * Time.fixedDeltaTime * Mathf.Cos(degree * Mathf.Deg2Rad), -1000 * Time.fixedDeltaTime * Mathf.Sin(degree * Mathf.Deg2Rad)));
    }
}
