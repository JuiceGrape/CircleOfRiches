using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMSolving : PlayManagerState
{
    public PMSolving(PlayManager container) : base(container)
    {

    }

    public override void Do()
    {

    }

    public override void Entry()
    {
        Debug.Log("Solving Entry");
        m_container.Actions.gameObject.SetActive(true);
        m_container.Actions.SetSolveMode();
        m_container.Text.gameObject.SetActive(true);
        m_container.Text.EnableBoth();
        m_container.GameboardScene.EnableScene();

    }

    public override void Exit()
    {
        m_container.Actions.SetNormalMode();
        m_container.Text.gameObject.SetActive(false);
    }

    public override PlayManagerState HandleEvent(PlayManagerEvent PMEvent)
    {
        switch (PMEvent)
        {
            case PlayManagerEvent.ChooseLetterDone:
                m_container.Board.SolveLetter(m_container.letterPressed);
                break;
            case PlayManagerEvent.Solve:
                CheckSolve();
                break;
            case PlayManagerEvent.SolveCorrect:
                PayoutPlayer();
                m_container.IncrementTurn();
                ResetBoard();
                return new PMDeciding(m_container);
            case PlayManagerEvent.SolveIncorrect:
                m_container.Board.StopSolve();
                m_container.IncrementTurn();
                return new PMDeciding(m_container);
        }

        return this;
    }

    private void CheckSolve()
    {
        if (m_container.Board.IsBoardCorrect())
        {
            m_container.HandleEvent(PlayManagerEvent.SolveCorrect);
        }
        else
        {
            m_container.HandleEvent(PlayManagerEvent.SolveIncorrect);
        }
    }

    private void PayoutPlayer()
    {
        m_container.currentPlayer.gameCash.IncreaseValue(m_container.currentPlayer.roundCash.GetValue());
        foreach(Player player in m_container.players)
        {
            player.roundCash.ResetValue();
        }
    }

    private void ResetBoard()
    {
        m_container.Text.ResetInput();
        m_container.Board.ResetBoard();
        m_container.SetRandomSolution();
    }
}
