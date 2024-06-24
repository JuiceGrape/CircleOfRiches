using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJsonWheel : MonoBehaviour
{
    [SerializeField] public TextAsset jsonFile;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Wheel>().Initialize(JsonImporter.JsonToWheel(jsonFile.text).segments);
    }
}
