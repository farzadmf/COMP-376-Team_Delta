using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SkipStoryScript : MonoBehaviour {

    bool mFadeIn, mFadeOut;

    private Text storyText;
    private float mTime;
    private bool[] shown = { false, false, false, false, false };
    private bool[] hidden = { false, false, false, false, false };
    private Animator anim;

	// Use this for initialization
	void Start () {
        storyText = gameObject.GetComponentsInChildren<Text>()[0];
        storyText.enabled = false;
        mTime = 0.0f;
        anim = storyText.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("RealPrototype1");
        }

        if (mTime > 0.2f && !shown[0])
        {
            storyText.text = "Once there was a scientist. He was not well known and was looked down upon by those around him. The only field he excelled in was the experimentation on human beings.";
            mFadeIn = true;
            shown[0] = true;
        }

        if (mTime > 7 && !hidden[0])
        {
            mFadeOut = true;
            hidden[0] = true;
        }

        if (mTime > 9.5 && !shown[1])
        {
            storyText.text = "One day, in order to be recognized by others as a genius, he decided he would make the ultimate being. He started his experiments on animals but even that was not enough, he needed living human beings. So he set forth and kidnapped humans to test on.";
            mFadeIn = true;
            shown[1] = true;
        }

        if (mTime > 21 && !hidden[1])
        {
            mFadeOut = true;
            hidden[1] = true;
        }

        if (mTime > 24 && !shown[2])
        {
            storyText.text = "The progress he made came at a price, each failed experiment would lose it's mind and start attacking those around it. In order to get rid of them, he killed them and dumped their bodies anywhere he could, not realizing that they were actually still alive.";
            mFadeIn = true;
            shown[2] = true;
        }

        if (mTime > 35.5 && !hidden[2])
        {
            mFadeOut = true;
            hidden[2] = true;
        }

        if (mTime > 39 && !shown[3])
        {
            storyText.text = "After many failures, one experiment, you, showed promise and the scientist was overjoyed. However you too, lost your mind and so was \"killed\" by the scientist. With your body dumped in a cemetary left to rot, you somehow survived and regained sanity. Remembering all the horrible things the scientist did to you and your fellow prisoners, you vow to bring death to the mad man.";
            mFadeIn = true;
            shown[3] = true;
        }

        if (mTime > 58 && !hidden[3])
        {
            mFadeOut = true;
            hidden[3] = true;
        }

        if (mTime > 61 && !shown[4])
        {
            storyText.text = "And so our story begins...";
            mFadeIn = true;
            shown[4] = true;
        }

        if (mTime > 63 && !hidden[4])
        {
            mFadeOut = true;
            hidden[4] = true;
        }

        if (mTime > 69 && hidden[4])
        {
            SceneManager.LoadScene("RealPrototype1");
        }

        anim.SetBool("FadeIn", mFadeIn);
        anim.SetBool("FadeOut", mFadeOut);
        mTime += Time.deltaTime;
	}

    public void EnableText()
    {
        storyText.enabled = true;
    }

    public void FadeInOff()
    {
        mFadeIn = false;
    }

    public void FadeOutOff()
    {
        mFadeOut = false;
        storyText.enabled = false;
    }
}
