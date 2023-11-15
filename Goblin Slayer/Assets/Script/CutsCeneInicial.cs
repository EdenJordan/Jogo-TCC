using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsCeneInicial : MonoBehaviour
{
    public GameObject cutsCene1;
    public GameObject cutsCene2;
    public GameObject cutsCene3;
    public GameObject cutsCene4;
    public GameObject cutsCene5;
    public GameObject cutsCene6;
    public GameObject cutsCene7;

    private float timer;
    
    public int cutsCeneVez;

    public GameObject fade;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 6;
        cutsCeneVez = 1;
        fade.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 1)
        {
            fade.SetActive(true);
        }

        /*if ()
        {
            
        }*/
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
                break;
            case 2:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(true);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                break;
            case 3:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(true);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                break;
            case 4:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(true);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                break;
            case 5:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(true);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(false);
                break;
            case 6:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(true);
                cutsCene7.SetActive(false);
                break;
            case 7:
                cutsCene1.SetActive(false);
                cutsCene2.SetActive(false);
                cutsCene3.SetActive(false);
                cutsCene4.SetActive(false);
                cutsCene5.SetActive(false);
                cutsCene6.SetActive(false);
                cutsCene7.SetActive(true);
                break;
            default:
                break;
        }
    }
}
