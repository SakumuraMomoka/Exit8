using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float minX = 0;
    public float maxX = 56;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, -10f);//初期位置
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
    
        //カメラをプレイヤーに合わせて動かす
        pos = new Vector3(player.position.x, 0, -10f);//カメラをプレイヤーに追従
        pos.x = Mathf.Clamp(pos.x, minX, maxX);//カメラが画面外を写さないよう制限

        transform.position = pos;
    }
}
