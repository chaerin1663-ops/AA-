using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Coroutine을 사용하기 위해 필요합니다.
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
    // 캔버스 그룹 컴포넌트를 참조할 변수
    private CanvasGroup canvasGroup;

    // 애니메이션 설정 변수
    public float initialDelay = 2.0f; // 초기 딜레이 시간 (2초)
    public float fadeInDuration = 1.0f; // 서서히 나타나는 시간
    public float blinkDuration = 0.5f; // 깜빡이는 주기 (한 번 나타나고 사라지는 데 걸리는 시간)

    private Coroutine blinkCoroutine;
    

    private Animator animator;

    void Start()
    {
 
        // 오브젝트에 부착된 CanvasGroup 컴포넌트 가져오기
        canvasGroup = GetComponent<CanvasGroup>();

        // 초기 투명도 0으로 설정
        canvasGroup.alpha = 0f;

        // Coroutine 시작
        StartCoroutine(StartAnimationSequence());
    }  
    

    // 전체 애니메이션 순서를 제어하는 코루틴
    IEnumerator StartAnimationSequence()
    {
        // 1. 초기 딜레이 (2초)
        yield return new WaitForSeconds(initialDelay);

        // 2. 서서히 나타나기 (Fade In)
        yield return StartCoroutine(FadeCanvasGroup(1f, fadeInDuration));

        // Fade In 완료 후, 버튼 상호작용 가능하도록 설정
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // 3. 깜빡이기 시작 (Blink)
        // 무한 반복 코루틴 시작
        blinkCoroutine = StartCoroutine(BlinkCanvasGroup());
    }

    // 지정된 시간 동안 캔버스 그룹을 페이드 인/아웃 시키는 코루틴
    IEnumerator FadeCanvasGroup(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            // Lerp 함수를 사용하여 현재 알파 값을 부드럽게 변경
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null; // 다음 프레임까지 대기
        }

        // 정확히 목표 알파 값으로 설정
        canvasGroup.alpha = targetAlpha;
    }

    // 깜빡이는 효과를 만드는 코루틴
    IEnumerator BlinkCanvasGroup()
    {
        while (true)
        {
            // 완전히 보이기 (Alpha 1) -> 숨기기 (Alpha 0)
            // 즉, targetAlpha를 0으로 설정하여 투명하게 만듭니다.
            yield return StartCoroutine(FadeCanvasGroup(0f, blinkDuration / 2));

            // 완전히 숨기기 (Alpha 0) -> 보이기 (Alpha 1)
            // 즉, targetAlpha를 1로 설정하여 다시 보이게 만듭니다.
            yield return StartCoroutine(FadeCanvasGroup(1f, blinkDuration / 2));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        StartCoroutine(FadeCanvasGroup(1f, 0.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blinkCoroutine = StartCoroutine(BlinkCanvasGroup());
    }

   
}