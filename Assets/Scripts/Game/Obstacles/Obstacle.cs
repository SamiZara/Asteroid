using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

    public float hp;
    public GameObject explosionParticle,debriParticle;
    public Rigidbody2D rb;
    public bool isInTimeWarpBubble, isScatterObject,isSpecialAsteroid;
    public float score,money;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
        degreeToMiddle = Random.Range(degreeToMiddle - Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE, degreeToMiddle + Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE);
        if(!isScatterObject)
            rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
        StartCoroutine(SpeedStabilizer());
        StartCoroutine(DirectionChecker());
    }

    void OnBecameInvisible()
    {
        Vector3 obstaclePos = transform.position;
        if (transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(obstaclePos.x, -obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        else if (transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(obstaclePos.x, -obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-obstaclePos.x, obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-obstaclePos.x, obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy();
        }
        FloatingTextManager.Instance.SpawnText(transform.position, ((int)(damage * 10)).ToString());
    }

    public void createDebris(Vector3 pos,float degree)
    {
        if(debriParticle != null)
            Instantiate(debriParticle,pos,Quaternion.Euler(0,0,degree - 90));
    }

    public void Destroy()
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;
        GeneratorManager.Instance.asteroids.Remove(gameObject);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            GlobalsManager.Instance.asteroidExplosionSound.Play();
        GameManager.Instance.Score += score * GameManager.Instance.ScoreMultiplier;
        GameManager.Instance.money += money;
        if (isSpecialAsteroid)
            GameManager.Instance.specialAsteroidDestroyCount += 1;
        else
            GameManager.Instance.normalAsteroidDestroyCount += 1;
        Destroy(gameObject);
    }

    IEnumerator SpeedStabilizer()
    {
        while (true)
        {
            if (!isInTimeWarpBubble && rb.velocity.magnitude != GlobalsManager.Instance.asteroidSpeed)
            {
                if (rb.velocity.magnitude < GlobalsManager.Instance.asteroidSpeed)
                {
                    rb.velocity += (GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * rb.velocity) / 10;
                    if (rb.velocity.magnitude > GlobalsManager.Instance.asteroidSpeed)//If exceeds speed limit
                        rb.velocity *= GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude;
                }
                else
                {
                    rb.velocity -= (GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * rb.velocity) / 10;
                    if (rb.velocity.magnitude < GlobalsManager.Instance.asteroidSpeed)//If exceeds speed limit
                        rb.velocity *= GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude;
                }
            }
            else
            {
                if (rb.velocity.magnitude < GlobalsManager.Instance.asteroidSpeed)
                {
                    rb.velocity += (GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * rb.velocity * Constants.SLOW_BUBBLE_FACTOR) / 10;
                    if (rb.velocity.magnitude > GlobalsManager.Instance.asteroidSpeed * Constants.SLOW_BUBBLE_FACTOR)//If exceeds speed limit
                        rb.velocity *= GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * Constants.SLOW_BUBBLE_FACTOR;
                }
                else
                {
                    rb.velocity -= (GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * rb.velocity * Constants.SLOW_BUBBLE_FACTOR) / 10;
                    if (rb.velocity.magnitude < GlobalsManager.Instance.asteroidSpeed * Constants.SLOW_BUBBLE_FACTOR)//If exceeds speed limit
                        rb.velocity *= GlobalsManager.Instance.asteroidSpeed / rb.velocity.magnitude * Constants.SLOW_BUBBLE_FACTOR;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator DirectionChecker()
    {
        while (true)
        {
            if (transform.position.y > GlobalsManager.Instance.screenPos.y + 1 || transform.position.y < -GlobalsManager.Instance.screenPos.y - 1 ||
                transform.position.x > GlobalsManager.Instance.screenPos.x + 1 || transform.position.x < -GlobalsManager.Instance.screenPos.x - 1)
            {
                float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
                degreeToMiddle = Random.Range(degreeToMiddle - Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE, degreeToMiddle + Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE);
                rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
            }
            /*if (Vector2.Distance(transform.position, Vector2.zero) > 11)
            {
                float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
                degreeToMiddle = Random.Range(degreeToMiddle - Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE, degreeToMiddle + Constants.ASTEROID_DEGREE_DEVIATION_TO_MIDDLE);
                rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
            }*/
            yield return new WaitForSeconds(1);
        }
    }
}
