using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTimer : MonoBehaviour
{
    [SerializeField] private Image m_img;
    private float m_ShowTimeLength = 3f;
    private float m_TimeTimerEnd = 0;
    private System.Action m_TimerDone;
    // Update is called once per frame
    void Update()
    {
        var TimeBetweenNowToEnd = m_TimeTimerEnd - Time.time; 
        if (TimeBetweenNowToEnd <= 0)
        {
            if (m_TimerDone != null) m_TimerDone();
        }
        else
        {
            var percent = TimeBetweenNowToEnd / m_ShowTimeLength;
            m_img.fillAmount = percent;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="second"></param>
    /// <param name="aniend">应有后续解除Timer显示的操作</param>
    public void setLastTime(float second, System.Action aniend)
    {
        m_ShowTimeLength = second;
        m_TimeTimerEnd = Time.time + m_ShowTimeLength;
        this.m_TimerDone = aniend;
    }

}
