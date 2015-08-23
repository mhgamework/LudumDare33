using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreyWalkScript : MonoBehaviour
{

    public WaypointPathScript Path;

    public float PathProgression;
    public float WalkSpeed = 1;
    public float PianoInterval = 3;
    public float PianoDuration = 2;

    private WaypointNodeScript lastVisitedNode;
    [SerializeField] private AudioSource huh;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WalkPath().GetEnumerator());
    }

    IEnumerable<YieldInstruction> WalkPath()
    {

        var timeLeft = PianoInterval * 100;
        while (timeLeft > 0)
        {

            var startPos = GetFromPoint();
            var endPos = GetTargetPoint();

            var length = (endPos - startPos).magnitude;


            var currNode = GetFromNode();
            var targetNode = GetToNode();


            if (currNode != lastVisitedNode)
            {
                lastVisitedNode = currNode;
                if (currNode.LookBack)
                {
                    StartCoroutine(DoPiano().GetEnumerator());
                    yield break;

                }
            }

            var realMoveSpeed = WalkSpeed * currNode.WalkModifier;



            PathProgression += realMoveSpeed * GetFromNode().WalkModifier / length * Time.deltaTime;
            PathProgression = Mathf.Clamp(PathProgression, 0, Path.transform.childCount - 1);
            updateOrientationUsingPathProgression();
            yield return null;
            timeLeft -= Time.deltaTime;
        }

    }

    private IEnumerable<YieldInstruction> DoPiano()
    {
        huh.Play();
        yield return new WaitForSeconds(0.64f);
        var lookDir = GetFromPoint() - GetTargetPoint();

        var timeLeft = PianoDuration;
        while (timeLeft > 0)
        {
            var numLaps = 2;
            var rotationAngle = timeLeft / PianoDuration; // 0..1
            rotationAngle *= numLaps; // 0..numLaps
            rotationAngle %= 1; // 0..1 * numLaps
            rotationAngle = Mathf.Abs(rotationAngle * 2 - 1);
            rotationAngle = rotationAngle * 2 - 1;
            rotationAngle *= 30;

            //((timeLeft / PianoDuration * 2) - 1) * 30;
            transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up) * Quaternion.AngleAxis(rotationAngle, Vector3.up);
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        StartCoroutine(WalkPath().GetEnumerator());
    }

    private WaypointNodeScript GetFromNode()
    {
        return GetNodeSafe(Path, (int)Mathf.Floor(PathProgression));
    }

    WaypointNodeScript GetToNode()
    {
        return GetNodeSafe(Path, (int)Mathf.Floor(PathProgression) + 1);
    }

    Vector3 GetFromPoint()
    {
        return GetPointOnPathSafe(Path, Mathf.Floor(PathProgression));
    }

    Vector3 GetTargetPoint()
    {
        return GetPointOnPathSafe(Path, Mathf.Floor(PathProgression + 1));
    }

    Vector3 GetPointOnPathSafe(WaypointPathScript path, float offset)
    {
        if (path.transform.childCount == 0) return path.transform.position;
        if (path.transform.childCount == 1) return path.transform.GetChild(0).position;
        var startIndex = (int)Mathf.Floor(offset);
        var endIndex = startIndex + 1;
        var factor = offset - startIndex;

        if (startIndex < 0) return path.transform.GetChild(0).position;
        if (endIndex >= path.transform.childCount) return path.transform.GetChild(path.transform.childCount - 1).position;

        return Vector3.Lerp(path.transform.GetChild(startIndex).transform.position, path.transform.GetChild(endIndex).transform.position, factor);
    }

    WaypointNodeScript GetNodeSafe(WaypointPathScript path, int node)
    {
        if (path.transform.childCount == 0) return null;
        if (path.transform.childCount == 1) return path.transform.GetChild(0).GetComponent<WaypointNodeScript>();
        var startIndex = Mathf.Clamp((int)Mathf.Floor(node), 0, path.transform.childCount - 1);

        return path.transform.GetChild(startIndex).GetComponent<WaypointNodeScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void updateOrientationUsingPathProgression()
    {
        var prevPos = transform.position;
        transform.position = GetPointOnPathSafe(Path, PathProgression);

        transform.rotation = Quaternion.LookRotation(Vector3.Normalize(transform.position - prevPos), Vector3.up);
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
