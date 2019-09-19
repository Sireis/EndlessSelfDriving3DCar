using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float RotationXCumulated = 0;
    float RotationYCumulated = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //float third = Input.GetAxisRaw("new");
        float rotate_x = Input.GetAxis("Mouse X");
        float rotate_y = Input.GetAxis("Mouse Y");

        RotationXCumulated += 2*rotate_x;
        //RotationXCumulated = RotationXCumulated % 360;

        RotationYCumulated -= 2*rotate_y;
        //RotationXCumulated = RotationYCumulated % 360;

        Vector3 translation = new Vector3(3*horizontal, 0, 3*vertical);
        translation = Quaternion.Euler(RotationYCumulated, RotationXCumulated, 0) * translation;

        transform.position += translation;
        transform.rotation = Quaternion.Euler(RotationYCumulated, RotationXCumulated, 0);

    }
}
