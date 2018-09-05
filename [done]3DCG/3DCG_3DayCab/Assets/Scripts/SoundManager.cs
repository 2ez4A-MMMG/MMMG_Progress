using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager soundMg;
    //audio parts
    [Header("BGM")]
    public AudioSource bgm_source;
    [Header("SFXs")]
    public AudioSource sfx_source;
    public AudioClip killerLaugh_sfx;
    public AudioClip carCrash_sfx;
    public AudioClip explosion_sfx;
    public AudioClip drawDagger_sfx;
    public AudioClip fleshRip_sfx;
    public AudioClip manScream_sfx;
    public AudioClip gunShot_sfx;
    public AudioClip openCarDoor_sfx;
    public AudioClip closeCarDoor_sfx;

    // Use this for initialization
    void Awake () {
        soundMg = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Audio Controller - playsoundeffectsONCE
    public void Play_sfx(AudioClip sfx, float volume)
    {
        sfx_source.clip = sfx;
        sfx_source.volume = volume;
        sfx_source.Play();
    }
    //coroutine version
    public IEnumerator Play_sfxCt(AudioClip sfx, float volume, float time)
    {
        sfx_source.clip = sfx;
        sfx_source.volume = volume;
        sfx_source.Play();
        yield return new WaitForSeconds(time);
        //audiosource.PlayOneShot(sfx, volume);
        //yield return new WaitForSeconds(sfx.length + timeOffset);
    }
}
