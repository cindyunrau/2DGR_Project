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

        if (parent.tag == "Player")
        {
            player = parent.gameObject.GetComponent<Player>();
        }

        if (parent.tag == "Shambler")
        {
            enemy = parent.gameObject.GetComponent<Shambler>();
        }

        if (parent.tag == "Ghost")
        {
            enemy = parent.gameObject.GetComponent<Ghost>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProtectWard")
        {
            if (parent.tag == "Shambler" || parent.tag == "Ghost")
            {
                // Damage enemies
                enemy.takeDamage(1);
            }

            if (parent.tag == "Player")
            {
                // Heal player
                gm.HealPlayer(1);
            }

        }

    }
}
