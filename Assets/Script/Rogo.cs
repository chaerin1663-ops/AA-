using System.Collections;
using UnityEngine;

public class Rogo : MonoBehaviour
{
    [Header("Hover Settings")]
    [Tooltip("중심 위치에서 위아래로 움직이는 최대 거리")]
    public float amplitude = 0.5f; // 진폭 (왕복 거리)

    [Tooltip("왕복 운동의 속도. 값이 높을수록 빨라집니다.")]
    public float frequency = 2.0f; // 빈도 (속도)

    [Tooltip("부유 운동 시작까지 기다릴 시간")]
    public float startDelay = 1.0f; // 1초 딜레이 설정

    private Vector3 startPosition; // 오브젝트의 초기 위치 (왕복 운동의 중심)
    private bool isHovering = false; // 부유 운동 상태 플래그

    void Start()
    {
        // 1. Start()에서 바로 코루틴을 시작하여 지연 로직을 실행합니다.
        StartCoroutine(StartHoverWithDelay());
    }

    void Update()
    {
        // 4. isHovering이 true일 때만 부유 운동을 실행합니다.
        if (isHovering)
        {
            HoverMovement();
        }
    }

    // 부유 운동을 지연시키는 코루틴
    private IEnumerator StartHoverWithDelay()
    {
        // 2. startDelay (1.0초) 만큼 기다립니다.
        yield return new WaitForSeconds(startDelay);

        // 3. 딜레이가 끝난 후, 현재 오브젝트의 위치를 부유 운동의 중심(startPosition)으로 저장합니다.
        startPosition = transform.position;

        // 4. 부유 운동을 시작할 수 있도록 플래그를 true로 설정합니다.
        isHovering = true;
    }

    // 위아래 왕복 운동을 계산하고 적용하는 메서드
    private void HoverMovement()
    {
        // 시간에 따른 주기적인 오프셋(offset) 계산
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // X, Z축은 startPosition을 유지하고 Y축만 오프셋을 적용합니다.
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + yOffset,
            startPosition.z
        );
    }
}