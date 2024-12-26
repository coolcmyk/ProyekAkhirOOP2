    // using System.Collections;
    // using System.Collections.Generic;
    // using UnityEngine;

    // public class AudioController : MonoBehaviour
    // {
    //     // Start is called before the first frame update
    //     public static AudioController instance;
    //     //public double goalTime;

    //     public AudioSource  ammo, 
    //                         enemyDeath, 
    //                         enemyShot, 
    //                         gunShot, 
    //                         health, 
    //                         playerHurt,
    //                         mainSong,
    //                         footStep;
    //     //public AudioClip clip;
        
    //     public void Start()
    //     {
    //         instance = this;
    //         mainSong.loop = true;
    //     }

    //     public void PlayAmmoPickup()
    //     {
    //         ammo.Stop();
    //         ammo.Play();
    //     }

    //     public void PlayEnemyDeath()
    //     {
    //         enemyDeath.Stop();
    //         enemyDeath.Play();
    //     }

    //     public void PlayEnemyShot()
    //     {
    //         enemyShot.Stop();
    //         enemyShot.Play();
    //     }

    //     public void PlayGunShot()
    //     {
    //         gunShot.Stop();
    //         gunShot.Play();
    //     }

    //     public void PlayHealth()
    //     {
    //         health.Stop();
    //         health.Play();
    //     }

    //     public void PlayPlayerHurt()
    //     {
    //         playerHurt.Stop();
    //         playerHurt.Play();
    //     }

    //     // public void PlayFootStep()
    //     // {
    //     //     if(Input.GetKeyDown("a")){
    //     //         footStep.SetActive(true);
    //     //     }
    //     //     if(Input.GetKeyDown("d")){
    //     //         footStep.SetActive(true);
    //     //     }
    //     //     if(Input.GetKeyDown("s")){
    //     //         footStep.SetActive(true);
    //     //     }
    //     //     if(Input.GetKeyDown("w")){
    //     //         footStep.SetActive(true);
    //     //     }
    //     // }

    //     // public void StopFootStep()
    //     // {
    //     //     if(Input.GetKeyUp("w")){
    //     //         footStep.SetActive(false);
    //     //     }
    //     //     if(Input.GetKeyUp("a")){
    //     //         footStep.SetActive(false);
    //     //     }
    //     //     if(Input.GetKeyUp("s")){
    //     //         footStep.SetActive(false);
    //     //     }
    //     //     if(Input.GetKeyUp("d")){
    //     //         footStep.SetActive(false);
    //     //     }
    //     // }

    //     public void PlayFootStep()
    //     {
    //         footStep.Play();
    //     }

    //     public void PlayMusic()
    //     {
    //         if(!mainSong.isPlaying){
    //             mainSong.Play();
    //         }
    //     }

    //     public void StopMusic()
    //     {
    //         if(mainSong.isPlaying){
    //             mainSong.Stop();
    //         }
    //     }
    // /*
    //     public void OnPlayMusic()
    //     {
    //         goalTime = AudioSettings.dspTime + 0.5;
    //         audioSource.clip = currentClip;
    //         audioSource.PlayScheduled(goalTime);
    //     }
    // */
    // }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    public AudioSource ammo,
                       enemyDeath,
                       enemyShot,
                       gunShot,
                       health,
                       playerHurt,
                       mainSong,
                       footStep;

    private float footstepTimer = 0f;
    public float footstepRate = 0.3f; // Adjust this value to control footstep frequency
    private bool isWalking = false;

    public void Start()
    {
        instance = this;
        mainSong.loop = true;
        footStep.volume = 0.6f; // Adjust volume as needed
    }

    void Update()
    {
        // Check for movement input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            HandleFootsteps();
        }
        else
        {
            isWalking = false;
            footstepTimer = 0f;
        }
    }

    private void HandleFootsteps()
    {
        if (isWalking)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                footStep.pitch = Random.Range(0.95f, 1.05f); // Add slight pitch variation
                PlayFootStep();
                footstepTimer = footstepRate;
            }
        }
    }

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
        footStep.Stop();
        footStep.Play();
    }

    public void PlayMusic()
    {
        if(!mainSong.isPlaying)
        {
            mainSong.Play();
        }
    }

    public void StopMusic()
    {
        if(mainSong.isPlaying)
        {
            mainSong.Stop();
        }
    }
}