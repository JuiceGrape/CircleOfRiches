using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayManagerEvent
{
    IntroDone,
    Spin,
    SpinDone,
    SpinFucked,
    BuyVowel,
    ChooseConsonant,
    ChooseLetterDone,
    Solve,
    SolveCorrect,
    SolveIncorrect
}
public abstract class PlayManagerState
{
    protected PlayManager m_container;
    public PlayManagerState(PlayManager container)
    {
        m_container = container;
    }

    public abstract void Entry();
    public abstract void Do();
    public abstract void Exit();

    public abstract PlayManagerState HandleEvent(PlayManagerEvent PMEvent);
}
