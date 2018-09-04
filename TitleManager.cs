using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour {
    


	// Use this for initialization
	void Start () {
		
	}
	
    public void PushStartButton()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
