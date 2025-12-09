using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStartMode : MonoBehaviour
{
    public float speed;
    public Animator Stand_Run;

    const int PLAYER_STAND = 0;
    const int PLAYER_RUN = 10;
    const int PLAYER_JUMP = 20;

    int playerMotion = PLAYER_STAND;

    //public AudioSource Start_Walk;
    public AudioSource GameStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMotion = PLAYER_STAND;

    }

    // Update is called once per frame
    void Update()
    {
        bool ispressingUp = Input.GetKey(KeyCode.Space);
        bool ispressingLeft = Input.GetKey(KeyCode.LeftArrow);
        bool ispressingRight = Input.GetKey(KeyCode.RightArrow);

        playerMotion = PLAYER_STAND;

        if (ispressingLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            transform.localScale = new Vector3(-1, 1, 1);
            playerMotion = PLAYER_RUN;
            //Start_Walk.Play();
        }
        if (ispressingRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            transform.localScale = new Vector3(1, 1, 1);
            playerMotion = PLAYER_RUN;
            //Start_Walk.Play();
        }

        if (Stand_Run != null)
        {
            Stand_Run.SetInteger("playerMotion", playerMotion);
        }

         if (ispressingUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            playerMotion = PLAYER_JUMP;
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Start"))
        {
            FindAnyObjectByType<GameStartSoundController>().PlaySoundAndLoadScene();
            // Debug.Log("게임 시작");

            Destroy(collision.gameObject);
            //SceneManager.LoadScene("MapChoice");
                   
        }
    }/*
    IEnumerator LoadSceneAfterSound(float delay)
    {
        // 사운드 클립 길이(초)만큼 대기
        yield return new WaitForSeconds(delay);

        // 대기 후 씬 전환
        SceneManager.LoadScene("MapChoice");
    }
    */
}

