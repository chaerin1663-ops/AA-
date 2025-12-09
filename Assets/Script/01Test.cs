using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rigid2D;

    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // 1. Update 시작 시 기본값은 Stand

        // 2. 키입력 및 이동 방향 설정
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) { key = 1; }
        if (Input.GetKey(KeyCode.LeftArrow)) { key = -1; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 점프한다
            if (this.rigid2D.linearVelocity.y == 0)
            {
                this.animator.SetTrigger("JumpTrigger");
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }
        }

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.linearVelocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 맞춰 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(
                key * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
                );
        }

        // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다
        if (this.rigid2D.linearVelocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        /*
        bool ispressingUp = Input.GetKey(KeyCode.Space);
        bool ispressingLeft = Input.GetKey(KeyCode.LeftArrow);
        bool ispressingRight = Input.GetKey(KeyCode.RightArrow);



        if (ispressingLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);

        }
        if (ispressingRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);

        }
        if (ispressingUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);

        }
        */

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Start"))
        {
            Debug.Log("게임 시작");
            SceneManager.LoadScene("Map1");
            Destroy(collision.gameObject);
        }
    }
}
