using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    // Reference to the chessman that is about to move
    GameObject reference = null;

    // Board position, not world position
    int matrixX;
    int matrixY;

    // false: move plate, true: attack plate
    public bool attack = false;

    public void Start(){
        if(attack){
            // Change color to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
    }

    public void OnMouseUp(){
        //print("Click on move plate");
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack){
            GameObject cp = controller.GetComponent<Game>().getPosition(matrixX, matrixY);

            if(cp.name == "white_king" || cp.name == "black_king")
            {
                controller.GetComponent<Game>().gameOverMethod();
            }

            Destroy(cp);
        }

        //empty the current position
        controller.GetComponent<Game>().setEmpty(
            reference.GetComponent<Chessman>().getXBoard(),
            reference.GetComponent<Chessman>().getYBoard()
        );


        //move the chessman to the new position
        reference.GetComponent<Chessman>().setXBoard(matrixX);
        reference.GetComponent<Chessman>().setYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        //controller keeps track of the position of the chessman
        controller.GetComponent<Game>().SetPosition(reference);

        //change turn
        controller.GetComponent<Game>().nextTurn();

        //destroy all moveplates
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y){
        matrixX = x;
        matrixY = y;
    }

    public void setRef(GameObject obj){
        reference = obj;
    }

    public GameObject getRef(){
        return reference;
    }
}
