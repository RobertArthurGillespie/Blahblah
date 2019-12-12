using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpButtonPushed()
    {
        ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
    }
    public void DownButtonPushed()
    {
        ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 100);
    }
    public void LeftButtonPushed()
    {
        ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 100);
    }
    public void RightButtonPushed()
    {
        ball.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 100);
    }
}
