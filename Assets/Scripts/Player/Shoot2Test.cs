using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot2Test : MonoBehaviour
{
    public Transform playerPosition;
    public Transform shootPosition;

    public GameObject shell;
    public float fireRate = 1f;
    public float nextRound = 0f;
    public AudioSource FireSound;
    public float zOffset;

    public Vector2 rotateVector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Below the mouseposition will be calculated in the world-position
        //獲取3D世界的 滑鼠位置 2DPerspective Camera
        var mousePos = Input.mousePosition;
        //大概是 將點的Z軸 矯正
        mousePos.z = -Camera.main.transform.position.z; 
        //從螢幕位置轉成世界位置
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);
        //把位置轉成方向
        //方向公式 A位置 減 B 位置 等於方向
        //normalized
        Vector3 dir = mousePosInWorld - this.transform.position;
        //把方向 sin cos 換成Z軸 
        //記住三角函數原則 Vector2 (Cos(),sin()) /=  Vector(y,x)
        //轉成角度
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        //套用角度
        transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
        //Debug.Log(angle);
        //RotateAim();
        */
       /* 
        if (Input.GetMouseButton(0) && Time.time > nextRound && GameDb.isWater)
        {
            //Debug.Log("On fire!");
            nextRound = Time.time + fireRate;
            Fire();
        }
        */
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time > nextRound && GameDb.isWater)
        {
            nextRound = Time.time + fireRate;
            Fire();
        }
    }
    
    public void RotateCenter(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            return;
        }
        rotateVector = context.ReadValue<Vector2>();
        Debug.Log("value = " + rotateVector);
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;
        
        //套用角度
        transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
    }

    void RotateAim()
    {
        Vector2 distanceVecter =
            (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)playerPosition.position;
        float angle = Mathf.Atan2(distanceVecter.y, distanceVecter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Fire()
    {
        GameDb.hp -= 5;
        GameObject fire = Instantiate(shell, shootPosition.position, shootPosition.rotation);
        fire.GetComponent<Rigidbody2D>().velocity = shootPosition.right * 15f;
        FireSound.Play();
    }
}
