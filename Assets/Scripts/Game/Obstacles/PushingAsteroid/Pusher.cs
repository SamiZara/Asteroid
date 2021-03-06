﻿using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

    private GameObject player = null;

    void Start()
    {
        StartCoroutine(Push());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject; 
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
        }
    }

    IEnumerator Push()
    {
        while (true)
        {
            if (player != null)
            {
                float degree = MathHelper.degreeBetween2Points(transform.position, player.transform.position);
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(250 * Mathf.Cos(degree * Mathf.Deg2Rad), 250 * Mathf.Sin(degree * Mathf.Deg2Rad)));
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
