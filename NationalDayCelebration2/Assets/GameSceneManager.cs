using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {
    private UIManager m_uiman;
    private AreaManager m_area;
    [SerializeField]private AITank m_prefabAI;
    private List<AITank> m_lstAITanks;
    private int progress = 0;
	// Use this for initialization
	void Awake () {
        Debug.Log("GSM Awake");
        m_uiman = UIManager.Instance;
        m_area = GetComponent<AreaManager>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (progress)
        {
            case -1://GameOver
                break;
            case 0:
                m_uiman.ShowTimerDestroyEnd(60, edTimerEnd);
                m_uiman.ShowTipsAndBreath("生存60秒！");
                var player = GameObject.FindWithTag("Player");
                var tank = player.GetComponent<Tank>();
                tank.OnLifeTimeEnd += edGameOver;
                progress++;
                break;
            case 1:
                //TODO:Show "First Wave"
                Debug.Assert(m_prefabAI);
                m_lstAITanks = new List<AITank>(5);
                StartCoroutine(CreatAITankRandom());
                progress++;
                break;
            case 2:
                break;
            case 3:
                //ai all have creat 
                break;
        }
	}

    private IEnumerator CreatAITankRandom()
    {
        for (int i = 0; i < 5; i++)
        {
            var ai = Instantiate(m_prefabAI);
        ai.Tank.SetRandomColor();
        ai.Tank.m_sTankName = Util.RandomIn("碰瓷","台风","停电","感冒","KTV","玄学");
        ai.Tank.OnLifeTimeEnd += WhenAITankDead;//TODO
        var randpos = m_area.GetRandomPosInArea();
        ai.transform.position = randpos;
        m_lstAITanks.Add(ai);
        yield return new WaitForSeconds(1);
        }
        progress++;
    }

    private void edGameOver()
    {
        if (progress == -1) return;
        progress = -1;
        m_uiman.ShowCenterText("面对无穷无尽的敌人，你终于跪了！");
        m_uiman.ShowReturnMainButton();
        G.edCount[0] = true;
    }

    private void WhenAITankDead()
    {
        //print("AI " + ai.name + "is Dead.")
       if( !m_lstAITanks.Exists(t => !t.isOutOfGame))
        {
            edKillAllEnermy();
        }
    }

    private void edTimerEnd()
    {
        if (progress == -1) return;
        progress = -1;
        m_uiman.ShowCenterText("你生存下来了！");
        m_uiman.ShowReturnMainButton();
        G.edCount[1] = true;
    }

    private void edKillAllEnermy()
    {
        if (progress == -1) return;
        progress = -1;
        m_uiman.ShowCenterText("你是人生赢家！你打败了所有敌人！");
        m_uiman.ShowReturnMainButton();
        G.edCount[2] = true;
    }
}
