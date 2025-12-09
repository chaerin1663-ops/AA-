using UnityEngine;

public class btnchaSelect : MonoBehaviour
{

    public GameObject Hair1;
    public GameObject Hair2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnBtnRightClick()
    {
        Hair1.SetActive(true);
    }

    public void OnBtnRighttwoClick()
    {
        Hair2.SetActive(true);
        Hair1.SetActive(false);
    }

    public void OnBtnLeftClick()
    {
        Hair1.SetActive(true);
        Hair2.SetActive(false);
    }
    public void OnBtnleftTwoClick()
    {
        Hair1.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
