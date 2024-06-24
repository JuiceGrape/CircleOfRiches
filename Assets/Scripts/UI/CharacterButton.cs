using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private Image m_buttonImage; //TODO: Use sprite swap buttons instead
    [SerializeField] private Button m_button;
    [SerializeField] public UnityEvent<char> m_OnButtonPressed;

    public char ButtonType
    {
        get;
        private set;
    }

    
    public void Initialize(Sprite sprite, char character)
    {
        if (m_text == null)
        {
            m_text = GetComponentInChildren<TextMeshProUGUI>();
            if (m_text == null)
            {
                Debug.LogError("No text object found in " + transform.name);
            }
        }

        if (m_buttonImage == null)
        {
            m_buttonImage = GetComponentInChildren<Image>();
            if (m_buttonImage == null)
            {
                Debug.LogError("No Image object found in " + transform.name);
            }
        }

        if (m_button == null)
        {
            m_button = GetComponentInChildren<Button>();
            if (m_button == null)
            {
                Debug.LogError("No button object found in " + transform.name);
            }
        }

        if (sprite != null)
        {
            m_buttonImage.sprite = sprite;
        }

        m_text.text = character.ToString();
        ButtonType = character;
        m_button.onClick.AddListener(OnClick);
    }

    public void EnableButton()
    {
        m_button.interactable = true;
    }

    public void DisableButton()
    {
        m_button.interactable = false;
    }

    private void OnClick()
    {
        m_OnButtonPressed.Invoke(ButtonType);
    }
}
