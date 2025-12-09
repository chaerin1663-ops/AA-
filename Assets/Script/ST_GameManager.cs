using UnityEngine;
using UnityEngine.SceneManagement;

public class ST_GameManager : MonoBehaviour
{
    public GameObject characterSelelct;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnClickCharacterSelect()
    {
        characterSelelct.SetActive(true);
    }

    public void OnClickEscape()
    {
        characterSelelct.SetActive(false);
    }
}
