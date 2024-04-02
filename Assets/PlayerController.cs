using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed, xLimits;
    [SerializeField] private BulletPoolerController bulletPooler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if(Input.GetKeyDown(KeyCode.Space)) ShootBullet();
    }

    private void MoveHorizontal()
    {
        float xPosition = transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        xPosition = Mathf.Clamp(xPosition, -xLimits, xLimits);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }

    private void ShootBullet() 
    {
        Debug.Log(transform.position);
        //Instantiate(bulletPrefab, transform.position, transform.rotation); 
    }
}
