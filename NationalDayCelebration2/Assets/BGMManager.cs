using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour {
    public AudioSource m_prefabBGM;
    private AudioSource m_BGM;
	// Use this for initialization
	void Start () {
        CreatBGM();
        SceneManager2.onSceneGoingToLoad += OnSceneGoingtoLoad;
        //SceneManager.sceneUnloaded += OnSceneUnLoad;
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }

    void CreatBGM()
    {
        Debug.Assert(m_prefabBGM);
        Debug.Assert(m_BGM == null);
        m_BGM = Instantiate(m_prefabBGM);
        SetBGMtoCamera();
    }

    private void SetBGMtoCamera()
    {
        var cam = Camera.main.transform;
        m_BGM.transform.parent = cam;
        m_BGM.transform.localPosition = Vector3.zero;
    }
    private void SetBGMFree()
    {
        m_BGM.transform.parent = null;        
    }

    private void OnSceneWasLoaded(Scene scene,LoadSceneMode mode)
    {
        if (m_BGM != null) SetBGMtoCamera();
    }

    private void OnSceneGoingtoLoad()
    {
        SetBGMFree();
        DontDestroyOnLoad(m_BGM);
    }
}
