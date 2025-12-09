using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroSoundManager : MonoBehaviour
{
    // 정적(static) 변수를 사용하여 어디서든 접근 가능하게 합니다.
    public static IntroSoundManager Instance;

    public AudioSource IntroBgm;

    private void Awake()
    {
        // 씬에서 IntroSoundManager 타입의 모든 활성 인스턴스를 찾습니다.
        // FindObjectsByType을 사용해야 배열이 반환되며, Length 속성을 사용할 수 있습니다.
        var soundManagers = FindObjectsByType<IntroSoundManager>(FindObjectsSortMode.None);

        // 찾은 인스턴스의 개수가 1개보다 크다면, 중복이므로 현재 인스턴스를 파괴합니다.
        if (soundManagers.Length > 1)
        {
            // 중복된 오브젝트는 파괴합니다.
            Destroy(gameObject);
        }
        else // 현재 인스턴스가 유일하다면
        {
            // 이 인스턴스를 전역 Instance로 설정합니다.
            Instance = this;

            // 씬 전환 시 파괴되지 않도록 설정합니다.
            // (이 코드가 Root GameObject에 붙어있어야 'DontDestroyOnLoad' 오류가 발생하지 않습니다.)
            DontDestroyOnLoad(gameObject);

            // 만약 시작 시 BGM을 바로 재생하고 싶다면 여기서 호출합니다.
            // IntroBgm.Play(); 
        }
    }

    void Start()
    {
        // BGM이 할당되어 있고 재생 중이 아니라면 재생
        if (IntroBgm != null && !IntroBgm.isPlaying)
        {
            IntroBgm.Play();
        }
    }
}