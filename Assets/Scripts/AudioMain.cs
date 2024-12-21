using System.Collection;
using System.Collections.Generic;
using UnityEngine;

public class AudioMain: MonoBehaviour 
{
    public static AudioController instance;

    public AudioSource ammo, enemyDeath, enemyShot, gunShot, health, playerHurt;
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

    public PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }


}