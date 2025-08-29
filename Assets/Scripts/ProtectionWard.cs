using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionWard : MonoBehaviour
{
    public GameObject parent;
    public SpriteRenderer sprite;
    private GameManager gm;
    private Player player;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();

        if (parent.tag == "Player")
        {
            player = parent.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProtectWard")
        {
            if (parent.tag == "Shambler" || parent.tag == "Ghost")
            {
                // Damage enemies
            }

            if (parent.tag == "Player")
            {
                // Heal player
                gm.HealPlayer(1);
            }

        }

    }
}
