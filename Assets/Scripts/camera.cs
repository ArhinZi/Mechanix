using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public bool fix;
    public float SharpnessZoom;
    public float CameraPosition;
    //public int CameraZoomMax;
    //public int CameraZoomMin;
    public float CameraSpeed;
    //public float CameraDeltaSpeed;


    public Ray ray;
    public RaycastHit hit;


    void Start()
    {
        fix = true;
    }
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            fix = !fix;
        }
        if (!fix)
        {
            CameraHeightPosition();
            CameraWidthPosition();
        }
	}
    
    private void CameraHeightPosition()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CameraPosition += SharpnessZoom;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CameraPosition -= SharpnessZoom;
        }
        CameraSpeed = CameraPosition/100;
        if(CameraPosition<20) CameraPosition = 20;
        GetComponent<Camera>().orthographicSize = CameraPosition;
    }

    private void CameraWidthPosition()
    {
        if(20>Input.mousePosition.x)
        {
            transform.position -= new Vector3(CameraSpeed, 0, 0);
        }
        if ((Screen.width - 10) < Input.mousePosition.x)
        {
            transform.position += new Vector3(CameraSpeed, 0, 0);
        }
        if (20 > Input.mousePosition.y)
        {
            transform.position -= new Vector3(0,CameraSpeed,0);
        }
        if ((Screen.height - 10) < Input.mousePosition.y)
        {
            transform.position += new Vector3(0,CameraSpeed,0);
        }
    }

    public GameObject GetBody()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null && hit.transform.gameObject.GetComponent<BodyCtrl>()!=null)
        {
            return(hit.transform.gameObject);
        }
        else 
            return null;
    }
    public GameObject GetJoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null && hit.transform.gameObject.GetComponent<JointCtrl>()!=null)
        {
            return(hit.transform.gameObject);
        }
        else 
            return null;
    }

    
    public GameObject GetTrigger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null)
        {
            return(hit.transform.gameObject);
        }
        else 
            return null;
    }
}
