using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelSegment m_wheelSegmentPrefab;
    

    [SerializeField] public UnityEvent<WheelSegment> OnSpinComplete;
    

    private List<WheelSegment> m_segments;
    private int m_segmentCount;

    private float m_spinSpeed = 0.0f;

    [SerializeField] private Dictionary<string, Sprite> m_texturePack;

    public void SetTexturePack(Dictionary<string, Sprite> texturePack)
    {
        m_texturePack = texturePack;
        //TODO: Change texture pack
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        //TODO: DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Spin(Random.Range(200.0f, 500.0f));
        }   
        
        if (m_spinSpeed > 0.0f)
        {
            transform.Rotate(0, 0, -m_spinSpeed * Time.deltaTime);
            m_spinSpeed -= (m_spinSpeed / 2) * Time.deltaTime;

            if (m_spinSpeed < 5.0f)
            {
                m_spinSpeed = 0.0f;
                OnSpinComplete.Invoke(GetCurrentSegment());
            }
        }
    }

    public void Spin(float force)
    {
        m_spinSpeed = force;
    }

    public bool IsSpinning()
    {
        return false; //TODO
    }

    public WheelSegment GetCurrentSegment()
    {
        float rotationPerSegment = 360.0f / (float)m_segmentCount;
        float segmentIndex = transform.rotation.eulerAngles.z / rotationPerSegment;
        int segmentIndexFixed = Mathf.RoundToInt(segmentIndex);
        if (segmentIndexFixed >= m_segmentCount)
        {
            segmentIndexFixed = 0;
        }
        Debug.Log(segmentIndexFixed);
        var segment = m_segments[segmentIndexFixed];
        return segment;
    }

    public void Initialize(WheelSegment.SegmentData[] segmentData)
    {
        m_segments = CreateWheel(segmentData, m_wheelSegmentPrefab);
        m_segmentCount = m_segments.Count;
    }

    private List<WheelSegment> CreateWheel(WheelSegment.SegmentData[] segmentData, WheelSegment prefab)
    {
        List<WheelSegment> retVal = new List<WheelSegment>();
        int count = segmentData.Length;
        float rotationPerSegment = 360.0f / (float)count;
        for(int segmentIndex = 0; segmentIndex < count; segmentIndex++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 180 - (rotationPerSegment * segmentIndex));
            string segmentName = "WheelSegment " + segmentIndex;
            WheelSegment segment = GameObject.Instantiate(
                prefab,
                transform.position,
                rotation,
                this.transform) as WheelSegment;
            segment.transform.name = segmentName;
            segment.Initialize(segmentData[segmentIndex], m_texturePack);
            retVal.Add(segment);
        }

        return retVal;
    }

}
