using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float MIN_X, MIN_Y, MIN_Z;
    [SerializeField] private float MAX_X, MAX_Y, MAX_Z;

    public float scrollSpeed = 70f;
    public int padding = 5;

    // Update is called once per frame
    void Update()
    {
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        if (mousePositionX < padding)
        {
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
            //			Debug.Log("Moving LEFT: mousePositionX =" + Input.mousePosition.x);
            //			Debug.Log("Moving LEFT: Screen.width =" + Screen.width);
            //			Debug.Log("Moving Left getting called");
        }

        if (mousePositionX >= Screen.width - padding)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
            //			Debug.Log("Moving RIGHT: mousePositionX =" + Input.mousePosition.x);
        }

        if (mousePositionY < padding)
        {
            transform.Translate(transform.up * -scrollSpeed * Time.deltaTime);
            //			Debug.Log("Moving DOWN: mousePositionY =" + Input.mousePosition.y);
        }

        if (mousePositionY >= Screen.height - padding)
        {
            transform.Translate(transform.up * scrollSpeed * Time.deltaTime);
            //			Debug.Log("Moving UP: mousePositionY =" + Input.mousePosition.y);
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
                                Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
                                Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
                                Mathf.Clamp(transform.position.z, MIN_Z, MAX_Z));

    }
}
