using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathSquirrel : MonoBehaviour
{
    public PathCreator path;
    public GameObject 松鼠;
    public float 位置;
    public EndOfPathInstruction 線段結尾事件;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        位置 += Time.deltaTime;

        松鼠.transform.position = path.path.GetPointAtDistance(位置,線段結尾事件); 
        //松鼠.transform.rotation = path.path.GetRotationAtDistance(位置,線段結尾事件);
        //Vector2 a = new Vector2(Mathf.Cos(松鼠.transform.eulerAngles.x),Mathf.Sin(松鼠.transform.eulerAngles.y));
        //float b = a.normalized;
        //松鼠.transform.eulerAngles = new Vector3(0,0,a); 
    }
}
