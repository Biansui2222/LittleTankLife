using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/// <summary>
/// 本类实现效果：两个摇杆控制物体基于屏幕方向移动旋转
/// </summary>
public class DoubleAxisControler : MonoBehaviour
{
    private const float m_forceFactory = 13f;
    public System.Func<float> GetAxis1Horizontal;
    public System.Func<float> GetAxis1Vertical;
    public System.Func<bool> GetAxis1Using;

    public System.Func<float> GetAxis2Horizontal;
    public System.Func<float> GetAxis2Vertical;
    public System.Func<bool> GetAxis2Using;

    private Rigidbody m_rigi;

    private void Awake()
    {
        m_rigi = GetComponent<Rigidbody>();
        GetAxis1Horizontal = () => Input.GetAxis("Horizontal");
        GetAxis1Vertical = () => Input.GetAxis("Vertical");
        GetAxis1Using = () => Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical")!=0;
        GetAxis2Horizontal = () => Input.GetAxis("Horizontal2");
        GetAxis2Vertical = () => Input.GetAxis("Vertical2");
        GetAxis2Using = () => Input.GetAxisRaw("Horizontal2") != 0 && Input.GetAxisRaw("Vertical2") != 0;
    }

    private void Update()
    {
        var maincamera = Camera.main.transform;
        var camerforward = Vector3.Scale(maincamera.forward, new Vector3(1, 0, 1)).normalized;
        var movedirection = GetAxis1Vertical() * camerforward + GetAxis1Horizontal() * maincamera.right;
        var viewDirection = GetAxis2Vertical() * camerforward + GetAxis2Horizontal() * maincamera.right;
        Move(movedirection);
        View(viewDirection);
    }

    private void Move(Vector3 v)
    {
        m_rigi.AddForce(v* m_forceFactory);
    }

    private void View(Vector3 v)
    {
        v = Vector3.ProjectOnPlane(v, transform.up);
        transform.LookAt(transform.position + v);
    }
}
