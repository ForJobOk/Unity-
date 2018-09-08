//学習内容： ①プレハブ__②isKinematic(重力)__③tag__④PlayerPrefs__⑤Invoke

//①ゲームオブジェクトをアセットとして登録し、複製を簡単に作れるようにする仕組みのこと
//②isKinematicで物理エンジンの影響の有効、無効を切り替えれる。
//③gameObject.tag == "タグ名"でゲームオブジェクトにタグを設定することができる。種類を区別したいときに使う。
//④情報を記憶する仕組み。キーを設定して、値を書き込み(保存)、値を読み出し(保存したものの取り出し？)を行うことができる。
//⑤Invoke("メソッド名", 秒数f)＿＿第一引数で指定したメソッドを第二引数で指定した秒数後に呼び出す。

//Gravityメソッドは本の説明のUNITYとバージョンが違ったので自分で考えて作った。初めて自分で人のコードを改変したかもしれない。ちゃんと動いて感動した。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int StageNo; 　　　　　　　　//ステージナンバー
    public bool isBallMoving; 　　　　　//ボール移動中か否か
    public GameObject ballPrefab; 　　　//ボールプレハブ
    public GameObject ball; 　　　　　　//ボールオブジェクト

    public GameObject goButton; 　　　　//ボタン：ゲーム開始
    public GameObject retryButton;     //ボタン：リトライ
    public GameObject clearText;       //テキスト:クリア

    public AudioClip clearSE;          //効果音:クリア
    private AudioSource audioSource;    //オーディオソース

    // Use this for initialization
    void Start() {
        Rigidbody2D rd = ball.GetComponent<Rigidbody2D>();
        rd.isKinematic = true;
        retryButton.SetActive(false);  //リトライボタンを非表示
        isBallMoving = false;          //ボールは「移動中ではない」

        //オーディオソースを取得
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }
    public void PushGoButton()
    {
        Gravity(false);       //重力を有効化

        retryButton.SetActive(true); //リトライボタンを表示
        goButton.SetActive(false);　 //Goボタンを非表示
        isBallMoving = true;　　　　 //ボールは「移動中」
    }
    //リトライボタンを押す
    public void PushRetryButton()
    {
        Destroy(ball);             　//ボールオブジェクトを削除

        //プレハブから新しいボールオブジェクト作成
        ball = (GameObject)Instantiate(ballPrefab);  //Instantiateは引数に指定したオリジナルのオブジェクトのクローンを返す
                                                     //変数に代入するときは(GameObject)をつけてキャストする

        Gravity(true); 　　　　　　　　//重力を無効化

        retryButton.SetActive(false);　//リトライボタンを非表示
        goButton.SetActive(true);　　　//GOボタンを表示
        isBallMoving = false;　　　　　//ボールは「移動中ではない」
    }


    //戻るボタン
    public void PushBackButton()
    {
        GobackStageSelect(); 
    }


    //ボールの重力を有効(false)、無効(true)に切り替える
    void Gravity(bool gravity)
    {
        Rigidbody2D rd = ball.GetComponent<Rigidbody2D>();
        rd.isKinematic = (gravity);  //isKinematicは物理エンジンを有効、無効に切り替える(bool型で有効はfalse)
    }

    //ステージクリア処理
    public void StageClear()
    {
        audioSource.PlayOneShot(clearSE); //クリア音再生

        //セーブデータ更新
        if(PlayerPrefs.GetInt("CLEAR",0)< StageNo)
        {
            //セーブされているステージナンバーより今のステージナンバーの方が大きければ
            PlayerPrefs.SetInt("CLEAR", StageNo); //ステージナンバーを記録
        }

        clearText.SetActive(true);        //クリア表示
        retryButton.SetActive(false);     //リトライボタン非表示


        //3秒後に自動的にステージセレクト画面へ
        Invoke("GobackStageSelect", 3.0f);
    }
    //移動処理
    void GobackStageSelect()  //Invokeで指定する為にメソッド作成
    {
        SceneManager.LoadScene("StageselectScene");
    }
}
