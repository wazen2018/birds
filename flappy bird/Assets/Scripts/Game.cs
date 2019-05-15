using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    /// <summary>
    /// 游戏状态，对应三种panel 开始 游戏中 游戏结束
    /// </summary>
    public enum Game_STATUS
    {
        Ready,
        InGame,
        GameOver
    }

    public GameObject PanelReady;
    public GameObject PanelInGame;
    public GameObject PanelGameOver;

    public Player  player;

    public PipelineManager pipelineManager;

    private Game_STATUS status;

    public int score;
    public Text uiScore;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            uiScore.text = score.ToString();
        }

    }

    public Game_STATUS Status
    {
        get { return status; }
        set { status = value;
            UpdateUI();
        }

    }

    // Use this for initialization
    void Start () {
        this.PanelReady.SetActive(true);

        this.player.Ondeath  +=  Player_death;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void StartGame()
    {
        //UpdateGame();
        //Debug.LogFormat("Start Game:{0}", this.status);
        this.Status = Game_STATUS.InGame;
        
        pipelineManager.StartRun();
        Debug.LogFormat("Start Game:{0}", this.Status);

        player.Fly();
        player.onSorce = onPlayerSorce;
    }

     void onPlayerSorce(int score)
    {
        this.Score+= score;
    }
    private void Player_death()
    {
        this.Status = Game_STATUS.GameOver;
        this.pipelineManager.Stop();
    }

    private  void UpdateUI()
    {
        this.PanelReady.SetActive(this.Status == Game_STATUS.Ready);
        this.PanelInGame.SetActive(this.Status == Game_STATUS.InGame);
        this.PanelGameOver.SetActive(this.Status == Game_STATUS.GameOver);
             
        
    }

    public void Restart()
    {
        this.Status = Game_STATUS.Ready;
        pipelineManager.Init();
        player.Init();
        this.Score = 0;

    }
    
}
