using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Player player;

    private int score;
    private int health;
    private int time;

    private void Awake()
    {

    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetHealth(3);
        NewLevel();
    }

    private void NewLevel()
    {
        Respawn();
    }

    private void Respawn()
    {

        StopAllCoroutines();
    }

    public void Kill()
    {
        Invoke(nameof(GameOver), 1f);
    }

    private void GameOver()
    {
        player.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        bool playAgain = false;

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

    private void AddScore(int score)
    {
        this.score += score;
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetHealth(int health)
    {
        this.health = health;
    }
}
