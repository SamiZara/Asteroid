using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;
    public float rotateSpeed, speed;
    private Rigidbody2D playerRb;
    //public TrailRenderer playerTrail;
    public ParticleSystem jetParticle;
    public bool isImmune;
    private bool isDashing;
    private Vector3 dashDestinaion;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
            playerRb.AddForce(new Vector2(speed * Time.fixedDeltaTime * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), speed * Time.deltaTime * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            jetParticle.Play();
        else if (Input.GetMouseButtonUp(0))
            jetParticle.Stop();
        if (isDashing)
        {
            if(Vector3.Distance(transform.position, dashDestinaion) < 0.5f)
            {
                playerRb.velocity = new Vector2(0, 0);
                transform.position = dashDestinaion;
                GetComponent<CircleCollider2D>().enabled = true;
                GameObject dash = transform.FindChild("Dash").gameObject;
                dash.GetComponent<Dash>().isDashed = false;
                dash.SetActive(false);
                isDashing = false;
            }
        }
    }

    public void rotate()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        float degree = MathHelper.degreeBetween2Points(transform.position, v3);
        if (degree < 0)
            degree += 360;
        float myRotation = transform.rotation.eulerAngles.z;
        if (Math.Abs(myRotation - degree) > 10)
        {
            if (myRotation > degree)
            {
                if (Math.Abs(myRotation - degree) < 180)
                    playerRb.angularVelocity = -rotateSpeed;
                else
                    playerRb.angularVelocity = rotateSpeed;
            }
            else
            {
                if (Math.Abs(myRotation - degree) < 180)
                    playerRb.angularVelocity = +rotateSpeed;
                else
                    playerRb.angularVelocity = -rotateSpeed;
            }
        }
        else
        {
            stopRotate();
        }

    }

    public void stopRotate()
    {
        playerRb.angularVelocity = 0;
    }

    void OnBecameInvisible()
    {
        Vector3 playerPos = transform.position;
        if(transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(playerPos.x, -playerPos.y, playerPos.z);
            playerPos = transform.position;
            //StartCoroutine("ResetTrailRenderer",playerTrail);
        }
        else if(transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(playerPos.x, -playerPos.y, playerPos.z);
            playerPos = transform.position;
            //StartCoroutine("ResetTrailRenderer", playerTrail);
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-playerPos.x, playerPos.y, playerPos.z);
            playerPos = transform.position;
            //StartCoroutine("ResetTrailRenderer", playerTrail);
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-playerPos.x, playerPos.y, playerPos.z);
            playerPos = transform.position;
            //StartCoroutine("ResetTrailRenderer", playerTrail);
        }
    }

    /*IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        tr.time = 0;
        yield return null;
        tr.time = 0.2f;
    }*/

    public void Destroy()
    {
        if (!isImmune)
            Debug.Log("Player hit");
        else
            Debug.Log("Player hit but immune");
    }

    public void Dash()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        dashDestinaion = v3;
        float degree = MathHelper.degreeBetween2Points(transform.position, v3);
        if (degree < 0)
            degree += 360;
        transform.eulerAngles = new Vector3(0, 0, degree);
        playerRb.velocity = new Vector3(0, 0, 0);//Stopping player first
        playerRb.velocity = new Vector2(Constants.DASH_SPEED * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Constants.DASH_SPEED * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        isDashing = true;
    }
}

