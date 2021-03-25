using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text critHit;
    public Text superEff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CritHit()
    {
        critHit.gameObject.SetActive(true);
        StartCoroutine(CritHitDelay());
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    IEnumerator CritHitDelay()
    {
        yield return new WaitForSeconds(2);
        critHit.gameObject.SetActive(false);

    }
}
