using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public GameObject player;
    Transform targetTransform;
    Transform transform;
    public float speed;

    void Start()
    {
        targetTransform = player.transform;
        transform = this.transform;
    }

        // Update is called once per frame
        void Update () {
        Vector3 vectorToTarget = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

    }
}
