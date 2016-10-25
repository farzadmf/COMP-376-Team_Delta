﻿using UnityEngine;
using System.Collections;

public class DamagePopUpController : MonoBehaviour {

    private static PopUpText popUpText;
    private static GameObject canvas;
    private static float minRandomValue = -.5f, maxRandomValue = .5f;
    private static int critFontsize = 30;
    private static Color critColor = new Color(255, 0, 0, 255);

    public static void Initialize()
    {
        canvas = GameObject.FindGameObjectWithTag("UIPopUp");

        if(!popUpText)
            popUpText = Resources.Load<PopUpText>("UI/DamageTextPopUp");

    }

    public static void CreateDamagePopUp(string text, Vector2 location, bool IsCrit)
    {
      
        PopUpText instance = Instantiate(popUpText);
       
        instance.transform.SetParent(canvas.transform, false);

        Vector2 position = new Vector2(Random.Range(-location.x, location.x) + Random.Range(minRandomValue, maxRandomValue), location.y + Random.Range(minRandomValue, maxRandomValue));

        instance.transform.position = location;
        instance.SetText(text);

        if (IsCrit)
        {
            instance.SetFontSize(critFontsize);
            instance.SetColor(critColor);
        }
    }

}