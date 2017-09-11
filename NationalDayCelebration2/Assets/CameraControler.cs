using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
    private Transform m_Player;//初始化自动寻找的玩家位置
    private float m_fHigh = 30f;//镜头高度
    private Vector3 mc_vPos;//缓存变量

	// Use this for initialization
	void Start () {
        var go = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(go,"CameraControler can't find player.");
        m_Player =go .transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        mc_vPos.x = m_Player.position.x;
        mc_vPos.y = m_fHigh;
        mc_vPos.z = m_Player.position.z;
        transform.position = mc_vPos;
	}
}
