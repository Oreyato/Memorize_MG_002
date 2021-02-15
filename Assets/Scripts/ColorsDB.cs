using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ColorsDB : MonoBehaviour
{
    #region Public&Private Fields
    //Public:
	public List<Colors> colorDatabase = new List<Colors>();
    public List<Colors> colorDBCopy = new List<Colors>();

/* I didn't manage to access the material so I had to do it by hand, in the Inspector
    public List<Colors> test = new List<Colors>
    {
        new Colors("Red", 0, Resources.Load("Materials/Red", typeof(Material)) as Material)
    };
*/

    //Private:
	private int gridSize = 0;
    private int tileNb = 0;

    private GameObject cube;

    #endregion
 
    #region Own Methods
	private void ColorInitialization(int _gridSize){
        /*We need: 
            - the size of the grid to control how many colors there will be, and how many of each
            - then we will have to give these infos to the Cube script for him to give the right color
            - however, it won't be easy because we don't want something "avec remise" ~here comes the third difficult step!
        */

        if (_gridSize <= colorDatabase.Count){
            for (int i = 0; i < _gridSize; i++)
            {
                colorDatabase[i].number = _gridSize;
            }
        }
        else Debug.Log("This grid is too big!");

        colorDBCopy = colorDatabase;
        /*Testing
        foreach (var color in colorDBCopy)
        {
            Debug.Log(color.colorName + ": " + color.number);
        }
        */
    }

    #endregion

    #region Unity Methods
    void Awake(){
        //Here is the second difficult step! We need to
        //1- Access the grid size
        gridSize = GetComponent<GameManager>().GridSize;

        //2- Square the GridSize to have the number of tiles we will have in total
        tileNb = gridSize*gridSize;

        //3- Initialize the number of colors depending on the grid size while playing with the list that uses a 
        //  custom class, with a few parameters already implemented
        ColorInitialization(gridSize);

    }

    void Start()
    {
	
    }
 
    void Update()
    {
	
    }
    #endregion
}