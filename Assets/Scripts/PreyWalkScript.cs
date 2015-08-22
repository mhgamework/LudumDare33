using UnityEngine;
using System.Collections;

public class PreyWalkScript : MonoBehaviour
{

    public GameObject Path;

    public float PathProgression;
    public float WalkSpeed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isValidPathProgression(Path, PathProgression)) return;

        var startIndex = (int)Mathf.Floor(PathProgression);
        var endIndex = startIndex + 1;

        var startPos = Path.transform.GetChild(startIndex).transform.position;
        var endPos = Path.transform.GetChild(endIndex).transform.position;

        var length = (endPos - startPos).magnitude;

        PathProgression += WalkSpeed/length*Time.deltaTime;

        var prevPos = transform.position;
        transform.position = GetPositionFromPath(Path, PathProgression);

        transform.rotation = Quaternion.LookRotation(Vector3.Normalize(transform.position - prevPos), Vector3.up);




    }

    public Vector3 GetPositionFromPath(GameObject path, float offset)
    {
        var startIndex = (int)Mathf.Floor(PathProgression);
        var endIndex = startIndex + 1;
        var factor = offset - startIndex;

        if (startIndex < 0) return path.transform.GetChild(0).position;
        if (endIndex >= path.transform.childCount) return path.transform.GetChild(path.transform.childCount - 1).position;


        return Vector3.Lerp(path.transform.GetChild(startIndex).transform.position, path.transform.GetChild(endIndex).transform.position, factor);
    }

    public bool isValidPathProgression(GameObject path, float offset)
    {
        if (path.transform.childCount == 0) return false;
        if (path.transform.childCount == 1) return false;
        var startIndex = (int)Mathf.Floor(PathProgression);
        var endIndex = startIndex + 1;
        var factor = offset - startIndex;

        if (startIndex < 0) return false;
        if (endIndex >= path.transform.childCount) return false;

        return true;
    }
}
