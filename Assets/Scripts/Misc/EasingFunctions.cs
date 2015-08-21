using System;
using UnityEngine;

public class EasingFunctions
{
    // -- PUBLIC

    // .. TYPES

    public enum TYPE
    {
        Out,
        In,
        FarIn,
        InOut,
        OutBounce,
        OutElastic,
        OutElasticBig,
        BackInCubic,
        Sinus,
        SmallStretch,
        OutOverShoot,
        Regular
    }

    // .. CONSTRUCTORS

    // .. FUNCTIONS

    public static float Ease(TYPE type, float normalized_time, float start_value, float end_value)
    {
        switch (type)
        {
            case TYPE.Out:
                {
                    return EaseOut(normalized_time, start_value, end_value);
                }

            case TYPE.In:
                {
                    return EaseIn(normalized_time, start_value, end_value);
                }

            case TYPE.FarIn:
                {
                    return FarEaseIn(normalized_time, start_value, end_value);
                }

            case TYPE.InOut:
                {
                    return EaseInOut(normalized_time, start_value, end_value);
                }

            case TYPE.OutBounce:
                {
                    return EaseOutBounce(normalized_time, start_value, end_value);
                }

            case TYPE.OutElastic:
                {
                    return EaseOutElastic(normalized_time, start_value, end_value);
                }

            case TYPE.OutElasticBig:
                {
                    return EaseOutElasticBig(normalized_time, start_value, end_value);
                }

            case TYPE.BackInCubic:
                {
                    return BackInCubic(normalized_time, start_value, end_value);
                }

            case TYPE.Sinus:
                {
                    return Sinus(normalized_time, start_value, end_value);
                }

            case TYPE.SmallStretch:
                {
                    return SmallStretch(normalized_time, start_value, end_value);
                }

            case TYPE.OutOverShoot:
                {
                    return OutOverShoot(normalized_time, start_value, end_value);
                }

            case TYPE.Regular:
                {
                    float
                        factor,
                        one_minus_factor;

                    factor = normalized_time;
                    one_minus_factor = 1.0f - factor;
                    return start_value * one_minus_factor + end_value * factor;
                }
        }

        return Mathf.Lerp(start_value, end_value, normalized_time);
    }

    public static Vector3 Ease(TYPE type, float normalized_time, Vector3 start_value, Vector3 end_value)
    {
        float
            factor,
            one_minus_factor;

        factor = Ease(type, normalized_time, 0.0f, 1.0f);
        one_minus_factor = 1.0f - factor;

        return new Vector3(
            start_value.x * one_minus_factor + end_value.x * factor,
            start_value.y * one_minus_factor + end_value.y * factor,
            start_value.z * one_minus_factor + end_value.z * factor
            );
    }

    public static Vector2 Ease(TYPE type, float normalized_time, Vector2 start_value, Vector2 end_value)
    {
        float
            factor,
            one_minus_factor;

        factor = Ease(type, normalized_time, 0.0f, 1.0f);
        one_minus_factor = 1.0f - factor;

        return new Vector2(
            start_value.x * one_minus_factor + end_value.x * factor,
            start_value.y * one_minus_factor + end_value.y * factor
            );
    }

    public static Quaternion Ease(TYPE type, float normalized_time, Quaternion start_value, Quaternion end_value)
    {
        float
            factor,
            one_minus_factor;

        factor = Ease(type, normalized_time, 0.0f, 1.0f);
        one_minus_factor = 1.0f - factor;

        return new Quaternion(
            start_value.x * one_minus_factor + end_value.x * factor,
            start_value.y * one_minus_factor + end_value.y * factor,
            start_value.z * one_minus_factor + end_value.z * factor,
            start_value.w * one_minus_factor + end_value.w * factor
            );
    }

    public static void Ease(TYPE type, float normalized_time, Transform start_value, Transform end_value, ref Transform value)
    {
        float factor = Ease(type, normalized_time, 0.0f, 1.0f);
        value.Lerp(start_value, end_value, factor);
    }

    public static void Ease(TYPE type, float normalized_time, TransformValue start_value, TransformValue end_value, ref Transform value)
    {
        float factor = Ease(type, normalized_time, 0.0f, 1.0f);
        value.Lerp(start_value, end_value, factor);
    }

    public static Color Ease(TYPE type, float normalized_time, Color start_value, Color end_value)
    {
        float
            factor,
            one_minus_factor;

        factor = Ease(type, normalized_time, 0.0f, 1.0f);
        one_minus_factor = 1.0f - factor;

        return new Color(
            start_value.r * one_minus_factor + end_value.r * factor,
            start_value.g * one_minus_factor + end_value.g * factor,
            start_value.b * one_minus_factor + end_value.b * factor,
            start_value.a * one_minus_factor + end_value.a * factor
            );
    }

    public static float EaseOut(float time, float start_value, float end_value)
    {
        float
            ease_value;

        ease_value = 1 - time;
        ease_value = 1 - ease_value * ease_value * ease_value;
        return end_value * ease_value + start_value * (1 - ease_value);
    }

    public static float EaseIn(float time, float start_value, float end_value)
    {
        float ease_value = time * time * time;
        return end_value * ease_value + start_value * (1 - ease_value);
    }

    public static float FarEaseIn(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            calculated_function = (-1.8f * tc * ts + 6.6f * ts * ts + -9.5f * tc + 6.9f * ts + -1.2f * time),
            one_minus_calculated_function = 1 - calculated_function;

        return start_value * one_minus_calculated_function + end_value * calculated_function;
    }

    public static float EaseInOut(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            ease_value;

        ease_value = 6 * tc * ts - 15 * ts * ts + 10 * tc;
        return end_value * ease_value + start_value * (1 - ease_value);
    }

    public static float EaseOutBounce(float time, float start_value, float end_value)
    {
        float
            temporary_value;

        if (time >= 1.0f)
        {
            return end_value;
        }
        else if (time < 0.363636f)     // 0.363636 == 1 / 2.75f from source code
        {
            temporary_value = (7.5625f * time * time);
        }
        else if (time < 0.727272f)    //== 2.0f * 0.363636f
        {
            temporary_value = time - 0.545454f;    // == 1.5f * 0.363636f
            temporary_value = (7.5625f * temporary_value * temporary_value + 0.75f);
        }
        else if (time < 0.909090f)   // == 2.5f * 0.36363636f
        {
            temporary_value = time - 0.818181f;    // == 2.25f * 0.363636f
            temporary_value = (7.5625f * temporary_value * temporary_value + 0.9375f);
        }
        else // if ( time < 1.0f )
        {
            temporary_value = time - 0.9545454f;    // == 2.625f * 0.363636f
            temporary_value = (7.5625f * temporary_value * temporary_value + 0.984375f);
        }

        return end_value * temporary_value + start_value * (1 - temporary_value);
    }

    public static float EaseOutElastic(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            calculated_function = (17.095f * tc * ts - 55.9325f * ts * ts + 69.38f * tc - 40.19f * ts + 10.6475f * time),
            one_minus_calculated_function = 1 - calculated_function;

        return start_value * one_minus_calculated_function + end_value * calculated_function;
    }

    public static float EaseOutElasticBig(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            calculated_function = (56.0f * tc * ts - 175.0f * ts * ts + 200.0f * tc - 100.0f * ts + 20.0f * time),
            one_minus_calculated_function = 1 - calculated_function;

        return start_value * one_minus_calculated_function + end_value * calculated_function;

    }

    public static float BackInCubic(float time, float start_value, float end_value)
    {
        return start_value + end_value * (4 * time * time * time - 3 * time * time);
    }

    public static float Sinus(float time, float start_value, float end_value)
    {
        return (Mathf.Sin(time) + 1) * 0.5f;
    }

    public static float SmallStretch(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            calculated_function = (4.895f * tc * ts + -9.19f * ts * ts + 5.595f * tc + -3.4f * ts + 3.1f * time),
            one_minus_calculated_function = 1 - calculated_function;

        return start_value * one_minus_calculated_function + end_value * calculated_function;
    }

    public static float OutOverShoot(float time, float start_value, float end_value)
    {
        float
            ts = time * time,
            tc = ts * time,
            calculated_function = (11.2975f * tc * ts + -23.195f * ts * ts + 30.695f * tc + -36.995f * ts + 19.1975f * time),
            one_minus_calculated_function = 1 - calculated_function;

        return start_value * one_minus_calculated_function + end_value * calculated_function;
    }

}
