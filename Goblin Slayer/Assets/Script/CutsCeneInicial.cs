using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsCeneInicial : MonoBehaviour
{
    public GameObject cutsCene1;
    public GameObject cutsCene2;
    public GameObject cutsCene3;
    public GameObject cutsCene4;
    public GameObject cutsCene5;
    public GameObject cutsCene6;
    public GameObject cutsCene7;
    public GameObject cutsCene8;
    public GameObject fade;

    public bool ultimaCutsCene;
    
    private float timer;
    
    public int cutsCeneVez;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        cutsCeneVez = 1;
        fade.SetActive(true);
        ultimaCutsCene = false;
    }

    // Update is called once per frame
    void Update()
    {
        CutsCenes();
        
        if (!ultimaCutsCene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cutsCeneVez = 8;
            }
            
            timer += Time.deltaTime;

            if (timer >= 8)
            {
                fade.GetComponent<Animator>().Play("FadeOut", -1, 0f);
                cutsCeneVez++;
                timer = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fade.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene("Fase1");
            }
        }
    }

    public void CutsCenes()
    {
        switch (cutsCeneVez)
        {
            case 1:
                cutsCene1.SetActive(true);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 2:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(true);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 3:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(true);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 4:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(true);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 5:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(true);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 6:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(true);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(false);
                break;
            case 7:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(true);
                cutsCene8.SetActive(false);
                break;
            case 8:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                cutsCene8.SetActive(true);
                ultimaCutsCene = true;
                break;
            default:
                break;
        }
    }
}
