﻿using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

    private GameObject player = null;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            StartCoroutine(Push());
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
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(500 * Time.fixedDeltaTime * Mathf.Cos(degree * Mathf.Deg2Rad), 500 * Time.fixedDeltaTime * Mathf.Sin(degree * Mathf.Deg2Rad)));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
