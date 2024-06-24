using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMIntro : PlayManagerState
{

    TextToSpeech speech = new TextToSpeech();

    public PMIntro(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {
        m_container.HandleEvent(PlayManagerEvent.IntroDone);
    }

    public override void Entry()
    {
        Debug.Log("Intro Entry");
        m_container.Actions.gameObject.SetActive(true);
        m_container.Text.gameObject.SetActive(true);
        speech.Speak("Lets give a warm welcome to our lovely contestants!");
        foreach(Player player in m_container.players)
        {
            speech.Speak(player.profile.Name);
        }
        speech.Speak("We hope they have fun and win big at:");
        speech.Speak("Circle! Of! Riches!");
        //m_container.Display.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        m_container.Actions.gameObject.SetActive(false);
        m_container.Text.gameObject.SetActive(false);
        //m_container.Display.gameObject.SetActive(false);
    }

    public override PlayManagerState HandleEvent(PlayManagerEvent PMEvent)
    {
        switch( PMEvent )
        {
            case PlayManagerEvent.IntroDone:
                return new PMDeciding(m_container);
        }

        return this;
    }
}
