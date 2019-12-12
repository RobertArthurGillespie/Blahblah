using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    public RaycastHit hit;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
        //Actively checks if the user is clicking (in the editor and on mobile)
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray ray;

            //Ensures that touch works in both the editor and when deployed to mobile
            if (Application.platform == RuntimePlatform.WindowsEditor)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            else
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            Debug.Log("Ray is starting at: " + ray.origin);
            if (Physics.Raycast(ray, out hit))
            {
                //ball.transform.Translate(ray.origin);
                //ball.GetComponent<Rigidbody>().AddForce(new Vector3(ray.origin.x,0f,0f)*100);
                Debug.Log("Raycast is successful!  Hit position is: " + hit.transform.position);
            }
        }
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right*100);
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left*100);
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*100);
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back*100);
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
}

