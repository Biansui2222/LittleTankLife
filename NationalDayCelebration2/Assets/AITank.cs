using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : MonoBehaviour {
    private Tank _Tank;
    public Tank Tank { get { if (_Tank == null) _Tank = GetComponent<Tank>(); return _Tank; } }
    private DoubleAxisControler m_MoveCtr;
    private Tank m_PlayerTank;
    private Transform m_PlayerTransform;
    private NavMeshAgent m_NavMeshAgent;
    public bool isOutOfGame {
        get {return Tank.isOutOfGame; }
    }
	// Use this for initialization
	void Awake () {
        //m_Tank = GetComponent<Tank>();
        m_MoveCtr = GetComponent<DoubleAxisControler>();
        m_PlayerTank = GameObject.FindGameObjectWithTag("Player").GetComponent<Tank>();
        m_PlayerTransform = m_PlayerTank.transform;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
        m_NavMeshAgent.SetDestination(m_PlayerTransform.position);
        Tank.Shot();
	}
}
