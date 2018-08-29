using UnityEngine;
using UnityEngine.UI;

// 情報表示用の UI を制御するコンポーネント
public class Hud : MonoBehaviour
{
    public static Hud instance;
    public Text m_Score;
    public Text m_Wave;
    public GameObject m_headShot;
    private int counter=0;

    private void Start()
    {
        instance = this;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        m_Score.text = GameController.instance.GetScore().ToString();
        m_Wave.text = Enemy.instance.GetWave().ToString();

        if(counter > 0)
        {
            counter--;
            if(counter == 0) m_headShot.SetActive(false);
        }
    }

    public void HeadShot()
    {
        m_headShot.SetActive(true);
        counter = 200;
    } 
}