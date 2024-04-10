using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceInfoPanel : MonoBehaviour
{
    public static FaceInfoPanel Instance;

    [SerializeField] private Text facePosText;
    [SerializeField] private Text faceRotText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFacePositionText(Vector3 position)
    {
        facePosText.text = $"Face Position : {position.x}, {position.y}, {position.z}";
    }

    public void SetFaceRotationText(Vector3 rotation)
    {
        facePosText.text = $"Face Position : {rotation.x}, {rotation.y}, {rotation.z}";
    }
}
