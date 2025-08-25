using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;

    private int health;
    private int time;

    public TMP_Text healthText;

    private void Awake()
    {

    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetHealth(3);
        healthText.text = "Health : " + getHealth();
        NewLevel();
    }

    private void NewLevel()
    {
        Respawn();
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StopAllCoroutines();
    }

    private void GameOver()
    {
        player.stopAllMovement();
        player.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
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

        NewGame();
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
        SetHealth(this.health - damage);

        if (this.health <= 0)
        {
            KillPlayer();
        }
        healthText.text = "Health : " + getHealth();
    }

    // Kills Main Character
    public void KillPlayer()
    {
        player.setDead();
        Invoke(nameof(GameOver), 1f);
    }
}
