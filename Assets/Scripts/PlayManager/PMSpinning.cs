using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMSpinning : PlayManagerState
{
    TextToSpeech speech = new TextToSpeech();
    public PMSpinning(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {

    }

    public override void Entry()
    {
        Debug.Log("Spinning Entry");
        m_container.GameboardScene.DisableScene();
        m_container.WheelScene.EnableScene();
        //TODO: DEBUG
        m_container.GameWheel.Spin(Random.Range(200.0f, 500.0f));
        //TODO: Enable spin UI
    }

    public override void Exit()
    {
        m_container.WheelScene.DisableScene();
    }

    public override PlayManagerState HandleEvent(PlayManagerEvent PMEvent)
    {
        switch (PMEvent)
        {
            case PlayManagerEvent.SpinDone:
                return new PMConsonant(m_container);
            case PlayManagerEvent.SpinFucked:
                m_container.IncrementTurn();
                return new PMDeciding(m_container);
        }

        return this;
    }
}