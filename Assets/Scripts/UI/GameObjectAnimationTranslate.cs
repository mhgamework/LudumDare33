using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Dragdrop.DragDropItem
{
    public class GameObjectAnimationTranslate 
        : MonoBehaviour
    {
        public void StartAnimation(
            EasingFunctions.TYPE ease_type,
            Vector3 start_position,
            Vector3 end_position,
            float time
            )
        {
            object[]
                parameters = { time, ease_type, start_position, end_position };

            StopCoroutine( "UpdateTranslation" );
            StartCoroutine( "UpdateTranslation", parameters );
      
        }

        //

        IEnumerator UpdateTranslation(
            object[] parameters
            )
        {
            float
                time,
                timer;
            Vector3
                start_position,
                end_position;
            Transform
                the_transform;

            time = (float)parameters[ 0 ];
            timer = 0.0f;

            start_position = (Vector3)parameters[ 2 ];
            end_position = (Vector3)parameters[ 3 ];

            the_transform = GetComponent< Transform >();
            the_transform.localPosition = start_position;

            while( timer < time )
            {
                yield return null;

                timer += Time.deltaTime;

                the_transform.localPosition = EasingFunctions.Ease(
                    (EasingFunctions.TYPE)parameters[1],
                    Mathf.Clamp( timer / time, 0.0f, 1.0f ), 
                    start_position,
                    end_position 
                    );
            }

            the_transform.localPosition = end_position;
        }
    }
}