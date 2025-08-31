using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameManager gm;

    public GameObject phase1Music;
    public GameObject phase2Music;


    public AudioSource soundObject;

    private void Awake()
    {
        phase1Music.SetActive(true);
        if (instance == null)
        {
            instance = this;
        }
    }

    public void playSoundClip(AudioClip audioClip, Transform t, float vol, float pitch)
    {
        //Spawn AudioSource
        AudioSource audioSource = Instantiate(soundObject, t.position, Quaternion.identity);

        //assign the audio clip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = vol;

        //assign pitch
        audioSource.pitch = pitch;

        //play sound
        audioSource.Play();

        //get length of soundFX clip
        float length = audioSource.clip.length;

        //destroy the object after the clip is done playing 
        Destroy(audioSource.gameObject, 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
