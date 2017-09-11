using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {
    //private Transform m_root;
    [SerializeField]private Transform[] m_EnermyBornPos;

    public Vector3 GetRandomPosInArea()
    {
        return m_EnermyBornPos[Random.Range(0,m_EnermyBornPos.Length)].position;
    }
}
