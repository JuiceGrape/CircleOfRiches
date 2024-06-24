using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextInput : MonoBehaviour
{
    [SerializeField] private float m_buttonWidth = 130;
    [SerializeField] private float m_buttonHeight = 130;

    [SerializeField] private CharacterButton m_buttonPrefab;

    [SerializeField] private GameBoard m_gameBoard;
    [SerializeField] public UnityEvent<char> OnButtonPressed;

    private List<CharacterButton> m_buttons;

    private List<char> m_guessedLetters = new List<char>();

    // Start is called before the first frame update
    void Start()
    {
        m_buttons = CreateInputField();
    }

    void OnButtonPress(char input)
    {
        OnButtonPressed.Invoke(input);
    }

    public void AddGuessedLetter(char letter)
    {
        m_guessedLetters.Add(letter);
    }

    public void EnableVowels()
    {
        foreach (var button in m_buttons)
        {
            if (m_guessedLetters.Contains(button.ButtonType))
            {
                button.DisableButton();
            }
            else if (IsVowel(button.ButtonType))
            {
                button.EnableButton();
            }
            else
            {
                button.DisableButton();
            }    
        }
    }

    public void EnableConsonants()
    {
        foreach (var button in m_buttons)
        {
            if (m_guessedLetters.Contains(button.ButtonType))
            {
                button.DisableButton();
            }
            else if (IsVowel(button.ButtonType))
            {
                button.DisableButton();
            }
            else
            {
                button.EnableButton();
            }
        }
    }

    public void EnableBoth()
    {
        foreach (var button in m_buttons)
        {
            if (m_guessedLetters.Contains(button.ButtonType))
            {
                button.DisableButton();
            }
            else
            {
                button.EnableButton();
            }
        }
    }

    public void ResetInput()
    {
        m_guessedLetters.Clear();
        EnableBoth();
    }

    static bool IsVowel(char c)
    {
        return "aeiouAEIOU".Contains(c);
    }

    List<CharacterButton> CreateInputField()
    {
        List<CharacterButton> retval = new List<CharacterButton>();
        int rowPos = 0;
        int columnPos = 0;
        int maxWidth = 14;
        int maxHeight = 2;

        float startXPos = -((m_buttonWidth * (float)(maxWidth - 1)) / 2.0f);
        float startYPos = (m_buttonHeight * (float)(maxHeight - 1)) / 2.0f;
        for (char target = 'A'; target <= 'Z'; target++)
        {
            float xpos = startXPos + (m_buttonWidth * rowPos);
            float ypos = startYPos - (m_buttonHeight * columnPos);

            CharacterButton temp = GameObject.Instantiate(
                m_buttonPrefab,
                this.transform) as CharacterButton;
            temp.transform.localPosition = new Vector2(xpos, ypos);
            temp.Initialize(null, target);
            temp.m_OnButtonPressed.AddListener(OnButtonPress);
            retval.Add(temp);
            rowPos++;

            if (rowPos == maxWidth)
            {
                rowPos = 0;
                columnPos++;
            }
        }

        return retval;
    }
}
