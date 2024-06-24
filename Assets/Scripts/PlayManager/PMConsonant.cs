using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PMConsonant : PlayManagerState
{
    public PMConsonant(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {

    }

    public override void Entry()
    {
        Debug.Log("Consonant Entry");
        m_container.Text.gameObject.SetActive(true);
        m_container.Text.EnableConsonants();
        m_container.GameboardScene.EnableScene();
        
    }

    public override void Exit()
    {
        m_container.Text.gameObject.SetActive(false);
    }

    public override PlayManagerState HandleEvent(PlayManagerEvent PMEvent)
    {
        switch (PMEvent)
        {
            case PlayManagerEvent.ChooseLetterDone:
                RevealConsonant();
                return new PMDeciding(m_container);
        }

        return this;
    }

    private void RevealConsonant()
    {
        char input = m_container.letterPressed;
        int foundLetterCount = m_container.Board.OpenLetter(input.ToString());
        m_container.Text.AddGuessedLetter(input);

        if (foundLetterCount == 0) 
        {
            m_container.IncrementTurn();
        }
        else
        {
            m_container.currentPlayer.roundCash.IncreaseValue(foundLetterCount * m_container.GameWheel.GetCurrentSegment().GetSegmentData().value);
        }
    }
}
