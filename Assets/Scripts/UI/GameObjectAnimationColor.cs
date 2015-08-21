using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectAnimationColor
    : MonoBehaviour
{
    // -- PUBLIC

    // .. OPERATIONS

    public void StartAnimation(
        Color start_color,
        Color end_color,
        float time
        )
    {
        object[]
            parameters = { time, start_color, end_color };

        StopCoroutine( "UpdateColor" );
        StartCoroutine( "UpdateColor", parameters );
    }

    //

    public void StartAnimation(
        float start_alpha,
        float end_alpha,
        float time
        )
    {
        if( UiGraphic == null )
        {
            UiGraphic = GetComponent<Graphic>();
        }

        StopCoroutine( "UpdateColor" );
        StartCoroutine( "UpdateColor", new object[]{ time, UiGraphic.color.CloneAdjustedAlpha(start_alpha ), UiGraphic.color.CloneAdjustedAlpha( end_alpha ) } );
    }

    //

    public void StopAnimation()
    {
        StopAllCoroutines();
    }
  
    //
  
    public void SetColor( 
        Color new_color
        )
    {
        if( UiGraphic != null )
        {
            UiGraphic.color = new_color;
        }
    }

    // -- PRIVATE

    // .. OPERATIONS
  
    public IEnumerator UpdateColor(
        object[] parameters
        )
    {
        float
            time,
            timer;
        Color
            start_color,
            end_color;

        timer = 0.0f;

        if( UiGraphic == null )
        {
            UiGraphic = GetComponent<Graphic>();
        }
        time = (float)parameters[ 0 ];
        start_color = ( Color )parameters[ 1 ];
        end_color = ( Color )parameters[ 2 ];

        SetColor( start_color );
    
        while( timer < time )
        {
            yield return null;
      
            timer += Time.deltaTime;

            SetColor( Color.Lerp(  start_color, end_color, Mathf.Clamp( timer / time, 0.0f, 1.0f ) ) );
        }

        SetColor( end_color );
    }

    // .. ATTRIBUTES

    Graphic
        UiGraphic;
}