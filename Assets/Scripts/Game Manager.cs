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

    public GameObject sword;
    public GameObject swordPickup;
    public GameObject spear;
    public GameObject spearPickup;
    public GameObject pistol;
    public GameObject pistolPickup; 
    public GameObject shotgun;
    public GameObject shotgunPickup; 

    private int numWeapons = 0;
    private string curWeapon;
    private string backupWeapon;


    // Mangaged Resources
    private int health;
    private int maxHealth = 4;

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
        if (Input.GetMouseButtonDown(1))
        {
            SwapWeapons();
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
        print("Take Damage");
        SetHealth(this.health - damage);

        if (this.health <= 0)
        {
            KillPlayer();
        }
    }

    public void HealPlayer(int healing)
    {
        print("Healing");
        if (this.health < maxHealth)
        {
            SetHealth(this.health + healing);
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
    public void AddSword()
    {
        if (numWeapons >= 2)
        {
            //spawn current weapon pickup on the ground
            //

            sword.SetActive(true);
            switch (curWeapon)
            {
                case "Spear":
                    spear.SetActive(false);
                    spearPickup.transform.position = player.transform.position;
                    spearPickup.SetActive(true);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    pistolPickup.transform.position = player.transform.position;
                    pistolPickup.SetActive(true);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    shotgunPickup.transform.position = player.transform.position;
                    shotgunPickup.SetActive(true);
                    break;
            }
            curWeapon = "Sword";

        }
        else if (numWeapons == 1)
        {
            sword.SetActive(true);
            switch (curWeapon)
            {
                case "Spear":
                    spear.SetActive(false);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    break;
            }
            numWeapons++;
            backupWeapon = curWeapon;
            curWeapon = "Sword";
        }
        else if (numWeapons == 0)
        {
            sword.SetActive(true);
            numWeapons++;
            backupWeapon = "None";
            curWeapon = "Sword";

        }
    }

    public void AddSpear()
    {
        if (numWeapons >= 2)
        {
            //spawn current weapon pickup on the ground
            //

            spear.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    swordPickup.transform.position = player.transform.position;
                    swordPickup.SetActive(true);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    pistolPickup.transform.position = player.transform.position;
                    pistolPickup.SetActive(true);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    shotgunPickup.transform.position = player.transform.position;
                    shotgunPickup.SetActive(true);
                    break;
            }
            curWeapon = "Spear";

        }
        else if (numWeapons == 1)
        {
            spear.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    break;
            }
            numWeapons++;
            backupWeapon = curWeapon;
            curWeapon = "Spear";
        }
        else if (numWeapons == 0)
        {
            spear.SetActive(true);
            numWeapons++;
            curWeapon = "Spear";
            backupWeapon = "None";
        }
    }

    public void AddPistol()
    {
        if (numWeapons >= 2)
        {
            //spawn current weapon pickup on the ground
            //

            pistol.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    swordPickup.transform.position = player.transform.position;
                    swordPickup.SetActive(true);
                    break;
                case "Spear":
                    spear.SetActive(false);
                    spearPickup.transform.position = player.transform.position;
                    spearPickup.SetActive(true);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    shotgunPickup.transform.position = player.transform.position;
                    shotgunPickup.SetActive(true);
                    break;
            }
            curWeapon = "Pistol";

        }
        else if (numWeapons == 1)
        {
            pistol.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    break;
                case "Spear":
                    spear.SetActive(false);
                    break;
                case "Shotgun":
                    shotgun.SetActive(false);
                    break;
            }
            numWeapons++;
            backupWeapon = curWeapon;
            curWeapon = "Pistol";
        }
        else if (numWeapons == 0)
        {
            pistol.SetActive(true);
            numWeapons++;
            curWeapon = "Pistol";
            backupWeapon = "None";
        }
    }
    public void AddShotgun()
    {
        if (numWeapons >= 2)
        {
            //spawn current weapon pickup on the ground
            //

            shotgun.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    swordPickup.transform.position = player.transform.position;
                    swordPickup.SetActive(true);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    pistolPickup.transform.position = player.transform.position;
                    pistolPickup.SetActive(true);
                    break;
                case "Spear":
                    spear.SetActive(false);
                    spearPickup.transform.position = player.transform.position;
                    spearPickup.SetActive(true);
                    break;
            }
            curWeapon = "Shotgun";

        }
        else if (numWeapons == 1)
        {
            shotgun.SetActive(true);
            switch (curWeapon)
            {
                case "Sword":
                    sword.SetActive(false);
                    break;
                case "Pistol":
                    pistol.SetActive(false);
                    break;
                case "Spear":
                    spear.SetActive(false);
                    break;
            }
            numWeapons++;
            backupWeapon = curWeapon;
            curWeapon = "Shotgun";
        }
        else if (numWeapons == 0)
        {
            shotgun.SetActive(true);
            numWeapons++;
            curWeapon = "Shotgun";
            backupWeapon = "None";
        }
    }

    public void SwapWeapons()
    {
        Debug.Log(curWeapon);
        Debug.Log(backupWeapon);

        if (numWeapons < 2)
        {
            return;
        }
        switch (curWeapon)
        {
            case "Sword":
                sword.SetActive(false);
                break;
            case "Spear":
                spear.SetActive(false);
                break;
            case "Pistol":
                pistol.SetActive(false);
                break;
            case "Shotgun":
                shotgun.SetActive(false);
                break;
        }
        switch (backupWeapon)
        {
            case "Sword":
                sword.SetActive(true);
                break;
            case "Spear":
                spear.SetActive(true);
                break;
            case "Pistol":
                pistol.SetActive(true);
                break;
            case "Shotgun":
                shotgun.SetActive(true);
                break;
        }
        string s = curWeapon;
        curWeapon = backupWeapon;
        backupWeapon = s;
    }
    public void updateDebug()
    {
        debugText.text = getHealth() + "\n" + inventory["fuel"] + "\n" + inventory["ammo"];
    }

}
