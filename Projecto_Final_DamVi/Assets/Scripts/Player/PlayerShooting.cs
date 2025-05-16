using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource audioSource;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.25f;

    private float cooldownTimer;
    private bool isTripleShot;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            if (isTripleShot)
                ShootTriple();
            else
                ShootSingle();

            cooldownTimer = shootCooldown;
        }
    }

    public void ActivateTripleShot(float duration)
    {
        isTripleShot = true;
        Invoke(nameof(DeactivateTripleShot), duration);
    }

    void DeactivateTripleShot()
    {
        isTripleShot = false;
    }

    void ShootSingle()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity, Camera.main.transform);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);
    }

    void ShootTriple()
    {
        Vector3[] offsets = {
        Vector3.zero,
        new Vector3(-0.5f, 0.5f, 0),
        new Vector3(0.5f, 0.5f, 0)
    };

        foreach (Vector3 offset in offsets)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position + offset, Quaternion.identity, Camera.main.transform);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
        }

        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);
    }

}


