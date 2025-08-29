using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionWard : MonoBehaviour
{
    public GameObject parent;
    public SpriteRenderer sprite;
    private GameManager gm;
    private Player player;
    private Enemy enemy;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Heal player
            gm.HealPlayer(1);
        }

        else if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            // Damage enemies
            collision.gameObject.GetComponent<Enemy>().takeDamage(1);
        }

    }
}
