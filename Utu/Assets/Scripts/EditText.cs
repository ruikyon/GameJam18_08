using UnityEngine;
using UnityEngine.UI;

// 情報表示用の UI を制御するコンポーネント
public class EditText : MonoBehaviour
{
    public Text m_Score;

    private void Start()
    {
        m_Score.text = GameController.finalScore.ToString()+" pts";
    }
}