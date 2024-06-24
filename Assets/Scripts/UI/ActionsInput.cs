using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionsInput : MonoBehaviour
{
    public UnityEvent<PlayManagerEvent> OnButtonPressed;

    [SerializeField] private Button m_buttonSpin;
    [SerializeField] private Button m_buttonSolve;
    [SerializeField] private Button m_buttonBuy;

    // Start is called before the first frame update
    void Start()
    {
        m_buttonSpin.onClick.AddListener(OnSpinPress);
        m_buttonSolve.onClick.AddListener(OnSolvePress);
        m_buttonBuy.onClick.AddListener(OnBuyPress);
    }

    void OnSpinPress()
    {
        OnButtonPressed.Invoke(PlayManagerEvent.Spin);
    }

    void OnSolvePress()
    {
        OnButtonPressed.Invoke(PlayManagerEvent.Solve);
    }

    void OnBuyPress()
    {
        OnButtonPressed.Invoke(PlayManagerEvent.BuyVowel);
    }

    public void SetNormalMode()
    {
        m_buttonSpin.gameObject.SetActive(true);
        m_buttonBuy.gameObject.SetActive(true);
        m_buttonSolve.gameObject.SetActive(true);
    }

    public void SetSolveMode()
    {
        m_buttonSpin.gameObject.SetActive(false);
        m_buttonBuy.gameObject.SetActive(false);
        m_buttonSolve.gameObject.SetActive(true);
    }

    public void ToggleVowel(bool active)
    {
        m_buttonBuy.interactable = active;
    }
}
