using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreyWalkScript : MonoBehaviour
{
    [SerializeField]
    private Animator EthanAnimator = null;

    [SerializeField]
    private Cloth ClothComponent = null;

    public WaypointPathScript Path;

    public float PathProgression;
    public float WalkSpeed = 1;
    public float PianoInterval = 3;
    public float PianoDuration = 2;

    private WaypointNodeScript lastVisitedNode;
    [SerializeField]
    private AudioSource huh;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(WalkPath().GetEnumerator());

        GameStateManagerScript.Get.PauseEvent.AddListener(() => { StopAllCoroutines(); });
        GameStateManagerScript.Get.UnPauseEvent.AddListener(() =>
        {
            {
                if (EthanAnimator != null)
                {
                    EthanAnimator.Play("HumanoidWalk");
                }
                StartCoroutine(WalkPath().GetEnumerator());
            }
        });
    }

    void Update()
    {
        if (Mathf.FloorToInt(PathProgression + 1) - Path.transform.childCount >= 0)
        {
            StopAllCoroutines();
            if (EthanAnimator != null)
            {
                EthanAnimator.Play("HumanoidIdle");
            }
        }
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
        if (EthanAnimator != null)
        {
            EthanAnimator.Play("HumanoidIdle");
        }

        huh.Play();
        yield return new WaitForSeconds(0.64f);


        var lookDir = GetFromPoint() - GetTargetPoint();

        //rotate backwards
        var rotation_time = 1f;
        StartCoroutine("UpdateRotation", new object[] { Quaternion.LookRotation(lookDir, Vector3.up), rotation_time });
        yield return new WaitForSeconds(rotation_time);

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

        //rotate to path rotation again
        StartCoroutine("UpdateRotation", new object[] { Quaternion.LookRotation(GetTargetPoint() - GetFromPoint(), Vector3.up), rotation_time });
        yield return new WaitForSeconds(rotation_time);

        if (EthanAnimator != null)
        {
            EthanAnimator.Play("HumanoidWalk");
        }

        StartCoroutine(WalkPath().GetEnumerator());
    }

    private IEnumerator UpdateRotation(object[] args)
    {
        Quaternion targetRot = (Quaternion)args[0];
        float rot_time = (float)args[1];

        var prey_transform = GetComponent<Transform>();
        var oriRot = prey_transform.rotation;

        var elapsed = 0f;
        while (elapsed < rot_time)
        {
            prey_transform.rotation = EasingFunctions.Ease(EasingFunctions.TYPE.Regular, elapsed / rot_time, oriRot, targetRot);
            elapsed += Time.deltaTime;
            yield return null;
        }

        prey_transform.rotation = targetRot;
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

    private void updateOrientationUsingPathProgression()
    {
        var prevPos = transform.position;
        var prevRot = transform.rotation;

        var newPos = GetPointOnPathSafe(Path, PathProgression);
        
        var newRot = Quaternion.LookRotation(Vector3.Normalize(newPos - prevPos), Vector3.up);

        if (Vector3.Distance(prevPos, newPos) > 0.5f || Quaternion.Angle(prevRot, newRot) > 20f)
            TeleportPrey(newPos, newRot);
        else
        {
            transform.position = newPos;
            transform.rotation = newRot;
        }
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

    public void TeleportPrey(Vector3 new_position, Quaternion new_rotation)
    {
        if (ClothComponent != null)
            ClothComponent.ClearTransformMotion();
        transform.position = new_position;
        transform.rotation = new_rotation;
    }
}
