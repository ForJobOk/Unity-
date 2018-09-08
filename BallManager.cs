//学習内容:①OnCollisionEnter2D,OnTriggerEnter2D
//①それぞれ、他のオブジェクトとの接触時に呼び出される。引数で何と衝突した時か指定できる。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ボールがCollisionに衝突
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "OutArea")
        {
            //ゲームマネージャーを取得
            GameObject gameManager = GameObject.Find("GameManager");

            //リトライ
            gameManager.GetComponent<GameManager>().PushRetryButton();
        }
    }

    //ボールが何かのトリガーに衝突
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ClearArea")
        {
            //クリアエリアに突入
            GameObject gameManager = GameObject.Find("GameManager"); //GameManagerのゲームオブジェクトを取得し変数に記憶しておく
            gameManager.GetComponent<GameManager>().StageClear();  　//記憶したゲームオブジェクトの中からStageClearメソッドを取得

        }
    }
}
