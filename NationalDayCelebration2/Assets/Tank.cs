using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 注意生命周期结束时只会OutGame不会删除
/// </summary>
public class Tank : MonoBehaviour
{
    public string m_sTankName = "Default";
    [SerializeField] public int m_nLife = 10;//生命值    
    public System.Func<bool> GetFireButton;//开火按下时 简单设计，不制作额外控制器
    private BulletManager m_BulletManager;
    private System.Action<string> ShowText;//
    public bool isOutOfGame {
        get { return !gameObject.activeSelf; }
    }
    //外观相关属性
    public Color BarrelColor {
        get { return m_matBarrel.color; }
    }
    public Color BodyColor {
        get { return m_matBody.color; }
    }

    public string Killer { get; private set; }

    private Material m_matBody;
    private Material m_matBarrel;
    private bool isBulletHaveSetMat = false;

    [SerializeField] private Object m_prefabObjBoom;//爆炸效果预制物
    public event System.Action OnLifeTimeEnd;

    //private void Awake()
    //{
    //    m_BulletManager = GetComponent<BulletManager>();
    //}


    void Awake()
    {
        Debug.Assert(m_prefabObjBoom, "boom effect not ready.");
        if (tag == "Player") m_sTankName = G.PlayerTankName;
        m_BulletManager = GetComponent<BulletManager>();
        if (GetFireButton == null) GetFireButton = () => true;
        //if (GetFireButton == null) GetFireButton = () => Input.GetButton("Fire1")|| Input.GetButton("Fire2")|| Input.GetButton("Fire3");
        //if (GetFireButton == null) GetFireButton = () => Input.anyKeyDown;
        var shower = GetComponent<ShowObjectInfoInGamePlay>();
        ShowText = text => shower.text = text;
    }

    private void Start()
    {
        //if (tag == "Player")
        //{
        //    m_BulletManager.SetBulletSpeed(20f);
        //    m_BulletManager.SetFireInterval(0.2f);
        //    m_BulletManager.ResizeBulletList2(20);
        //    //if (m_matBarrel != null) m_BulletManager.setBulletMaterial(m_matBarrel);
        //}
        ShowText(m_sTankName);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetFireButton())
        {
            Shot();
        }
        ShowText(m_sTankName + m_nLife);
    }

    public void Shot()
    {
        m_BulletManager.Shot();
    }

    public void Boom(object sth)
    {
        playAnimationBoom();
        m_nLife--;
        if (m_nLife <= 0)
        {
            Bullet b = sth as Bullet;
            Debug.Assert(b != null);
            var killer = b.owner.GetComponent<Tank>().m_sTankName;
            Debug.Assert(killer != string.Empty);
            this.Killer = killer;
            if (OnLifeTimeEnd != null) OnLifeTimeEnd();
            OutOfGame();
        }
    }

    public void OutOfGame()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 响应被子弹碰撞消息
    /// </summary>
    private void OnHited(Bullet bullet)
    {
        if(!isFriendlyBullet(bullet))
        Boom(bullet);
    }

    /// <summary>
    /// 播放爆炸动画效果
    /// </summary>
    private void playAnimationBoom(System.Action onEffectEnd = null)
    {
        Instantiate(m_prefabObjBoom, transform.position, transform.rotation);
    }

    /// <summary>
    /// 首次使用时，会对子弹上色，耗费更多资源
    /// </summary>
    /// <param name="cor"></param>
    public void SetBarrelColor(Color cor)
    {
        if (m_matBarrel == null)
        {
            m_matBarrel = CreatStandardMaterial();
        }
        m_matBarrel.color = cor;
        if (isBulletHaveSetMat == false)
        {
            m_BulletManager.setBulletMaterial(m_matBarrel);
            isBulletHaveSetMat = true;
        }
    }

    public void SetBodyColor(Color cor)
    {
        if (m_matBody == null)
        {
            m_matBody = CreatStandardMaterial();
            var Render = GetComponent<Renderer>();
            Render.material = m_matBody;
        }
        m_matBody.color = cor;
    }

    private Material CreatStandardMaterial()
    {
        return new Material(Shader.Find("Standard"));
    }

    public void SetRandomColor()
    {
        SetBarrelColor(Random.ColorHSV());
        SetBodyColor(Random.ColorHSV());
    }

    private bool isFriendlyBullet(Bullet bullet)
    {
        if (tag == "Player" && bullet.owner.tag == "Player") return true;
        if (tag != "Player" && bullet.owner.tag != "Player") return true;
        return false;
    }
}
