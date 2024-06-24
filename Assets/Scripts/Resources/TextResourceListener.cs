using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextResourceListener : MonoBehaviour
{
    public Resource resource;

    public bool AddsName = false;

    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        if (resource != null)
        {
            Init(resource);
        }
        
    }

    public void Init(Resource resource)
    {
        OnResourceChanged();
        resource.OnValueChanged.AddListener(OnResourceChanged);
    }
    
    void OnResourceChanged()
    {
        text.text = resource.GetValue().ToString();

        if (AddsName)
        {
            text.text += " " + resource.m_name;
        }
    }
}
