using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerGameMode: MonoBehaviour
{

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 1000.0f;

    public Sprite[] healthSprites;
    public Image healthUI;

    public int maxHealth = 9;
    public int damageAmount = 1;

    private int currentHealth;

    public GameObject endingWindow;
    public GameObject GameOver;

    public gameManager gameManager;

    public AudioSource EatCoin;
    public AudioSource Hearth;
    public AudioSource Hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 점프한다
            if (Mathf.Abs(this.rigid2D.linearVelocity.y) < 0.01f)
            {
                this.animator.SetTrigger("JumpTrigger");
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            if (gameManager.instance != null)
            {
                gameManager.instance.AddScore(10);
               
            }
            
            Debug.Log("코인 획득");
            Destroy(collision.gameObject); // 코인 오브젝트 제거
            EatCoin.Play();
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("장애물 충돌!");
            TakeDamage(damageAmount);
        }
        if (collision.CompareTag("Finish"))
        {
            endingWindow.SetActive(true);
            if (gameManager.instance != null)
            {
             gameManager.gameClear();
            }
            StartCoroutine(ActivateGameOverAfterDelay(3.0f));
        }
    }

    
   
    IEnumerator ActivateGameOverAfterDelay(float delay)
        {
            // 지정된 시간(3초)만큼 대기합니다.
            yield return new WaitForSeconds(delay);

            // 대기가 끝난 후 GameOver 오브젝트를 활성화합니다.
            if (GameOver != null)
            {
                GameOver.SetActive(true);
                Debug.Log("3초 지연 후 게임 오버 화면 활성화됨.");
            }
    }


    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // 이미 죽은 경우

        // 체력 감소
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // 체력이 0보다 작아지지 않도록 처리
        this.animator.SetTrigger("Player_hit");

        Debug.Log("데미지 입음! 현재 체력: " + currentHealth);
        Hit.Play();

        // 체력이 변했으므로 UI 업데이트
        UpdateHealthUI();

        // 체력이 0이 되었을 때 처리
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        // currentHealth는 0~9의 값을 가집니다.
        // 이 값을 인덱스로 사용하여 Sprite 배열에서 이미지를 가져옵니다.
        // currentHealth 9 = Element 9 (9칸)
        // currentHealth 0 = Element 0 (0칸)

        // 배열의 길이를 초과하지 않도록 검사
        if (healthUI != null && healthSprites != null && currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            // 인덱스 currentHealth에 해당하는 스프라이트로 변경
            healthUI.sprite = healthSprites[currentHealth];
        }
        else if (currentHealth == 0 && healthSprites.Length > 0)
        {
            // 안전 장치: 체력이 0일 때 배열의 첫 번째 요소(0칸 이미지)를 사용
            healthUI.sprite = healthSprites[0];
        }

        if (currentHealth <= 3)
        {
            Hearth.Play();
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망! 게임 오버!");
        // 여기에 게임 오버 처리 (예: SceneManager.LoadScene("GameOverScene"))
        gameObject.SetActive(false); // 플레이어 오브젝트 비활성화
        SceneManager.LoadScene("GameOver");
    }
}
