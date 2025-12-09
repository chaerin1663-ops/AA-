using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Coroutine 사용을 위해 추가

public class GameStartSoundController : MonoBehaviour
{
    public AudioSource gameStartSound;
    public string targetSceneName = "MapChoice";

    // 사운드 재생 후 씬을 전환하는 코루틴
    public void PlaySoundAndLoadScene()
    {
        // 씬 전환 시 파괴되지 않도록 설정
        DontDestroyOnLoad(this.gameObject);

        gameStartSound.Play();

        // 사운드 재생 시간에 맞춰 씬 전환을 지연시킵니다.
        // 예를 들어 사운드 길이가 2초라면 2초 후에 씬을 로드합니다.
        StartCoroutine(LoadSceneAfterDelay(gameStartSound.clip.length));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 사운드 재생 시간만큼 대기
        SceneManager.LoadScene(targetSceneName);

        // 씬 로드 후에는 이 사운드 오브젝트를 파괴하여 정리합니다.
        Destroy(this.gameObject);
    }
}