using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    // Position on the board
    private int xBoard = -1;
    private int yBoard = -1;

    // Variables to keep track of "black" or "white" player
    private string player;

    // References for all the sprites that the chesspiece can be
    public Sprite black_queen, black_king, black_bishop, black_knight, black_rook, black_pawn;
    public Sprite white_queen, white_king, white_bishop, white_knight, white_rook, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //tale the instantiate location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                player = "black";
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                player = "black";
                break;
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                player = "white";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                player = "white";
                break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.31f;
        y += -2.31f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int getXBoard()
    {
        return xBoard;
    }
    public int getYBoard()
    {
        return yBoard;
    }
    public void setXBoard(int x)
    {
        xBoard = x;
    }
    public void setYBoard(int y)
    {
        yBoard = y;
    }
    // using box 2d collider
    private void OnMouseUp()
    {
        //print("Click on chessman");

        if (!controller.GetComponent<Game>().isGameOver() &&
            controller.GetComponent<Game>().getCurrentPlayer() == player)
        {
            //destroy all moveplates
            DestroyMovePlates();

            //initiate moveplates
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, -1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y)
        && sc.getPosition(x, y) == null)
        {
            CreateMovePlate(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y)
        && sc.getPosition(x, y).GetComponent<Chessman>().player != player)
        {
            CreateAttackPlate(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.getPosition(x, y);
            if (cp == null)
            {
                CreateMovePlate(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                CreateAttackPlate(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.getPosition(x, y) == null)
            {
                CreateMovePlate(x, y);
            }
            //attack on the left
            if (sc.PositionOnBoard(x + 1, y)
            && sc.getPosition(x + 1, y) != null
            && sc.getPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                CreateAttackPlate(x + 1, y);
            }
            // attack on the right
            if (sc.PositionOnBoard(x - 1, y)
            && sc.getPosition(x - 1, y) != null
            && sc.getPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                CreateAttackPlate(x - 1, y);
            }

        }
    }

    public void CreateMovePlate(int matrixX, int MatrixY)
    {
        CreateMP(matrixX, MatrixY, false);
    }

    public void CreateAttackPlate(int matrixX, int MatrixY)
    {
        CreateMP(matrixX, MatrixY, true);
    }

    public void CreateMP(int matrixX, int MatrixY, bool moveMode)
    {
        float x = matrixX;
        float y = MatrixY;

        //setup offset
        x *= 0.66f;
        y *= 0.66f;

        //setup position
        x += -2.31f;
        y += -2.31f;

        //create object instance
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = moveMode;
        mpScript.setRef(gameObject);
        mpScript.SetCoords(matrixX, MatrixY);
    }
}
