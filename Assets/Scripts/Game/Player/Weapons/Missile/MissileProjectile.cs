using UnityEngine;
using System.Collections;

public class MissileProjectile : Projectile
{
    public float rotateSpeed;
    public GameObject target;
    public bool autoLock;
    private bool isReadyToDestroy = false;
    public new void Start()
    {
        tier = Missile.missileTier;
        startTime = Time.time;
        if (autoLock)
            target = MissileLocker.lockedAsteroid;
        rb = GetComponent<Rigidbody2D>();
        if (tier % 2 == 0)
        {
            damage *= 1.5f;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            MissileLocker.ManuelLockOnAsteroid();
            target = MissileLocker.lockedAsteroid;
            if (target == null)
            {
                Destroy();
                return;
            }
        }
        if (!isReadyToDestroy && target != null)
        {
            float degree = MathHelper.degreeBetween2Points(transform.position, target.transform.position);
            if (degree < 0)
                degree += 360;
            float myRotation = transform.rotation.eulerAngles.z;
            if (myRotation > degree)
            {
                if (Mathf.Abs(myRotation - degree) < 180)
                    rb.angularVelocity = -rotateSpeed;
                else
                {
                    rb.angularVelocity = rotateSpeed;
                }
            }
            else
            {
                if (Mathf.Abs(myRotation - degree) < 180)
                    rb.angularVelocity = rotateSpeed;
                else
                {
                    rb.angularVelocity = -rotateSpeed;
                }
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Missile.PlayProjectileExplosionSound();
    }
}
