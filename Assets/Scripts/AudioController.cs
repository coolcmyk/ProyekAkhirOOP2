using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioController instance;
    public GameObject footStep;

    public AudioSource  ammo, 
                        enemyDeath, 
                        enemyShot, 
                        gunShot, 
                        health, 
                        playerHurt, 
                        mainSong;
    public AudioClip clip;

    public void PlayAmmoPickup()
    {
        ammo.Stop();
        ammo.Play();
    }

    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }

    public void PlayEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }

    public void PlayGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }

    public void PlayHealth()
    {
        health.Stop();
        health.Play();
    }

    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }

    public void PlayFootStep()
    {
        if(Input.GetKeyDown("a")){
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("d")){
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("s")){
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("w")){
            footStep.SetActive(true);
        }
    }

    public void StopFootStep()
    {
        if(Input.GetKeyUp("w")){
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("a")){
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("s")){
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("d")){
            footStep.SetActive(false);
        }
    }
}
