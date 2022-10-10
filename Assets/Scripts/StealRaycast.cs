//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StealRaycast : MonoBehaviour
//{
//    [SerializeField]
//    private float rayDistance = 10.0f;

//    private Thief thief;

//    void Start()
//    {
//        thief = GetComponent<Thief>();
//    }

//    void Update()
//    {
//        RaycastHit raycastHit;
//        Ray rightRay = new Ray(transform.position, Vector3.right);
//        Ray leftRay = new Ray(transform.position, Vector3.left);
//        Ray forwardRay = new Ray(transform.position + new Vector3(0, 1.8f, 0), Vector3.forward);
//        if (Physics.Raycast(rightRay, out raycastHit, rayDistance) && Physics.Raycast(leftRay, out raycastHit, rayDistance))
//        {
//            if (raycastHit.collider.CompareTag("People"))
//            {
//                People hitObj = raycastHit.collider.GetComponent<People>();
//                hitObj.transform.parent = null;
//                hitObj.IsNervous = true;
//            }
//        }
//        if (Physics.Raycast(forwardRay, out raycastHit, rayDistance))
//        {
//            if (raycastHit.collider.CompareTag("People"))
//            {
//                raycastHit.collider.enabled = false;
//                thief.IsStealing = true;
//            }
//        }
//        if (Physics.Raycast(leftRay, out raycastHit, rayDistance))
//        {
//            if (raycastHit.collider.CompareTag("People"))
//            {
//                thief.IsLeft = true;
//                People hitObj = raycastHit.collider.GetComponent<People>();
//                hitObj.transform.parent = null;
//                hitObj.IsNervous = true;
//            }
//        }
//        if (Physics.Raycast(rightRay, out raycastHit, rayDistance))
//        {
//            if (raycastHit.collider.CompareTag("People"))
//            {
//                thief.IsRight = true;
//                People hitObj = raycastHit.collider.GetComponent<People>();
//                hitObj.transform.parent = null;
//                hitObj.IsNervous = true;
//            }
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Vector3 left = transform.TransformDirection(Vector3.left) * rayDistance;
//        Debug.DrawRay(transform.position + new Vector3(0, 0, 1), left, Color.red);

//        Vector3 right = transform.TransformDirection(Vector3.right) * rayDistance;
//        Debug.DrawRay(transform.position + new Vector3(0, 0, 1), right, Color.blue);

//        //Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
//        //Debug.DrawRay(transform.position + new Vector3(0, 1.8f, 1), forward, Color.magenta);
//    }
//}
