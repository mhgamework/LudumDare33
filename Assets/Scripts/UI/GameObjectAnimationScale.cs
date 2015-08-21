using System.Collections;
using UnityEngine;

public class GameObjectAnimationScale
    : MonoBehaviour
{
    // -- PUBLIC

    // .. OPERATIONS
  
    public void StartAnimation(
        EasingFunctions.TYPE ease_type,
        Vector3 start_scale,
        Vector3 end_scale,
        float time
        )
    {
        object[]
            parameters = { time, ease_type, start_scale, end_scale };
    
        StopCoroutine( "UpdateScale" );
        StartCoroutine( "UpdateScale", parameters );
    }

    // -- PRIVATE

    // .. OPERATIONS
  
    IEnumerator UpdateScale(
        object[] parameters
        )
    {
        float
            time,
            timer;
        Vector3
            start_scale,
            end_scale;
        Transform
            the_transform;

        time = (float)parameters[ 0 ];
        timer = 0.0f;
    
        start_scale = (Vector3)parameters[ 2 ];
        end_scale = (Vector3)parameters[ 3 ];
    
        the_transform = GetComponent< Transform >();
        the_transform.localScale = start_scale;
    
        while( timer < time )
        {
            yield return null;
      
            timer += Time.deltaTime;

            the_transform.localScale = EasingFunctions.Ease(
                (EasingFunctions.TYPE)parameters[1],
                Mathf.Clamp( timer / time, 0.0f, 1.0f ), 
                start_scale,
                end_scale
                );
        }
    
        the_transform.localScale = end_scale;
    }

}