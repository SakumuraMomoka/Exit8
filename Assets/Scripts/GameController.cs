using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int Exit_num = 0;
    int Ihen_num = 0;

    [SerializeField] Transform[] addTF;//広告のTransform、配列数５

    [SerializeField] Transform playerTF;
    [SerializeField] PlayerController player;

    [SerializeField] private SpriteRenderer sr;//Exitの看板のSpriteRenderer

    [SerializeField] private Sprite[] Exitsr;//Exitの看板のTransform、配列数１０

    [SerializeField] private SpriteRenderer Exit;//出口の階段のSpriteRenderer

    [SerializeField] CameraController cameraC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Exit.enabled = false;//出口のスプライトを非表示
    }

    // Update is called once per frame
    void Update()
    {
        //異変の生成 Ihen_num = 0~5のとき異変なし
        for (int i = 0; i < addTF.Length; i++)
        {
            addTF[i].rotation =
                (Ihen_num == i + 6)
                ? Quaternion.Euler(180, 0, 0)
                : Quaternion.identity;
        }

        //一周ごとのExit_numとIhen_numの管理
        bool hasIhen = Ihen_num >= 6;//異変があるときtrue

        if (!hasIhen)//異変がなかった時
        {
            if (player.reachedMax)
            {
                Exit_num ++;
                Ihen_num = Random.Range(0, 11);
                player.reachedMax = false;
            }
            else if (player.reachedMin)
            {
                Exit_num = 0;
                Ihen_num = Random.Range(0, 11);
                player.reachedMin = false;
            }
        }
        else//異変があったとき
        {
            if (player.reachedMin)
            {
                Exit_num++;
                Ihen_num = Random.Range(0, 11);
                player.reachedMin = false;
            }
            else if (player.reachedMax)
            {
                Exit_num = 0;
                Ihen_num = Random.Range(0, 11);
                player.reachedMax = false;
            }
        }

        //Exitnumの値によって、Exitのスプライトを対応するものに変更
        sr.sprite = Exitsr[Exit_num];
        //出口の表示
        if (Exit_num == 9)
        {
            Exit.enabled = true;
            player.maxX = 14.5f;
            cameraC.maxX = 0;

            if (playerTF.position.x <= 4 && playerTF.position.x >= 0.5f)
            {
                SceneManager.LoadScene("Clear");//クリアシーンへ遷移
            }
        }

    }
}
