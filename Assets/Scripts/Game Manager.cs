using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.SearchService;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverScreen;

    // Mangaged Resources
    private int health;
    private int maxHealth = 3;

    private int time;
    private Dictionary<string, int> inventory =
    new Dictionary<string, int>();

    // Debug Objects
    public TMP_Text debugText;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        print("NewGame");
        StopAllCoroutines();

        SetHealth(maxHealth);
        inventory.Add("fuel", 0);
        inventory.Add("ammo", 0);

        updateDebug();

        gameOverScreen.SetActive(false);
        player.gameObject.SetActive(true);

    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        print("GameOver");
        player.setDead();
        player.gameObject.SetActive(false);
        player.stopAllMovement();

        gameOverScreen.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        print("PlayAgain");
        bool playAgain = false;

        // play again menu

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }

            yield return null;
        }

        Reload();
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
    private void SetHealth(int health)
    {
        this.health = health;
    }

    public void DamagePlayer(int damage)
    {
        print("DamagePlayer");
        SetHealth(this.health - damage);
        debugText.text = "Health : " + getHealth();

        if (this.health <= 0)
        {
            KillPlayer();
            //Invoke(nameof(GameOver), 1f);
        }
    }

    public void KillPlayer()
    {
        player.setDead();
        player.gameObject.SetActive(false);
        player.stopAllMovement();
        Invoke(nameof(GameOver), 1f);
    }

    public void AddFuel(int amount)
    {
        inventory["fuel"] += amount;
        updateDebug();

    }

    // Returns true if successful
    public bool UseFuel()
    {
        if (inventory["fuel"] > 0)
        {
            inventory["fuel"] -= 1;
            updateDebug();
            return true;
        }
        return false;
    }

    public void AddAmmo(int amount)
    {
        inventory["ammo"] += amount;
        updateDebug();
    }

    // Returns true if successful
    public bool UseAmmo()
    {
        if (inventory["ammo"] > 0)
        {
            inventory["ammo"] -= 1;
            updateDebug();
            return true;
        }
        return false;
    }

    public void updateDebug()
    {
        debugText.text = getHealth() + "\n" + inventory["fuel"] + "\n" + inventory["ammo"];
    }

}
