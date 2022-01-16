using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour
{
    public static AudioClip shoot, mysterySound, invaderKilled, invaderShoot, playerKilled;
    static AudioSource audioSrc;

    void Start()
    {
        //przypisanie AudioClip do dŸwiêków znajduj¹cych siê w folderze Resources
        shoot = Resources.Load<AudioClip>("shoot");
        mysterySound = Resources.Load<AudioClip>("ufo");
        invaderKilled = Resources.Load<AudioClip>("invaderkilled");
        invaderShoot = Resources.Load<AudioClip>("invaderShoot");
        playerKilled = Resources.Load<AudioClip>("explosion");

        audioSrc = GetComponent<AudioSource>(); //odniesienie do komponentu AudioSource
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shoot":
                audioSrc.PlayOneShot(shoot);
                break;
            case "invaderKilled":
                audioSrc.PlayOneShot(invaderKilled);
                break;
            case "ufo":
                audioSrc.PlayOneShot(mysterySound);
                break;
            case "invaderShoot":
                audioSrc.PlayOneShot(invaderShoot);
                break;
            case "playerKilled":
                audioSrc.PlayOneShot(playerKilled);
                break;
        }
    }
}
