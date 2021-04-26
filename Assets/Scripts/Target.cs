using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            AudioManager.instance.Play("BulletHit");
            ShootingRangeManager.shotRegistered = true;
            Destroy(gameObject);

        }
    }

    public void Method()
    {
        throw new System.NotImplementedException();
    }
}
