using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip MonsterHurt;
    [SerializeField]
    private AudioClip DinnerTime;
    [SerializeField]
    private AudioClip MonsterDeath;
    [SerializeField]
    private AudioClip MonsterChewing;
    private AudioSource src;
    // Use this for initialization
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMonsterDeath()
    {
        playSafe(MonsterDeath);
    }



    public void PlayDinnerTime()
    {
        playSafe(DinnerTime);
    }

    public void PlayMonsterHurt()
    {
        playSafe(MonsterHurt);
    }

    public void PlayMonsterChewing()
    {
        playSafe(MonsterChewing, true);
    }

    private void playSafe(AudioClip clip, bool loop = false)
    {
        if (src.isPlaying && !src.loop) return;
        src.loop = loop;
        src.clip = clip;
        src.Play();
    }
}
