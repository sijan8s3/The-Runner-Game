using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    public int score = 0;
    [SerializeField] TMP_Text finalScore;

    //total number of orbs
    public int redOrbs = 0;
    public int greenOrbs = 0;
    public int blueOrbs = 0;

    //text scoring variables
    public TMP_Text scoreText;
    public TMP_Text greenText;
    public TMP_Text redText;
    public TMP_Text blueText;

    //defining enums for cleaner code
    Orb red = Orb.red;
    Orb green = Orb.green;
    Orb blue = Orb.blue;
    Orb white = Orb.white;
    public static Orb activeOrb = Orb.white;

    public GameObject player;
    public SoundSystem soundEffects;

    //dynamic numbers for cleaner code and easily applying cheat codes
    public static int switchingThreshold = 5;
    public static int baseMultiplier = 1;
    public static int currentMultiplier = 1;
    public static int highMultiplier = 5;
    public static int scorePerOrb = 1;

    public static bool isShieldActive = false;

    public static bool isGamePaused = false;
    public static bool isGameOver = false;

    //cheat codes variables
    public static bool isInvincible = false;
    public static bool isGreedy = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    private void Start()
    {
        isGameOver = false;
        soundEffects = FindObjectOfType<SoundSystem>();
        Debug.Log(MusicToggle.isMute);
        if(MusicToggle.isMute == false)
        {
            soundEffects.PlayMusic(Levels.game);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J) && !isGamePaused) 
        {
            if(redOrbs >= switchingThreshold)
            {
                ChangeOrb(red);
            }
            else
            {
                if (MusicToggle.isMute == false)
                {
                    soundEffects.PlaySFX(Events.insufficientOrbs);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.K) && !isGamePaused)
        {
            if (greenOrbs >= switchingThreshold)
            {
                ChangeOrb(green);
            }
            else
            {
                if (MusicToggle.isMute == false)
                {
                    soundEffects.PlaySFX(Events.insufficientOrbs);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.L) && !isGamePaused)
        {
            if (blueOrbs >= switchingThreshold)
            {
                ChangeOrb(blue);
            }
            else
            {
                if (MusicToggle.isMute == false)
                {
                    soundEffects.PlaySFX(Events.insufficientOrbs);
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && activeOrb != white && !isGamePaused)
        {
            ActivatePower();
        }
        if (Input.GetKeyUp(KeyCode.I) && isGreedy)
        {
            redOrbs++;
            UpdateScore();
        }
        if (Input.GetKeyUp(KeyCode.O) && isGreedy)
        {
            greenOrbs++;
            UpdateScore();
        }
        if (Input.GetKeyUp(KeyCode.P) && isGreedy)
        {
            blueOrbs++;
            UpdateScore();
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !isGameOver)
        {
            ForwardForce.movingSpeed = isGamePaused ? ForwardForce.SetMovingSpeed() :
                0;
            HandlePauseMenu();
        }
    }

    public void HandlePauseMenu()
    {
        ForwardForce.movingSpeed = isGamePaused ? ForwardForce.SetMovingSpeed() : 0;
        isGamePaused = !isGamePaused;
        pauseMenu.SetActive(isGamePaused);
        if (isGamePaused)
        {
            FindObjectOfType<CheatsScript>().cheat.text = "";
            FindObjectOfType<CheatsScript>().feedback.text = "";

        }
        if (MusicToggle.isMute == false)
        {
            soundEffects.PlayMusic(isGamePaused ? Levels.title : Levels.game);
        }

    }

    public void Restart()
    {
        HandlePauseMenu();
        activeOrb = Orb.white;
        isInvincible = false;
        isShieldActive = false;
        isGreedy = false;
        currentMultiplier = baseMultiplier;

    }

    public void GoToMainMenu()
    {

        SceneManager.LoadScene(0);
        ForwardForce.movingSpeed = ForwardForce.SetMovingSpeed();
        isGamePaused = false;
        pauseMenu.SetActive(false);
        if (MusicToggle.isMute == false)
        {
            soundEffects.PlayMusic(Levels.title);
        }

    }

    public void ChangeOrb(Orb color)
    {
        if(activeOrb != color)
        {
            isShieldActive = false;
            player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(isShieldActive);

            currentMultiplier = baseMultiplier;

            activeOrb = color;
            if (MusicToggle.isMute == false)
            {
                soundEffects.PlaySFX(Events.changeForm);
            }
            switch (color)
            {
                case Orb.red: redOrbs--;
                    player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
                    break;
                case Orb.blue: blueOrbs--;
                    player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                    break;
                case Orb.green: greenOrbs--;
                    player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
                    break;
                case Orb.white: player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.white; break;
                default: break;

            }
            UpdateScore();

        }
    }

    public void ActivatePower()
    {
        bool isPowerActivated = false;
        switch (activeOrb)
        {
            case Orb.red:
                if (redOrbs > 0)
                {
                    RedPower();
                    isPowerActivated = true;
                }
                break;
            case Orb.green:
                if (greenOrbs > 0)
                {
                    if(currentMultiplier != highMultiplier)
                    {
                        GreenPower();
                        isPowerActivated = true;
                    }
                    else
                    {
                        if (MusicToggle.isMute == false)
                        {
                            soundEffects.PlaySFX(Events.insufficientOrbs);
                        }
                    }

                }
                break;
            case Orb.blue:
                if (blueOrbs > 0)
                {
                    if (!isShieldActive)
                    {
                        BluePower();
                        isPowerActivated = true;
                    }
                    else
                    {
                        if (MusicToggle.isMute == false)
                        {
                            soundEffects.PlaySFX(Events.insufficientOrbs);
                        }
                    }

                }
                break;
            default: break;
        }
        if (MusicToggle.isMute == false && isPowerActivated)
        {
            soundEffects.PlaySFX(Events.activatePower);
        }
    }

    void RedPower()
    {
        isShieldActive = false;
        player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(isShieldActive);
        currentMultiplier = baseMultiplier;


        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
            Destroy(obstacle);
        redOrbs--;
        if (redOrbs == 0)
        {
            ChangeOrb(white);
        }
        UpdateScore();

    }

    void GreenPower()
    {
        isShieldActive = false;
        player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(isShieldActive);

        
        currentMultiplier = highMultiplier;
        greenOrbs--;
        if(greenOrbs == 0)
        {
            ChangeOrb(white);
        }
        UpdateScore();

    }

    void BluePower()
    {
        isShieldActive = true;
        player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(isShieldActive);
        blueOrbs--;
        if (blueOrbs == 0)
        {
            isShieldActive = false;
            player.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(isShieldActive);
            ChangeOrb(white);
        }
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        greenText.text = greenOrbs + "";
        redText.text = redOrbs + "";
        blueText.text = blueOrbs + "";
    }

    public void GameOver()
    {
        //stop the camera from moving after player destruction
        Destroy(player.GetComponent<ForwardForce>());
        gameOverMenu.SetActive(true);
        isGameOver = true;
        if(MusicToggle.isMute == false)
        {
            soundEffects.PlayMusic(Levels.title);
        }
        finalScore.text = "Your score: " + score;
    }
}
