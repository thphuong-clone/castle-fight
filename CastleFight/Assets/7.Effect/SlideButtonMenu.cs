using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideButtonMenu : MonoBehaviour
{
    public Vector3 defaultAnchorPosition;
    public SlideMenuButton[] menuButton;
    public float slideTime;
    public float fadeTime;

    void Start()
    {
        foreach (SlideMenuButton b in menuButton)
        {
            b.button.gameObject.SetActive(false);
            b.button.anchoredPosition3D = defaultAnchorPosition;
        }
    }

    /// <summary>
    /// Set position of button when slide
    /// </summary>
    /// <param name="index">Index of button in button list</param>
    /// <param name="position">Position of button when slide</param>
    public void SetSlidePosition(int index, Vector3 position)
    {
        menuButton[index].position = position;
    }

    /// <summary>
    /// Show menu
    /// </summary>
    public void Show()
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                b.button.gameObject.SetActive(true);
            }
            foreach (SlideMenuButton b in menuButton)
            {
                //b.button.gameObject.SetActive(true);
                LeanTween.move(b.button, b.position, slideTime).setEase(LeanTweenType.easeOutQuad);
                LeanTween.alpha(b.button, 1, fadeTime);
                LeanTween.textAlpha(b.button, 1, fadeTime);
            }
        }
    }

    /// <summary>
    /// Show menu with ease type
    /// </summary>
    public void Show(LeanTweenType easeType)
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                b.button.gameObject.SetActive(true);
            }
            foreach (SlideMenuButton b in menuButton)
            {
                LeanTween.move(b.button, b.position, slideTime).setEase(easeType);
                LeanTween.alpha(b.button, 1, fadeTime);
                LeanTween.textAlpha(b.button, 1, fadeTime);
            }
        }
    }

    /// <summary>
    /// Show menu, ignore the time scale
    /// </summary>
    public void ShowIgnoreTimescale()
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                b.button.gameObject.SetActive(true);
            }
            foreach (SlideMenuButton b in menuButton)
            {
                //b.button.gameObject.SetActive(true);
                LeanTween.move(b.button, b.position, slideTime).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
                LeanTween.alpha(b.button, 1, fadeTime).setIgnoreTimeScale(true);
                LeanTween.textAlpha(b.button, 1, fadeTime).setIgnoreTimeScale(true);
            }
        }
    }

    /// <summary>
    /// Hide menu
    /// </summary>
    public void Hide()
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                LeanTween.move(b.button, defaultAnchorPosition, slideTime).setEase(LeanTweenType.easeOutQuad);
                LeanTween.alpha(b.button, 0, fadeTime);
                LeanTween.textAlpha(b.button, 0, fadeTime);
            }
            StartCoroutine("DisableButton");
        }
    }

    /// <summary>
    /// Hide menu with ease type
    /// </summary>
    public void Hide(LeanTweenType easeType)
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                LeanTween.move(b.button, defaultAnchorPosition, slideTime).setEase(easeType);
                LeanTween.alpha(b.button, 0, fadeTime);
                LeanTween.textAlpha(b.button, 0, fadeTime);
            }
            StartCoroutine("DisableButton");
        }
    }

    /// <summary>
    /// Hide menu, ignore the timescale
    /// </summary>
    public void HideIgnoreTimescale()
    {
        if (IsTweening())
            return;
        else
        {
            foreach (SlideMenuButton b in menuButton)
            {
                LeanTween.move(b.button, defaultAnchorPosition, slideTime).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
                LeanTween.alpha(b.button, 0, fadeTime).setIgnoreTimeScale(true);
                LeanTween.textAlpha(b.button, 0, fadeTime).setIgnoreTimeScale(true);
            }
            StartCoroutine("DisableButton");
        }
    }

    private bool IsTweening()
    {
        foreach (SlideMenuButton b in menuButton)
        {
            //Debug.Log(LeanTween.isTweening(b.button.gameObject));
            if (LeanTween.isTweening(b.button.gameObject))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator DisableButton()
    {
        while (IsTweening())
        {
            yield return null;
        }
        foreach (SlideMenuButton b in menuButton)
        {
            b.button.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForTweenComplete()
    {
        while (IsTweening())
        {
            yield return null;
        }
    }

    [Serializable]
    public class SlideMenuButton
    {
        public RectTransform button;
        public Vector3 position;
    }
}

