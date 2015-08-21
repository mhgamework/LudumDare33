using UnityEngine;
using System.Collections;

public class MathfExtended
{
    // -- PUBLIC

    // .. FUNCTIONS

    public static float Repeat(float time, float repeat)
    {
        while (time > repeat)
        {
            time -= repeat;
        }

        while (time < 0.0f)
        {
            time += repeat;
        }

        return time;
    }

    public static Vector3 BezierCurve(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, float time)
    {
        Vector3
            result;
        float
            one_minus_time,
            part_one,
            part_two,
            part_three,
            part_four;

        one_minus_time = 1 - time;

        part_one = time * time * time;
        part_two = 3.0f * time * time * one_minus_time;
        part_three = 3.0f * time * one_minus_time * one_minus_time;
        part_four = one_minus_time * one_minus_time * one_minus_time;


        result.x = point1.x * part_one + point2.x * part_two + point3.x * part_three + point4.x * part_four;
        result.y = point1.y * part_one + point2.y * part_two + point3.y * part_three + point4.y * part_four;
        result.z = point1.z * part_one + point2.z * part_two + point3.z * part_three + point4.z * part_four;

        return result;
    }

    // -- PRIVATE

    // .. FUNCTIONS


}
