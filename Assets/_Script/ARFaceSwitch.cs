using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitch : MonoBehaviour
{
    ARFaceManager arFaceManager;


    public Material[] materials;

    private int switchCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        // GetComponent를 이용하면 동일한 게임오브젝트내에 있는 다른 컴포넌트에 접근해서 그 컴포넌트를 가져올 수 있다.
        arFaceManager = GetComponent<ARFaceManager>();
        arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = materials[0]; 
    }

    void SwitchFaces()
    {
        // AR Face Manager에는 AR Face를 추적하는 기능이 들어가 있는데 거기에서 추적 가능한 모든 얼굴들을 포문을 통해서 이 루프안을 돌게하는 것
        foreach (ARFace face in arFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[switchCount];
        }

        switchCount = (switchCount + 1) % materials.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SwitchFaces();
        }
    }
}
