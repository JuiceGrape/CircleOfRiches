using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public struct WheelStructure
{
    public string name;
    public WheelSegment.SegmentData[] segments;
}

[System.Serializable]
public struct QuestionPackage
{
    public string category;
    public string[] questions;
}

public class JsonImporter
{
    public static WheelStructure JsonToWheel(string json)
    {
        return JsonConvert.DeserializeObject<WheelStructure>(json);
    }

    public static QuestionPackage JsonToQuestion(string json)
    {
        return JsonConvert.DeserializeObject<QuestionPackage>(json);
    }

}
