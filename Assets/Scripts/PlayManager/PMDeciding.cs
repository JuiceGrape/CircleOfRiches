using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMDeciding : PlayManagerState
{
    public PMDeciding(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {

    }

    public override void Entry()
    {
        Debug.Log("Deciding Entry");
        m_container.GameboardScene.EnableScene();
        m_container.Actions.gameObject.SetActive(true);
        m_container.Actions.ToggleVowel(m_container.currentPlayer.roundCash.GetValue() >= PlayManager.VowelCost);
    }

    public override void Exit()
    {
        m_container.Actions.gameObject.SetActive(false);
    }

    public override PlayManagerState HandleEvent(PlayManagerEvent PMEvent)
    {
        switch (PMEvent)
        {
            case PlayManagerEvent.Spin:
                return new PMSpinning(m_container);
            case PlayManagerEvent.BuyVowel:
                return new PMVowel(m_container);
            case PlayManagerEvent.Solve:
                return new PMSolving(m_container);
        }

        return this;
    }
}
