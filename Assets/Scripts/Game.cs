using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;
    //Position and team of the chesspiece
    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private string currentPlayer = "white";
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate( object, position, rotation ) - creating object 
        // through code instead of dragging and dropping in unity scene
        //Instantiate(chesspiece, new Vector3(0,0,-1), Quaternion.identity);
        init();
        
    }

    void init(){
        playerWhite = new GameObject[]{
            Create("white_rook", 0, 0),
            Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0),
            Create("white_queen", 3, 0),
            Create("white_king", 4, 0),
            Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0),
            Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1),
            Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1),
            Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1)
        };
        playerBlack = new GameObject[]{
            Create("black_rook", 0, 7),
            Create("black_knight", 1, 7),
            Create("black_bishop", 2, 7),
            Create("black_queen", 3, 7),
            Create("black_king", 4, 7),
            Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7),
            Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6),
            Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6),
            Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6),
            Create("black_pawn", 7, 6)
        };

        // set all piece positions on the position board
        for(int i=0; i < playerBlack.Length; i++){
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y){
        //create tmp object
        GameObject obj = Instantiate(chesspiece, new Vector3(x,y,-1), Quaternion.identity);

        //take out chessman component and adjust the name and position
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.setXBoard(x);
        cm.setYBoard(y);
        //assign controller to the chessman
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj){
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.getXBoard(), cm.getYBoard()] = obj;
    }

    public void setEmpty(int x, int y){
        positions[x,y] = null;
    }

    public GameObject getPosition(int x, int y){
        return positions[x,y];
    }

    public bool PositionOnBoard(int x, int y){
        return !(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1));
    } 

    public string getCurrentPlayer(){
        return currentPlayer;
    }

    public bool isGameOver(){
        return gameOver;
    }

    public void nextTurn(){
        if(currentPlayer.Equals("white")){
            currentPlayer = "black";
        }else{
            currentPlayer = "white";
        }
    }
    public void Update(){
        if(gameOver==true && Input.GetMouseButtonDown(0)){
            gameOver= false;
            SceneManager.LoadScene("Game");
            
        }
    }

    public void gameOverMethod(){
        gameOver = true;

        GameObject.Find("RestartTXT").GetComponent<Text>().enabled = true;
    }
}
