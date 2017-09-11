using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCreater : MonoBehaviour {
    [SerializeField]private GameObject SystemOBJ;
	// Use this for initialization
	void Awake () {
        Debug.Log("SC Awake");
        var sysobj = GameObject.FindWithTag("System");
        if (sysobj == null) InitSystem();
        Destroy(gameObject);
	}
	
    private void InitSystem()
    {
        Debug.Assert(SystemOBJ);
        var sys=Instantiate(SystemOBJ);
        DontDestroyOnLoad(sys);
    }
}
