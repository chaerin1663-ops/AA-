using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;

public class gameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Renderer bgRenderer;
    private float bgSpeed = 0.3f;
    [SerializeField] private float accelerationRate = 0.1f;

    public static gameManager instance;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI socrePopup;

    private int score = 0;

    public Animator grade;
    public Animator gradePopup;

    private int currentAnimStage = 0;

    public GameObject endingWindow;
    public GameObject GameOver;
    public GameObject stopWindow;

    public PlayerEnding PlayerEnding;

    public AudioSource GradeUP;


    /*
    const int GAME_OVER = 40;
    const int GAME_CLEAR = 20;
    const int GAME_PLAY = 10;

    int gamemode = GAME_PLAY;
    */
    Rigidbody2D rigid2D;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreDisplay();
        //gamemode = GAME_PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseSpeed();
        BGScrolling();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            stopWindow.SetActive(true);
        }
        /*
        if (gamemode == GAME_PLAY)
        {
            if (currentAnimStage == 4)
            {
                gamemode = GAME_CLEAR;
            }
        }

        else if (gamemode == GAME_CLEAR)
        {
            //GameClearProcedure();

        }
        */

    }

    /* private void GameClearProcedure()
     {
         if (PlayerEnding != null)
         {
             PlayerEnding.PlayClearAnimation();

         }
     }    
    */
    private void IncreaseSpeed()
    {
        bgSpeed += Time.deltaTime * accelerationRate;
    }


    private void BGScrolling()
    {
        float move = Time.deltaTime * bgSpeed;

        bgRenderer.material.mainTextureOffset += Vector2.right * move;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();

        CheckForAnimationTransition();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString("D4");
        socrePopup.text = score.ToString("D4");
    }
    public void CheckForAnimationTransition()
    {
        if (grade == null || gradePopup == null) return;

        if (score >= 1200 && currentAnimStage < 11)
        {
            grade.SetBool("A+", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 12;
            Debug.Log("학점 A+ 달성!");
            GradeUP.Play();
        }

        if (score >= 1100 && currentAnimStage < 10)
        {
            grade.SetBool("A", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 10;
            Debug.Log("학점 A 달성!");
            GradeUP.Play();
        }

        if (score >= 1000 && currentAnimStage < 9)
        {
            grade.SetBool("A-", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 9;
            Debug.Log("학점 A- 달성!");
            GradeUP.Play();
        }

        if (score >= 900 && currentAnimStage < 8)
        {
            grade.SetBool("B+", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 8;
            Debug.Log("학점 B+ 달성!");
            GradeUP.Play();
        }

        if (score >= 700 && currentAnimStage < 7)
        {
            grade.SetBool("B", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 7;
            Debug.Log("학점 B 달성!");
            GradeUP.Play();
        }

        if (score >= 600 && currentAnimStage < 6)
        {
            grade.SetBool("B-", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 6;
            Debug.Log("학점 B- 달성!");
            GradeUP.Play();
        }

        if (score >= 500 && currentAnimStage < 5)
        {
            grade.SetBool("C+", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 5;
            Debug.Log("학점 C+ 달성!");
            GradeUP.Play();
        }

        if (score >= 400 && currentAnimStage < 4)
        {
            grade.SetBool("C", true);
            //gradePopup.SetBool("graduate", true);
            currentAnimStage = 4;
            Debug.Log("학점 C 달성!");
            GradeUP.Play();
        }

        else if (score >= 300 && currentAnimStage < 3)
        {
            grade.SetBool("C-", true);
            //gradePopup.SetBool("4th", true);
            currentAnimStage = 3;
            Debug.Log("학점 C- 달성!");
            GradeUP.Play();
        }
        else if (score >= 200 && currentAnimStage < 2)
        {
            grade.SetBool("D+", true);
            // gradePopup.SetBool("3rd", true);
            currentAnimStage = 2;
            Debug.Log("학점 D+ 달성!");
            GradeUP.Play();


        }
        else if (score >= 100 && currentAnimStage < 1)
        {
            grade.SetBool("D", true);
            //gradePopup.SetBool("2nd", true);
            currentAnimStage = 1;
            Debug.Log("학점 D 달성!");
            GradeUP.Play();
        }

    }

    public void gameClear() 
    {
        if (currentAnimStage >= 4)
        {
            //SceneManager.LoadScene("Clear");
            if (PlayerEnding != null)
            {
                PlayerEnding.PlayClearAnimation();
            } 
        }
        else if (currentAnimStage <= 3)
        {
            //SceneManager.LoadScene("GameOver");
            
            if (PlayerEnding != null)
            {
                PlayerEnding.GameOverAnimation();
            }
            
        }    
    }

    public void OnClickStopBtn()
    {
        Time.timeScale = 0;
        stopWindow.SetActive(true);
    }
    public void OnClickContinueBtn()
    {
        Time.timeScale = 1;
        stopWindow.SetActive(false);
    }
    public void OnClickRetryBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameMap");
    }

    public void OnClickHomeBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("start");
    }

    public void OnBackHomeClick()
    {
        SceneManager.LoadScene("Start");
    }
}
