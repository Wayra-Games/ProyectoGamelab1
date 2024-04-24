using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveCameraHorizontal(float posX)
    {
        mainCamera.transform.position = new Vector3(posX, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
