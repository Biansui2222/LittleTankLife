using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField]private Text m_TankName;
    [SerializeField] private Text m_Title;

    private void Start()
    {
        var ed1 = G.edCount[0];
        var ed2 = G.edCount[1];
        var ed3 = G.edCount[2];
        if (!ed1 && !ed2 && !ed3) {
            m_Title.text= "人生小作战ヾ(≧▽≦*)o";
        }
        else if(ed1 && !ed2 && !ed3)
        {
            m_Title.text = "餐具人生的再崛起(。・∀・)ノ";
        }
        else if( ed2 && !ed3)
        {
            m_Title.text = "咸鱼人生的再翻身(ง •_•)ง";
        }
        else if( ed3)
        {
            m_Title.text = "帝王人生的再游戏ψ(•∀• )>";
        }
    }

    public void OnSingleModePressed()
    {
        if (m_TankName.text != string.Empty)
        {
            G.PlayerTankName = m_TankName.text;
        }
        SceneManager.LoadScene(1);
    }
}
