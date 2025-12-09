using UnityEngine;
using UnityEngine.EventSystems;

public class STButtonAni : MonoBehaviour
{
    
    private Renderer objectRenderer;
    private Color originalColor;
    private Animator animator;

    public int layerIndex = 0;
    public string targetStateName = "StartIcon";

    public Color highlightColor = Color.gray;
    

    public AudioSource BtnClick;


  
    void Start()
    {
        
        objectRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }

        if (animator != null)
        {
            Debug.LogError("Animator 컴포넌트가 없습니다.");
        }

        if (animator != null)
        {
            animator.speed = 1f;
        }    
    }

    public void OnPointerEnterPause()
    {
        if (animator == null) return;
        animator.speed = 0f;
        animator.Play(targetStateName, layerIndex, 1.0f);
        //Debug.Log("애니메이션 정지 및 마지막 프레임 고정");
    }
    public void OnPointerExitResum()
    {
        if (animator == null) return;
        animator.speed = 1f;
        animator.Play(targetStateName, layerIndex, 0.0f);
    }
    public void OnPointerEnterEffect()
    {
        if (objectRenderer != null)
            {
                objectRenderer.material.color = highlightColor;
            }
    }
    public void OnPointerExitEffect()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
        
    } 
}

