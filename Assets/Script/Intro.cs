using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public AudioSource IntroBGM;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButtonClick()
    {
        
        SceneManager.LoadScene("Start");
    }
}
