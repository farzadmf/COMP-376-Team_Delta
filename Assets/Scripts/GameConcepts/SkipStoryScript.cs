using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SkipStoryScript : MonoBehaviour {

    bool mFadeIn, mFadeOut;

    private Text storyText;
    private float mTime;
    private int index;
    private string[] story = { "Once there was a scientist. He was not well known and was looked down upon by those around him. The only field he excelled in was the experimentation on human beings.",
                                 "One day, in order to be recognized by others as a genius, he decided he would make the ultimate being. He started his experiments on animals but even that was not enough, he needed living human beings. So he set forth, and kidnapped humans to test on.",
                                 "The progress he made came at a price, each failed experiment would lose its mind and start attacking those around it. In order to get rid of them, he killed them and dumped their bodies anywhere he could, not realizing that they were actually still alive.",
                                 "After many failures, one experiment, you, showed promise and the scientist was overjoyed. However you too, lost your mind and so was \"killed\" by the scientist. With your body dumped in a cemetary left to rot, you somehow survived and regained sanity. Remembering all the horrible things the scientist did to you and your fellow prisoners, you vow to bring death to the mad man.",
                                 "And so our story begins..." };
    private bool[] shown = { false, false, false, false, false };
    private bool[] hidden = { false, false, false, false, false };
    private bool clicked;
    private Animator anim;

	// Use this for initialization
	void Start () {
        storyText = gameObject.GetComponentsInChildren<Text>()[0];
        storyText.enabled = false;
        mTime = 0.0f;
        index = 0;
        clicked = false;
        anim = storyText.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                clicked = true;
            }
            else
            {
                SceneManager.LoadScene("RealPrototype1");
            }
        }

        if (index < 5)
        {
            if (!hidden[index] && clicked && !mFadeIn)
            {
                mFadeOut = true;
                hidden[index] = true;
                mTime = 0.0f;
            }

            if (mTime > 0.3f && !shown[index])
            {
                storyText.text = story[index];
                mFadeIn = true;
                shown[index] = true;
            }
        }
        else
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
        clicked = false;
        index += 1;
    }
}
