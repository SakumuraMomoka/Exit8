using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 11.0f;
    public SpriteRenderer sr;
    public float move;
    public float maxX = 70.5f;
    public float minX = -14.5f;
    Animator animator;

    //playerが画面のどちら側の端に行ったかを判断、Gamecontrollerで使う
    public bool reachedMax;//右端
    public bool reachedMin;//左端

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //プレーヤーを横方向に移動
        move = Input.GetAxisRaw("Horizontal");
        pos += new Vector3(move * speed * Time.deltaTime ,0 ,0);

        //移動の無限ループ //左右端を越えたら反対側へワープ
        if (pos.x < minX)
        {
            pos = new Vector3(maxX, pos.y, 0);
            reachedMin = true;
        }
        else if (pos.x > maxX)
        {
            pos = new Vector3(minX, pos.y, 0);
            reachedMax = true;
        }

        transform.position = pos;

        //アニメーションの制御
        animator.SetBool("isWalk", move != 0);//横方向の入力があるとき、isWalk=true

        //入力の方向に応じてスプライトの向きを変える
        if (move != 0)
        {
            sr.flipX = move < 0;
        }
    }
}
