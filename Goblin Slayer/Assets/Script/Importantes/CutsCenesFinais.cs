using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsCenesFinais : MonoBehaviour
{
    public GameObject cutsCene1;
    public GameObject cutsCene2;
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
            fade.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene("Creditos");
        }
    }

    public void CutsCenes()
    {
        switch (cutsCeneVez)
        {
            case 1:
                cutsCene1.SetActive(true);
                cutsCene2.SetActive(false);
                break;
            case 2:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(true);
                break;
            case 3:
                ultimaCutsCene = true;
                break;
            default:
                break;
        }
    }
}
