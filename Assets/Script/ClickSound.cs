using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource BtnClickSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnClickBtn()
    {
        BtnClickSound.Play();
    }
    void Start()
    {
        BtnClickSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
