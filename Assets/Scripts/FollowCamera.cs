using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    private Quaternion initialRotation;
    private Vec3Filter cameraArmFilter;
    private FloatFilter rotationFilter;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = target.transform.rotation;
        cameraArmFilter = new Vec3Filter(1);
        rotationFilter = new FloatFilter(3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        Quaternion totalRotation = initialRotation * target.transform.rotation;
        Vector3 cameraArm = cameraArmFilter.GetFilteredValue(3 * Vector3.up + totalRotation * (4 * Vector3.back));
        float angle = rotationFilter.GetFilteredValue(target.transform.rotation.eulerAngles.y);

        transform.position = target.transform.position + cameraArm;
        transform.rotation = initialRotation * Quaternion.Euler(0,angle,0);
    }
}
