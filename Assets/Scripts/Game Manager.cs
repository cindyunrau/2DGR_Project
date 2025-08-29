using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverScreen;
    public FadeInUI escapeText;
    public FadeInUI pauseText;
    private float textDuration = 2f;

    // Mangaged Resources
    private int health;
    private int maxHealth = 3;

    private int time;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Debug Objects
    public TMP_Text debugText;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        updateDebug();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeText.gameObject.SetActive(true);
            StartCoroutine(FadeOut(escapeText,2f));
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseText.gameObject.SetActive(true);
            StartCoroutine(Pause());
        }
        else if (!Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            StartCoroutine(FadeOut(pauseText,0f));
            
        }
        
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }
    IEnumerator FadeOut(FadeInUI text,float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(text.FadeOut());
    }

    private void NewGame()
    {
        print("NewGame");
        StopAllCoroutines();

        SetHealth(maxHealth);
        inventory.Add("fuel", 0);
        inventory.Add("ammo", 0);


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
        //player.gameObject.SetActive(false);
        player.stopAllMovement();

        gameOverScreen.SetActive(true);

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

        if (this.health <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        player.setDead();
        //player.gameObject.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.stopAllMovement();
        Invoke(nameof(GameOver), 0.01f);
    }

    public void AddFuel(int amount)
    {
        inventory["fuel"] += amount;

    }

    // Returns true if successful
    public bool UseFuel()
    {
        if (inventory["fuel"] > 0)
        {
            inventory["fuel"] -= 1;
            return true;
        }
        return false;
    }

    public void AddAmmo(int amount)
    {
        inventory["ammo"] += amount;
    }

    // Returns true if successful
    public bool UseAmmo(int amount)
    {
        if (inventory["ammo"] > 0)
        {
            inventory["ammo"] -= amount;
            return true;
        }
        return false;
    }

    public int GetAmmo()
    {
        return inventory["ammo"];
    }

    public void updateDebug()
    {
        debugText.text = getHealth() + "\n" + inventory["fuel"] + "\n" + inventory["ammo"];
    }

}
