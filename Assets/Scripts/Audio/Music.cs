using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Music : MonoBehaviour {

    [SerializeField]
    private float delayPerTick;

    [SerializeField]
    private float transitionAmountPerTick;

    [SerializeField]
    public AudioSource[] music = new AudioSource[5];

    private int currentIndex = 0;


    public void ChangeMusic(int index)
    {
        int newIndex = 0;

        if (currentIndex == index)
            newIndex = currentIndex - 1;
        else
            newIndex = index;

        
        StartCoroutine(slowlyMuteThenRaise(music[currentIndex],music[newIndex]));


        currentIndex = newIndex;
    }

    private IEnumerator slowlyMuteThenRaise(AudioSource toMute,AudioSource toRaise)
    {
        while (toMute.volume > 0)
        {
            toMute.volume = toMute.volume - transitionAmountPerTick;
            yield return new WaitForSeconds(delayPerTick);
        }

        toMute.Stop();

        toRaise.volume = 0.0f;
        toRaise.Play();

        while (toRaise.volume < 1)
        {
            toRaise.volume = toRaise.volume + transitionAmountPerTick;
            yield return new WaitForSeconds(delayPerTick);
        }

    }


    //Coroutine to take slowly mute overtime until 0
    private IEnumerator slowlyMute(AudioSource source)
    {
        while (source.volume > 0)
        {
            yield return new WaitForSeconds(delayPerTick);
            source.volume = source.volume - transitionAmountPerTick;
        }

        source.Stop();
    }


    //Coroutine to take slowly bring up music overtime until 1
    private IEnumerator slowlyBringToMax(AudioSource source)
    {
        while (source.volume < 1)
        {
            yield return new WaitForSeconds(delayPerTick);
            source.volume = source.volume + transitionAmountPerTick;
        }
    }

}
