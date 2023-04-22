// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class flashBangBehaviour : MonoBehaviour
// {
//     public float distanceScale = 1f;
//     public float throwDistance = 3f;
//     public float throwHeight = 0.3f;
//     public float throwSpeed = 1f;
//     private Vector3 direction;
//     // Start is called before the first frame update
//     void Start()
//     {
//         Vector3 mousePos = Input.mousePosition;
//         mousePos.z = 10;
//         Vector3 mosuseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
//         Vector3 direction = mosuseWorldPos - transform.position;
//         direction.Normalize();

//         toss();
//     }

//     void toss() (
//         Vector3  target = transform.position + (direction * throwDistance);     
//         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * throwSpeed) {
//             float heightOffset = throwHeight*(2 * UnityEngine.Mathf.Pow(2,t*distanceScale-target.y) + UnityEngine.Mathf.Pow(2, target.y));
//             transform.position = Vector3.Lerp(trasnform.position, target, t) + new Vector3(0,heightOffset * 1-t, 0);
//         }
//     )

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
