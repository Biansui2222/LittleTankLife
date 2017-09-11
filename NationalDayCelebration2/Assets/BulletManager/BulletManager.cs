using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private List<Bullet> m_lstBullets;
    [SerializeField] private Bullet m_prefabBullet;
    public float m_fFireInterval = 0.5f;
    private float m_fNextFireTime = 0f;
    const float m_fBulletVelocity = 15f;
    const float m_fBulletLiftTime = 2f;
    const int m_iBulletLimit = 6;
    [SerializeField] private Transform m_FirePos;
    public GameObject Owner {
        get { return gameObject; }
    }
    void Start()
    {
        Debug.Assert(m_prefabBullet);
        Debug.Assert(m_FirePos);
        if (m_lstBullets == null) initLstBullet();
    }

    //void Update()
    //{
    //    Shot();
    //}

    private void OnDestroy()
    {
        foreach (var bullet in m_lstBullets) if (bullet) Destroy(bullet.gameObject);
    }

    public void Shot()
    {
        if (Time.time > m_fNextFireTime)
        {
            PopBullet();
            m_fNextFireTime = Time.time + m_fFireInterval;
        }
    }

    private void PopBullet()
    {
        var deadbullet = m_lstBullets.FindLast(bullet => bullet.isOutOfGame);
        if (deadbullet == null)
        {
            Debug.Log("BulletMan - haven't bullet");
            return;//已经达到子弹上限
        }
        deadbullet.EnterTheGame(m_fBulletLiftTime, m_FirePos);
    }

    //public void RecycleBullet(Bullet bullet)
    //{
    //    bullet.OutOfGame();
    //}

    public void setBulletMaterial(Material mat)
    {
        if (m_lstBullets == null) initLstBullet();
        foreach (var bullet in m_lstBullets)
        {
            bullet.setBulletMaterial(mat);
        }
    }

    private void initLstBullet()
    {
        m_lstBullets = new List<Bullet>(m_iBulletLimit);
        for (int i = 0; i < m_iBulletLimit; i++)
        {
            var bullet = CreatBullet();
            //bullet.transform.parent = transform;会导致主物体转向影响子弹位置
            //bullet.init(m_fBulletVelocity, () => { /*Debug.Log(bullet.name+":lifeEnd");*/  bullet.OutOfGame(); });
            //bullet.init(m_fBulletVelocity, () => RecycleBullet(bullet));
            m_lstBullets.Add(bullet);
        }
    }


    private Bullet CreatBullet()
    {
        var obj = Instantiate(m_prefabBullet.gameObject);
        Debug.Assert(obj, "init bulletlist fail because prefab error");
        var bullet = obj.GetComponent<Bullet>();
        bullet.init(m_fBulletVelocity, () => bullet.OutOfGame(), Owner);
        return bullet;
    }

    private Bullet CreatBulletExist()
    {
        Debug.Log("cbe");
        Debug.Assert(m_lstBullets[0] != null);
        var b = Instantiate(m_lstBullets[0].gameObject);
        var c = b.GetComponent<Bullet>();
        c.OutOfGame();
        return c;
    }

    public void SetBulletSpeed(float speed)
    {
        foreach (var b in m_lstBullets)
        {
            b.m_fVelocity = speed;
        }
    }

    public void SetFireInterval(float time)
    {
        m_fFireInterval = time;
    }

    [System.Obsolete]
    public void ReSizeBulletList(int some)
    {
        if (some < m_lstBullets.Count)
        {
            //TODO
        }
        else
        {
            for (int i = 0; i < m_lstBullets.Count - some; i++)
            {
                var b = CreatBullet();
                m_lstBullets.Add(b);
            }
        }
    }


    private void ClearBulletLst()
    {
        foreach(var b in m_lstBullets)
        {
            if (b != null) Destroy(b.gameObject);
        }
        m_lstBullets.Clear();
    }

    public void ResizeBulletList2(int some)
    {

        if (some < m_lstBullets.Count)
        {
            //TODO
            print("no");
        }
        else
        {
            print("do");
            var newlst = new List<Bullet>(some);
            for(int i = 0; i < some; i++)
            {
                newlst.Add(CreatBullet());
            }
            ClearBulletLst();
            m_lstBullets = newlst;


            //for (int i = 0; i < some- m_lstBullets.Count; i++)
            //{
            //    var b = CreatBulletExist();
            //    b.transform.parent = gameObject.transform;
            //    m_lstBullets.Add(b );
            //    print("Add");
            //}
        }

    }
}

