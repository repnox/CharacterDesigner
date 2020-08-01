using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Canvas))]
public class TransitionHandler : MonoBehaviour
{
    public bool showingByDefault;

    public TransitionHandler previousUI;

    public TransitionHandler nextUI;

    private readonly int IsShowing = Animator.StringToHash("IsShowing");

    private int transitionDelta;

    private void Start()
    {
        if (showingByDefault)
        {
            Show();
        }
        else
        {
            HideNow();
        }
    }

    public void ClickPrevious()
    {
        transitionDelta = -1;
        GetComponent<Animator>().SetBool(IsShowing, false);
    }

    public void ClickNext()
    {
        transitionDelta = 1;
        GetComponent<Animator>().SetBool(IsShowing, false);
    }

    public void Show()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().SetBool(IsShowing, true);
    }

    public void Hide()
    {
        GetComponent<Animator>().SetBool(IsShowing, false);
    }

    public void HideNow()
    {
        Hide();
        GetComponent<Canvas>().enabled = false;
    }

    // To be used by animator, when it's fully hidden
    public void _animationCallback()
    {
        HideNow();
        if (transitionDelta == 1)
        {
            nextUI.Show();
        } else if (transitionDelta == -1)
        {
            previousUI.Show();
        }

        transitionDelta = 0;
    }

}
