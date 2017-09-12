using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStick2 : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //private Vector3 m_OrginalPosition;
    public enum JoyState { up,down}
    private JoyState m_JoyState=JoyState.up;
    public JoyState State { get { return m_JoyState; } }
    private Vector3 m_StartPos;
    [SerializeField]private float m_AxisRange=1;//在此范围内Axis为-1~1，范围外取满值
    [SerializeField]private Vector2 m_Axis=Vector2.zero;
    public Vector2 Axis {
        get { return m_Axis; }
    }

    void Start () {
        m_StartPos = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
        m_Axis = CalcVirtualAxes(transform.position);
    }

    /// <summary>
    /// 通过原始位置到现位置偏移得出轴值，轴域为正方形
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector2 CalcVirtualAxes(Vector3 pos)
    {
        var delta = m_StartPos - pos;
        delta.x = Mathf.Clamp(delta.x, -m_AxisRange, m_AxisRange);
        delta.y = Mathf.Clamp(delta.y, -m_AxisRange, m_AxisRange);
        delta /= m_AxisRange;
        //delta.Normalize();
        return -delta;
    }



    void IPointerUpHandler.OnPointerUp(PointerEventData data)
    {
        m_JoyState = JoyState.up;
        transform.position = m_StartPos;
        m_Axis = Vector2.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_JoyState = JoyState.down;
    }
}
