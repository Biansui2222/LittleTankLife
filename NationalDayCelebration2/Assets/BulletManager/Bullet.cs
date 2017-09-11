using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigi;//外部序列化节约取组件时间
    public float m_fVelocity = 2f;//速度控制
    public float m_fDeadTime = 1f;//销毁时刻
    public System.Action OnLifeTimeEnd;//生命周期结束,可以使用Man监听回收子弹或者销毁
    public bool isOutOfGame {
        get {
            //print(gameObject);
            return !gameObject.activeSelf; }
    }
    public GameObject owner;

    [System.Diagnostics.Conditional("DEBUG")]
    private void Awake()
    {
        Debug.Assert(tag == "Bullet", "one bullet haven't set tag right");
        //Debug.Assert(m_BulletManager);
        Debug.Assert(m_rigi);
    }

    private void Start()
    {
        if (OnLifeTimeEnd == null) OnLifeTimeEnd = () => Destroy(gameObject);//生命周期结束时授予默认函数
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward,Color.blue);
        if (Time.time > m_fDeadTime)
        {
            if (OnLifeTimeEnd != null) OnLifeTimeEnd();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("OnHited", this, SendMessageOptions.DontRequireReceiver);
        if (OnLifeTimeEnd != null) OnLifeTimeEnd();
    }

    public void init(float Velocity,System.Action OnLifeTimeEnd,GameObject owner)
    {
        m_fVelocity = Velocity;
        this.OnLifeTimeEnd = OnLifeTimeEnd;
        this.owner = owner;
        OutOfGame();
    }

    /// <summary>
    /// 以参数所指位置调整自身位置
    /// </summary>
    /// <param name="tran"></param>
    public void Reposition(Transform tran)
    {
        transform.position = tran.position;
        transform.forward = tran.forward;
    }

    public void OutOfGame()
    {
        //Debug.Log(name + ":OutOfGame");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入游戏的子弹需要一些数据
    /// </summary>
    /// <param name="lifeTime">生命周期</param>
    /// <param name="position">位置-点</param>
    /// <param name="forward">位置-前</param>
    public void EnterTheGame(float lifeTime, Vector3 position, Vector3 forward)
    {
        //Debug.Log(name + ":EnterGame");
        Debug.Assert(isOutOfGame);
        transform.position = position;
        transform.forward = forward;
        m_rigi.velocity = transform.forward * m_fVelocity;
        m_fDeadTime = Time.time + lifeTime;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 进入游戏的子弹需要一些数据
    /// </summary>
    /// <param name="lifeTime">生命周期</param>
    /// <param name="tran">位置</param>
    public void EnterTheGame(float lifeTime, Transform tran)
    {
        EnterTheGame(lifeTime, tran.position, tran.forward);
    }


    public void setBulletMaterial(Material mat)
    {
            var renderer = GetComponent<Renderer>();
            renderer.material = mat;
    }
}
