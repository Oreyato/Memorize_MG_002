using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class ProtectedInfos : MonoBehaviour
{
    #region Public&Private Fields
    //Public:
    public int _GridSize = 3;
    public int _TimeToMemorize = 5;
    public int _Lives = 1;

    //Private:
    [SerializeField] private Dropdown gridsize;
    [SerializeField] private Dropdown timeToMemorize;
    [SerializeField] private Dropdown lives;

    #endregion

    #region Own Methods
    
    public void PlayGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void GridSize(){
        _GridSize = gridsize.value + 2;
        Debug.Log(_GridSize);
    }

    public void TimeToMemorize(){
        if (timeToMemorize.value == 1){
            _TimeToMemorize = 5;
        } else if (timeToMemorize.value == 2){
            _TimeToMemorize = 10;
        }else if (timeToMemorize.value == 3){
            _TimeToMemorize = 15;
        } else _TimeToMemorize = 30;
    }

    public void Lives(){
        if (lives.value == 1){
            _Lives = 1;
        } else if (lives.value == 2){
            _Lives = 3;
        } else _Lives = 5;
    }

    public void Quit(){
        Application.Quit();
    }

    //Unity
    public void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start(){

    }

    #endregion
}