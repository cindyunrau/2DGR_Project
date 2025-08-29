using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int damage;
    public float timeBetweentShooting, spread, range, timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;
    private int bulletsShot;

    private bool shooting, readyToShoot;
    //public TrailRenderer bulletTrail;

    [Header("References")]
    public Transform attackPoint;
    public RaycastHit2D rayHit;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsWall; 
    public GameManager gameManager;
    [SerializeField] private GameObject bulletTrail;

    private void Awake()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        //if (readyToShoot && shooting && gameManager.GetAmmo() > 0)
        //{
        //    bulletsShot = bulletsPerTap;
        //    gameManager.AddAmmo(1);
        //    Shoot();
        //}
    }

    private void MyInput()
    {
        gameManager.AddAmmo(1);
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && shooting && gameManager.GetAmmo() > 0)
        {
            bulletsShot = bulletsPerTap;
            //gameManager.AddAmmo(1);
            Shoot();
        }

    }

    private void Shoot()
    {
        readyToShoot = false;

        Debug.Log("Shooting");
        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = attackPoint.right + new Vector3(x, y, 0);

        // Raycast
        //RaycastHit2D rayHit = Physics2D.Raycast(attackPoint.position, direction, range, whatIsEnemy);
        LayerMask hittable = whatIsEnemy | whatIsWall;
        RaycastHit2D rayHit = Physics2D.Raycast(attackPoint.position, direction, range, hittable);
        if (rayHit)
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().takeDamage(damage);
            }
        }

        //TrailRenderer trail = Instantiate(bulletTrail, attackPoint.position, Quaternion.identity);
        //StartCoroutine(SpawnTrail(trail, rayHit));

        var trail = Instantiate(bulletTrail, attackPoint.position, transform.rotation);

        var trailScript = trail.GetComponent<BulletTrail>();
        if (rayHit.collider != null)
        {
            trailScript.SetTargetPosition(rayHit.point);
            
        }
        else
        {
            var endPosition = attackPoint.position + direction * range;
            trailScript.SetTargetPosition(endPosition);
        }

        gameManager.UseAmmo(1);
        bulletsShot--;
        Invoke("ResetShot", timeBetweentShooting);

        if (bulletsShot > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    //private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit2D hit)
    //{
    //    float time = 0;
    //    Vector2 startPosition = trail.transform.position;

    //    while(time < 1)
    //    {
    //        trail.transform.position = Vector2.Lerp(startPosition, hit.point, time);
    //        time += Time.deltaTime / trail.time;

    //        yield return null;
    //    }

    //    trail.transform.position = hit.point;
    //    Destroy(trail.gameObject, trail.time);
    //}

}
