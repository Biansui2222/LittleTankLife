using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[DisallowMultipleComponent]
public class UIManager : MonoBehaviour//务必设置好行动级别s
{
    private static UIManager m_single;
    public static UIManager Instance {
        get {
            Debug.Assert(m_single);
            return m_single;
        }
    }

    private Canvas m_Canvas {
        get {
            if (_m_Canvas == null) _m_Canvas = FindMainCanvas();
            return _m_Canvas;
        }
    }
    private Canvas _m_Canvas;
    private Text m_TextUI {
        get {
            if (_m_TextUI == null) _m_TextUI = FindPrefabText();
            return _m_TextUI;
        }
    }//prefab
    [SerializeField] private Text _m_TextUI;
    private Button m_ButtonUI {
        get {
            if (_m_ButtonUI == null) _m_ButtonUI = FindPrefabButton();
            return _m_ButtonUI;
        }
    }
    [SerializeField] private Button _m_ButtonUI;
    [SerializeField] private uiTimer _m_TimerUI;
    private uiTimer m_TimerUI {
        get {
            if (_m_TimerUI == null) _m_TimerUI = FindPrefabTimerUI();
            return _m_TimerUI;
        }
    }

    //private Button m_ButtonUI;//prefab
    public event System.Action m_Update;

    private void Awake()
    {
        Debug.Log("UIM Awake");
        //如果已经存在此组件，则取消加载
        var objs = FindObjectsOfType<UIManager>();
        if (objs.Length > 1)
        {
            Destroy(this);
            //throw new System.Exception("too many UIManager");
        }
        m_single = this;
        m_single._m_Canvas = FindMainCanvas();
    }


    private void Update()
    {
        if (m_Update != null) m_Update();
    }

    //public static void init()
    //{
    //    m_single = FindObjectOfType<UIManager>();
    //    //m_single.m_mono = mono;
    //    m_single._m_Canvas = FindMainCanvas();
    //    //m_single._m_TextUI = FindPrefabText();
    //}

    private static Canvas FindMainCanvas()
    {
        var canvasobj = GameObject.Find("Canvas");
        Debug.Assert(canvasobj);
        var canvas = canvasobj.GetComponent<Canvas>();
        return canvas;
    }

    private static Text FindPrefabText()
    {
        //TODO:从预制物中寻找
        var tran = m_single.m_Canvas.transform.Find("Text");
        Debug.Assert(tran);
        return tran.GetComponent<Text>();
    }

    private static Button FindPrefabButton()
    {
        //TODO
        var tran = m_single.m_Canvas.transform.Find("Button");
        Debug.Assert(tran);
        return tran.GetComponent<Button>();
    }

    private static uiTimer FindPrefabTimerUI()
    {
        var ui = GameObject.Find("uiTimer");
        Debug.Assert(ui);
        var script = ui.GetComponent<uiTimer>();
        return script;
    }

    public void ShowCenterText(string text, float time = 99)
    {
        var comp = Instantiate(m_TextUI, m_Canvas.transform);
        comp.text = text;
        Destroy(comp.gameObject, time);
    }

    public void ShowTipsAndUpdate(string text, System.Action<Text> update)
    {
        var textcomp = Instantiate(m_TextUI, m_Canvas.transform);
        textcomp.text = text;
        System.Action fUpdate = null;
        fUpdate = () =>
        {
            if (textcomp != null) update(textcomp);
            else
            {
                m_Update -= fUpdate;
            }
        };
        m_Update += fUpdate;
    }

    public void ShowTipsAndBreath(string text)
    {
        System.Action fUpdateanim = null;
        ShowTipsAndUpdate(text, component =>
        {
            if (fUpdateanim == null)
            {
                fUpdateanim = AnimationLib.Breath(
                    b => component.gameObject.SetActive(b),
                    () => Object.Destroy(component.gameObject));
            }
            fUpdateanim();
        });
    }

    public void ShowReturnMainButton()
    {
        var btn = Instantiate(m_ButtonUI, m_Canvas.transform);
        var textobj = btn.transform.Find("Text");
        var text = textobj.GetComponent<Text>();
        text.text = "返回菜单";
        UnityEngine.Events.UnityAction fReturnMain = () => SceneManager.LoadScene(0);
        btn.onClick.AddListener(fReturnMain);
    }


    public void ShowTimerDestroyEnd(float time, System.Action TimerEnd)
    {
        var timer = Instantiate(m_TimerUI,m_Canvas.transform);
        timer.setLastTime(time, () =>
        {
            Destroy(timer.gameObject);
            if(TimerEnd!=null)TimerEnd();
        }
        );
    }
}
