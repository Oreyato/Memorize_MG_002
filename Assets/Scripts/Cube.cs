using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Cube : MonoBehaviour
{
    #region Public&Private Fields
    //Public:
	
    //Private:
    private GameObject gameManager;
    private Colors colorToChoose;
    private Material tileColor;

    private bool isChosen = false;
    private int gridSize = 0;
    private int maxIterations = 0;

    //I- Up animation -------------------------------------------------------

    private float _x = 0;
    private float _z = 0;
    private float ascPower = 0;

    //O- Up animation -------------------------------------------------------

    //I- Turning 180° animation ---------------------------------------------

    private bool toTurn = false;
    private bool isTurned = false;
    private bool turnInitPlus = false;
    private bool turnInitMinus = false;
    private float almostOne = 0.999f;
    private float almostZero = 0.001f;
    private float counterTurn = 0f;
    private float turningCd = 0.01f;
    private float turningAround = 0f;

    //O- Turning 180° animation ---------------------------------------------

    #endregion
 
    #region Own Methods
	private void Material() {
        ColorsDB cCopy = gameManager.GetComponent<ColorsDB>();

        //Getting a random color and be sure that there are some left:
        #region Pick one random color
        //There are surely cleaner way to do it, but it works

        var rand = Random.Range(0, gridSize-1);
        colorToChoose = cCopy.colorDBCopy[rand];

        maxIterations = gridSize;

        while (!isChosen) {
            if (colorToChoose.number > 0) {
                //Setting it to the material of Tile's child (ColoredPart)
                GameObject child = transform.GetChild(0).gameObject;
                child.GetComponent<Renderer>().material = cCopy.colorDBCopy[rand].material;

                //Remove one
                cCopy.colorDBCopy[rand].number -= 1;
                isChosen = true;
            }
            else if (maxIterations > 0) {
                maxIterations -= 1;
                rand += 1;

                if (rand > gridSize-1) rand = 0;

                colorToChoose = cCopy.colorDBCopy[rand];
            }
            else if (maxIterations <= 0) {
                Debug.LogError("ERROR: There is no space left"); 
                break;
            }
        }

        #endregion
    }

    private void RotationPlus(){
        this.transform.Rotate(0f, 0f, turningAround);
        turningAround += 0.25f;
    }
    private void RotationMinus(){
        this.transform.Rotate(0f, 0f, turningAround);
        turningAround -= 0.25f;
    }

    private void Answer(){
        tileColor = colorToChoose.material;
        Material qColor = gameManager.GetComponent<GameManager>().qColor;

        if (qColor == tileColor){
            Debug.Log("Good answer!");
            gameManager.GetComponent<GameManager>().TotalAnswer += 1;
        }
        else {
            Debug.Log("Bad answer!");
            gameManager.GetComponent<GameManager>().Lives -= 1;
        }

    }

    #endregion

    #region Unity Methods
    private void Awake(){
        //Third difficult step! We now want to have access to the ColorDB script and its list, the ColorDB. 
        gameManager = GameObject.Find("GameManager");
        gridSize = gameManager.GetComponent<GameManager>().GridSize;
    }

    void Start()
    {
        Material();

        //Get position
        _x = this.transform.position.x;
        _z = this.transform.position.z;

        //When I will need to get the tiles Up, I'd like to control how much they go up relatively to their position in the grid
        //  the more offcentered they are, the less they will go up, so that all tiles appears to almost have the same size in the camera perspective
        ascPower = 8 / (Mathf.Sqrt((_x*_x)+1) + Mathf.Sqrt((_z*_z)+1));
    }
 
    void Update()
    {
        #region Rotation
        //I- TurningPlus animation ---------------------------------------

        toTurn = gameManager.GetComponent<GameManager>().ToTurn;

        if (toTurn && !isTurned) { //adding the ToTurn that is coming from the GameManager script
            turnInitPlus = true;
        }
        if (turnInitPlus) {
            if (this.transform.localRotation.z <= almostOne) {
                counterTurn += Time.deltaTime;
                if (counterTurn >= turningCd) {
                    RotationPlus();
                    counterTurn -= turningCd;
                }
            }
            else {
                toTurn = false;
                turnInitPlus = false;
                isTurned = true;

                turningAround = 0f;
                counterTurn = 0f;
            }
        }

        //O- TurningPlus animation ---------------------------------------

        //I- TurningMinus animation --------------------------------------

        if (toTurn && isTurned) {
            turnInitMinus = true;
        }
        if (turnInitMinus) {
            if (this.transform.localRotation.z >= almostZero) {
                counterTurn += Time.deltaTime;
                if (counterTurn >= turningCd) {
                    RotationMinus();
                    counterTurn -= turningCd;
                }
            }
            else {
                toTurn = false;
                turnInitMinus = false;
                isTurned = false;

                turningAround = 0f;
                counterTurn = 0f;
            }
        }

        //I- TurningMinus animation --------------------------------------
        #endregion

    }

    void OnMouseEnter(){
        if (isTurned) {
            this.transform.localPosition = new Vector3(_x, ascPower, _z);
        }
    }
    void OnMouseExit(){
        if (isTurned) {
            this.transform.localPosition = new Vector3(_x, 0, _z);
        }
    }
    void OnMouseDown() {
        turnInitMinus = true;

        Answer();
    }

    #endregion
}