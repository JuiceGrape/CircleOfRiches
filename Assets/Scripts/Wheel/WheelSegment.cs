using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;

public class WheelSegment : MonoBehaviour
{
    const string prefix = "<rotate=90>";

    public enum SegmentType
    {
        value,
        bankrupt,
        freeplay,
        loseTurn,
        express,
        gamble
    }

    [System.Serializable]
    public struct SegmentData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SegmentType type;
        public int value;
        public string image;
    }


    [SerializeField] private SegmentData m_segmentData;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public void Initialize(SegmentData data, Dictionary<string, Sprite> texturePack)
    {
        if (m_spriteRenderer == null)
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            if (m_spriteRenderer == null)
            {
                Debug.LogError("No SpriteRenderer object found in " + transform.name);
            }
        }
        
        if (texturePack.ContainsKey(data.image))
        {
            m_spriteRenderer.sprite = texturePack[data.image];
        }
        else
        {
            m_spriteRenderer.sprite = texturePack["Default"];
        }
        m_segmentData = data;

    }

    public SegmentData GetSegmentData()
    {
        return m_segmentData;
    }

    public static string GetStringFromSegmentData(SegmentData data)
    {
        switch (data.type)
        {
            case SegmentType.value:
                return data.value.ToString();
            case SegmentType.bankrupt:
                return "Bankrupt";
            case SegmentType.freeplay:
                return "Free Play";
            case SegmentType.loseTurn:
                return "Lose Turn";
            case SegmentType.express:
                return "Express";
            case SegmentType.gamble:
                return "?" + data.value.ToString();
        }
        return "Unsupported";
    }
}
