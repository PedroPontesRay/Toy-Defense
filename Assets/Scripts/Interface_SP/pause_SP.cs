using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_SP : MonoBehaviour
{
    bool muteVFX = false;
    bool muteMusic = false;


    public void Continuar()
    {
        Interface_Manager.instanceInterface.pauseMenu.SetActive(false);
        Interface_Manager.instanceInterface.gameplayMenu.SetActive(true);
        Time.timeScale= 1.0f;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void MenuPrincipal()
    {
        //chamada do menu
    }

    public void TrocarArcenal() 
    {
        //trocar arsenal
    }

    //sounds
    public void MuteUnmuteVFX()
    {
        muteVFX = !muteVFX;
        if(muteVFX == true)
        {
            //mudo
        }
        else 
        {
            //ativa
        }
    }
    public void MuteUnmuteMusic()
    {
        muteMusic = !muteVFX;
        if (muteVFX == true)
        {
            //mudo
        }
        else
        {
            //ativa
        }
    }

}
