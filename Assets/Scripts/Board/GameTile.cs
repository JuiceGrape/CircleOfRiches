using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTile : MonoBehaviour
{
    public enum TileState
    {
        closed,
        open,
        solving,
        revealed
    }

    [SerializeField] private TextMeshPro m_text;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private Sprite m_closedSprite;
    private Sprite m_openSprite;

    private string m_letter = "0";
    private string m_solveLetter = "";

    public TileState State { get; private set; }

    public float GetWidth()
    {
        return (transform as RectTransform).rect.width;
    }

    public float GetHeight()
    {
        return (transform as RectTransform).rect.height;
    }

    public void Initialize(Sprite closed, Sprite open)
    {
        if (m_text == null)
        {
            m_text = GetComponentInChildren<TextMeshPro>();
            if (m_text == null)
            {
                Debug.LogError("No text object found in " + transform.name);
            }
        }

        if (m_spriteRenderer == null)
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            if (m_spriteRenderer == null)
            {
                Debug.LogError("No SpriteRenderer object found in " + transform.name);
            }
        }

        m_closedSprite = closed;
        m_openSprite = open;
    }

    public bool IsLetter(string letter)
    {
        if (State == TileState.closed) return false;
        return letter.ToUpper().Equals(m_letter) || letter.ToLower().Equals(m_letter);
    }

    public void SetLetter(string letter)
    {
        m_text.enabled = false;
        m_text.SetText(letter);
        m_letter = letter;
        Open();
    }

    public void Reveal()
    {
        m_text.enabled = true;
        State = TileState.revealed;
    }

    public void Open()
    {
        m_spriteRenderer.sprite = m_openSprite;
        State = TileState.open;
    }

    public void Close()
    {
        m_text.enabled = false;
        m_spriteRenderer.sprite = m_closedSprite;
        State = TileState.closed;
    }

    public void Solve(string letter)
    {
        if (State == TileState.open)
        {
            m_solveLetter = letter;
            m_text.SetText(letter);
            m_text.enabled = true;
            State = TileState.solving;
        }
    }

    public void ResetSolve()
    {
        if (State == TileState.solving)
        {
            State = TileState.open;
            m_solveLetter = "";
            m_text.enabled = false;
            m_text.SetText(m_letter);
        }
    }

    public bool IsCorrect()
    {   // Every closed and revealed tile is correct, otherwise check if it matches the first solvable letter
        return State == TileState.closed 
            || State == TileState.revealed 
            || IsLetter(m_solveLetter);
    }
}
