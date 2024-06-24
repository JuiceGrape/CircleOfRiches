using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] private Camera m_sceneCamera;
    public void EnableScene()
    {
        m_sceneCamera.gameObject.SetActive(true);
    }

    public void DisableScene()
    {
        m_sceneCamera.gameObject.SetActive(false);
    }
}
