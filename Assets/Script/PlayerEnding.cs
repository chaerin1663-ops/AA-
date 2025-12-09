using UnityEngine;

public class PlayerEnding : MonoBehaviour
{
    Rigidbody2D rigid2D;  
    public Animator animator;
    public gameManager gameManager;

    public AudioSource gameClear;
    public AudioSource gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    public void PlayClearAnimation()
    {
        this.animator.SetTrigger("Clear");
        gameClear.Play();
    }  
    public void GameOverAnimation()
    {
       this.animator.SetTrigger("GameOver");
       gameOver.Play();
    }    
}
