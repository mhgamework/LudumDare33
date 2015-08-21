using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class CameraAnimation : MonoBehaviour
{
    // -- PUBLIC

    // .. TYPES

    [Serializable]
    public class CameraAnimationKeyframe
    {
        public Camera CameraPreview;
        public EasingFunctions.TYPE TransformEaseType = EasingFunctions.TYPE.Regular;
        public EasingFunctions.TYPE FieldOfViewEaseType = EasingFunctions.TYPE.Regular;
        public float TimeBetweenFrames;
    }

    public class CameraAnimationCompletedEvent : UnityEvent<CameraAnimation> { }
    public CameraAnimationCompletedEvent OnAnimationCompleted;

    // .. ACCESSORS

    public float GetTotalAnimationTime()
    {
        return KeyFrames.Sum(e => e.TimeBetweenFrames);
    }

    // .. OPERATIONS

    public void Play(Camera camera_to_animate)
    {
        StopCoroutine("UpdateAnimation");
        StartCoroutine("UpdateAnimation", camera_to_animate);
    }

    // -- PRIVATE

    // .. OPERATIONS

    private void GetCameraKeyFramesAtTime(float time, out CameraAnimationKeyframe previous_frame, out CameraAnimationKeyframe next_frame)
    {
        previous_frame = null;
        next_frame = null;

        if (time <= KeyFrames[0].TimeBetweenFrames)
        {
            previous_frame = new CameraAnimationKeyframe { CameraPreview = StartCameraPreview };
            next_frame = KeyFrames[0];
            return;
        }

        float elapsed = KeyFrames[0].TimeBetweenFrames;
        for (int i = 1; i < KeyFrames.Length; i++)
        {
            elapsed += KeyFrames[i].TimeBetweenFrames;
            if (time <= elapsed)
            {
                previous_frame = KeyFrames[i - 1];
                next_frame = KeyFrames[i];
                return;
            }
        }

        throw new Exception(string.Format("Given time ({0}) is out of the keyframe range (max {1})!", time, elapsed));
    }

    // .. COROUTINES

    IEnumerator UpdateAnimation(Camera camera_to_animate)
    {
        float elapsed = 0f;
        float total_time = KeyFrames.Sum(e => e.TimeBetweenFrames);

        Transform camera_transform = camera_to_animate.GetComponent<Transform>();

        while (elapsed < total_time)
        {
            float elapsed_frame = 0;
            CameraAnimationKeyframe from, to;
            GetCameraKeyFramesAtTime(elapsed, out from, out to);

            TransformValue from_transform = new TransformValue(from.CameraPreview == null ? camera_transform : from.CameraPreview.GetComponent<Transform>());
            TransformValue to_transform = new TransformValue(to.CameraPreview.GetComponent<Transform>());
            float from_fov = from.CameraPreview == null ? camera_to_animate.fieldOfView : from.CameraPreview.fieldOfView;
            float to_fov = to.CameraPreview.fieldOfView;

            while (elapsed_frame < to.TimeBetweenFrames)
            {
                float normalized_time = elapsed_frame / to.TimeBetweenFrames;

                //Transform
                EasingFunctions.Ease(to.TransformEaseType, normalized_time, from_transform, to_transform, ref camera_transform);
                //Field of View
                camera_to_animate.fieldOfView = EasingFunctions.Ease(to.FieldOfViewEaseType, normalized_time, from_fov, to_fov);

                elapsed += Time.deltaTime;
                elapsed_frame += Time.deltaTime;
                yield return null;
            }

            camera_transform.localPosition = to_transform.LocalPosition;
            camera_transform.localRotation = to_transform.LocalRotation;
            camera_transform.localScale = to_transform.LocalScale;
            camera_to_animate.fieldOfView = to_fov;
        }

        if (OnAnimationCompleted != null)
            OnAnimationCompleted.Invoke(this);
    }

    // .. ATTRIBUTES

    [Tooltip("Leave empty to use the current camera settings ('current' at start of the animation).")]
    [SerializeField]
    private Camera StartCameraPreview = null;
    
    [SerializeField]
    private CameraAnimationKeyframe[] KeyFrames = null;

}
