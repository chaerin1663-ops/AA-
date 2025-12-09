using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI 요소를 사용하기 위해 필요

public class HealthSystem : MonoBehaviour
{
    // === 인스펙터에 설정할 항목 ===

    // 체력 이미지 스프라이트들을 저장할 배열 (0칸부터 10칸까지, 총 11개가 필요합니다)
    // ⚠️ 배열 크기는 11로 설정해야 0~10까지의 체력을 모두 커버합니다.
    public Sprite[] healthSprites;

    // UI Image 컴포넌트 참조 (체력 표시 이미지가 붙어있는 UI 오브젝트)
    public Image healthUI;

    // === 체력 변수 ===
    public int maxHealth = 9;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        // 시작 시 초기 체력 이미지 설정
        UpdateHealthUI();
    }

    // 체력을 감소시키는 메서드
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return; // 이미 죽은 경우 더 이상 처리하지 않음

        // 체력 감소
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(0, currentHealth); // 체력이 0보다 작아지지 않도록 최소값 설정

        Debug.Log("현재 체력: " + currentHealth);

        // 체력이 변했으므로 UI 업데이트
        UpdateHealthUI();

        // 체력이 0이 되었을 때 처리
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // UI 이미지를 현재 체력에 맞게 업데이트하는 핵심 메서드
    void UpdateHealthUI()
    {
        // currentHealth는 0~10의 값을 가집니다.
        // 예를 들어 currentHealth가 7이면, 배열의 인덱스 7에 해당하는 스프라이트를 사용합니다.
        // 배열의 길이를 초과하지 않도록 currentHealth가 maxHealth를 넘지 않는다고 가정합니다.

        if (healthUI != null && healthSprites != null && healthSprites.Length > currentHealth)
        {
            // 인덱스 currentHealth에 해당하는 스프라이트로 변경
            healthUI.sprite = healthSprites[currentHealth];
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망!");
        // 여기에 게임 오버 로직을 추가합니다.
    }
}