using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public&Private Fields

    //Public:
    public int GridSize = 3;
    public bool ToTurn = false;

    public int TotalAnswer = 0;
    public int Lives = 1;

    public Material qColor;

    //Private:
    private bool isOdd_ = false;
    private int row = 0;
    private int colomn = 0;
    private float height_ = 30f;

    #region Time Management

    private float seconds = 0f;
    private float cTiles = 0f; //when to create tiles
    private bool t1 = false; //makin sure that it only happen once
    private float tMemorize = 0f; //when to memorize tiles
    private bool t2 = false;
    private float qAsk = 0f; //when to ask the question
    private bool t3 = false;

    [SerializeField] private float selectedTimer = 5f;

    private bool expl = false;
    private float gridCounter = 0f;
    private float TileCreatTrigger = 0.5f;

    #endregion

    //Win/Lose conditions
    private bool cdt1 = false;
    private bool cdt2 = false;

    //Final display
    private float seconds2 = 0f;
    private float theEnd = 3f;

    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject camera_;
    [SerializeField] private GameObject tileCreatExpl;
    [SerializeField] private Text qText;
    [SerializeField] private Text tLives;
    private GameObject protectedInfos;

    private string qColorString;

    #endregion
 
    #region Own Methods

    private void IsOdd(){
        //-> we do it inside a method because we will use it for a few other elements inside the game
        if (GridSize % 2 != 0){
            isOdd_ = true;
        }
        else isOdd_ = false;
    }

	private void GridInitialization(){
        //The first issue I came across was here. Because of the way I wanted to implement the grid creation, I had two options:
        // - if the player wants a grid with an ODD number, the algorithm is supposed to give even coordinates
        // - if the player wants a grid with an EVEN number, the algorithm is supposed to give, you guessed it, odd coordinates

        //Convert "GridSize" into coordinates
        int realGridSize = GridSize - 1;
        int reverseGrid = realGridSize*-1;

        row = reverseGrid;
        colomn = reverseGrid;

        for(int i = reverseGrid; i <= realGridSize; i++) {
            for(int j = reverseGrid; j <= realGridSize; j++) {
                colomn = j;

                //Printing Even coordinates if the Grid is Odd
                if (j % 2 == 0 && i % 2 == 0 && isOdd_) {
                    Instantiate(tile, new Vector3(row, 0, colomn), Quaternion.identity);
                }
                //Printing Odd coordinates if the Grid is Even
                else if (j % 2 != 0 && i % 2 != 0 && !isOdd_) {
                    Instantiate(tile, new Vector3(row, 0, colomn), Quaternion.identity);
                }
            } 
            row = i+1;
        }
    }

    private void CameraInitialization(){
        height_ += GridSize*3; 

        camera_.transform.position = new Vector3(0f, height_, 0f);

    }

    private void Ask(){
        var rand = UnityEngine.Random.Range(0, GridSize - 1);
        qColor = GetComponent<ColorsDB>().colorDBCopy[rand].material;

        switch (rand)
        {
            case 0:
                qColorString = "Red";
                break;
            case 1:
                qColorString = "Green";
                break;
            case 2:
                qColorString = "Light blue";
                break;
            case 3:
                qColorString = "Dark blue";
                break;
            case 4:
                qColorString = "Orange";
                break;
            case 5:
                qColorString = "Yellow";
                break;
        }

        qText.text = "Search for... " + qColorString;
    }

    private void YouWin(){
        qText.text = "You won!";
        cdt2 = true;
    }

    private void GameOver(){
        qText.text = "You lose!";
        cdt2 = true;
    }

    private void BackToMainMenu(){
        Destroy(protectedInfos);
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region Unity Methods

    void Awake(){
        //Getting main menu infos 
        protectedInfos = GameObject.Find("ProtectedInfos");
        var comp = protectedInfos.GetComponent<ProtectedInfos>();

        GridSize = comp._GridSize;
        selectedTimer = comp._TimeToMemorize;
        Lives = comp._Lives;

        //Testing if GridSize is an ODD number or not ->
        IsOdd();

        CameraInitialization();

        #region Timing Management 
        //Initializing the timings here, easier to get from the rest

        cTiles = 0.5f;
        tMemorize = cTiles + selectedTimer;
        qAsk = tMemorize + 0.2f;

        #endregion
    }
 
    void Update()
    {
        #region Timing Management 
        seconds += Time.deltaTime;

        //Tiles creation
        if (seconds >= cTiles && !t1) {
            if (!expl && gridCounter >= TileCreatTrigger/2) {
                Instantiate(tileCreatExpl, new Vector3(0, -10, 0), Quaternion.identity);
                expl = true;
            }

            gridCounter += Time.deltaTime;

            if(gridCounter >= TileCreatTrigger) {
                GridInitialization();
                t1 = true;
            }
        }
        //Stop memorize sequence
        if (seconds >= tMemorize && !t2) {
            ToTurn = true;
            if (seconds >= tMemorize + 0.25f) {
                ToTurn = false;
                t2 = true;
            }   
        }
        //Ask question
        if (seconds >= qAsk && !t3) {
            Ask();
            t3 = true;
        }
        if(seconds >= qAsk) {
            tLives.text = "Lives: " + Lives;
        }

        #endregion

        #region Win/Lose cdt + final timing

        //Win condition
        if(TotalAnswer >= GridSize && !cdt1) {
            YouWin();
            cdt1 = true;
        }

        //Lose condition
        if(Lives <= 0 && !cdt1) {
            GameOver();
            cdt1 = true;
        }

        if (cdt2) {
            seconds2 += Time.deltaTime;

            if (seconds2 >= theEnd) {
                BackToMainMenu();
            }
        }
        
        #endregion
    }

    #endregion
}