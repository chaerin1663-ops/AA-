using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButtonClick()
    {
        Debug.Log("스타트 버튼 클릭");
        SceneManager.LoadScene("Start");
    }
}
