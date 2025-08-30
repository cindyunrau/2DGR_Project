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

    [Header("References")]
    public Transform attackPoint;
    public RaycastHit2D rayHit;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsWall; 
    public GameManager gameManager;
    private Animator anim;
    [SerializeField] private GameObject bulletTrail;

    private void Awake()
    {
        readyToShoot = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        //gameManager.AddAmmo(1);
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
            Shoot();
        }

    }

    private void Shoot()
    {
        readyToShoot = false;
        anim.SetTrigger("Fire");

        Debug.Log("Shooting");
        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = attackPoint.right + new Vector3(x, y, 0);

        // Raycast
        LayerMask hittable = whatIsEnemy | whatIsWall;
        RaycastHit2D rayHit = Physics2D.Raycast(attackPoint.position, direction, range, hittable);
        if (rayHit)
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Shambler") || rayHit.collider.CompareTag("Ghost"))
            {
                rayHit.collider.GetComponent<Enemy>().takeDamage(damage);
            }
        }

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

}
