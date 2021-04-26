using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void Fire()
    {
        // Spawn a bullet from the barrels location + rotation + direction
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.right;

        audioSource.PlayOneShot(audioClip);
        // Destroy bullet 3 seconds after it's shot
        Destroy(spawnedBullet, 3);
    }
}
