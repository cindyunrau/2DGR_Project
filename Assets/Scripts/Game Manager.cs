using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.SearchService;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverScreen;

    // Mangaged Resources
    private int health;
    private int time;

    // Debug Objects
    public TMP_Text healthText;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        print("NewGame");
        StopAllCoroutines();

        SetHealth(3);
        healthText.text = "Health : " + getHealth();
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
        player.stopAllMovement();
        player.gameObject.SetActive(false);

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
        healthText.text = "Health : " + getHealth();
    }

    public void KillPlayer()
    {
        print("Kill Player");
        player.setDead();
        Invoke(nameof(GameOver), 1f);
    }
}
