using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMVowel : PlayManagerState
{
    public PMVowel(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {

    }

    public override void Entry()
    {
        Debug.Log("Vowel Entry");
        m_container.Text.gameObject.SetActive(true);
        m_container.Text.EnableVowels();
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
                PayVowel();
                return new PMDeciding(m_container);
        }

        return this;
    }

    private void PayVowel()
    {
        char input = m_container.letterPressed;
        int foundLetterCount = m_container.Board.OpenLetter(input.ToString());
        m_container.Text.AddGuessedLetter(input);

        m_container.currentPlayer.roundCash.DecreaseValue(PlayManager.VowelCost);//TODO: Global settings class

        if (foundLetterCount == 0)
        {
            m_container.IncrementTurn();
        }
    }
}
