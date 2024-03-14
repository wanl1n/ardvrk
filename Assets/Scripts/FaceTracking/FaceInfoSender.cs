using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceInfoSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FaceInfoPanel.Instance != null)
        {
            FaceInfoPanel.Instance.SetFacePositionText(GetComponent<ARFace>().pose.position);
            //FaceInfoPanel.Instance.SetFaceRotationText(GetComponent<ARFace>().pose.rotation);
        }
    }
}
