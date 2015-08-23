using UnityEngine;
using System.Collections;

public class CheckPointAnimator : MonoBehaviour
{
    private Transform TheTransform;

    [SerializeField]
    private float RotationTime = 2f;

    [SerializeField]
    private float BobTime = 5f;

    void Start()
    {
        TheTransform = GetComponent<Transform>();
        StartCoroutine("UpdateRotation");
        StartCoroutine("UpdateBobbing");
    }

    IEnumerator UpdateRotation()
    {
        float elapsed = 0;
        Vector3 ori_angles = TheTransform.eulerAngles;

        float from_angle = 0f;
        float to_angle = 180f;

        while (true)
        {
            while (elapsed < RotationTime)
            {
                from_angle = 0f;
                to_angle = 180f;

                var new_y_rot = EasingFunctions.Ease(EasingFunctions.TYPE.InOut, elapsed / RotationTime, from_angle, to_angle);
                TheTransform.eulerAngles = new Vector3(ori_angles.x, new_y_rot, ori_angles.z);

                elapsed += Time.deltaTime;
                yield return null;
            }
            elapsed = 0f;

            while (elapsed < RotationTime)
            {
                from_angle = 180f;
                to_angle = 360f;

                var new_y_rot = EasingFunctions.Ease(EasingFunctions.TYPE.InOut, elapsed / RotationTime, from_angle, to_angle);
                TheTransform.eulerAngles = new Vector3(ori_angles.x, new_y_rot, ori_angles.z);

                elapsed += Time.deltaTime;
                yield return null;
            }
            elapsed = 0f;
        }
    }

    IEnumerator UpdateBobbing()
    {
        float elapsed = 0;
        Vector3 ori_pos = TheTransform.localPosition;

        float from;
        float to;

        while (true)
        {
            while (elapsed < BobTime)
            {
                from = elapsed < 0.5f * BobTime ? 0f : 0.5f;
                to = elapsed < 0.5f * BobTime ? 0.5f : 0f;

                var new_y_pos = EasingFunctions.Ease(EasingFunctions.TYPE.InOut, elapsed / BobTime, from, to);

                TheTransform.localPosition = new Vector3(ori_pos.x, new_y_pos, ori_pos.z);

                elapsed += Time.deltaTime;
                yield return null;
            }
            elapsed = 0f;
        }
    }
}
