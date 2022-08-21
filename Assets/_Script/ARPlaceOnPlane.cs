using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{

    public ARRaycastManager arRaycaster;
    public GameObject placeObject;

    GameObject spawnObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObjectByTouch();
        //UpdateCenterObject();
    }

    // 평면 인식된 공간을 터치하여 오브젝트를 위치시킴
    private void PlaceObjectByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                if (!spawnObject)
                {
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }

    // 화면의 센터부분에 오브젝트를 매 프레임마다 위치
    private void UpdateCenterObject()
    {
        // ViewportToScreenPoint 포인트를 뷰좌표에서 스크린 좌표로 변(카메라 스크린의 센터지점을 받아오는 함수)
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placeObject.SetActive(false);
        }
    }
}
