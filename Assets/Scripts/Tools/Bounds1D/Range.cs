using UnityEngine;
using System.Collections;

// Author: Pierre CAMILLI

[System.Serializable]
public struct Range {

    #region Getter Setter
    [SerializeField]
    private float m_min;
    /// <summary>
    /// Minimal value
    /// </summary>
    public float Min { get { return m_min; } }

    [SerializeField]
    private float m_max;
    /// <summary>
    /// Maximal value
    /// </summary>
    public float Max { get { return m_max; } }
    
    public bool Set(float min, float max)
    {
        if (min <= max)
        {
            m_min = min;
            m_max = max;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Difference between max and min value
    /// </summary>
    public float Length { get { return Max - Min; } }
    #endregion

    /// <summary>
    /// One dimensional bounds
    /// </summary>
    /// <param name="min">Minimal value of the bounds</param>
    /// <param name="max">Maximal value of the bounds</param>
    Range(float min = 0.0f, float max = 0.0f)
    {
        if (min <= max)
        {
            m_min = min;
            m_max = max;
        }
        else
        {
            m_max = min;
            m_min = min;
        }
    }

    #region Operators

    public static Range operator -(Range bounds)
    {
        return new Range(-bounds.Max, -bounds.Min);
    }

    public static Range operator +(Range bounds, float value)
    {
        return new Range(bounds.Min + value, bounds.Max + value);
    }

    public static Range operator -(Range bounds, float value)
    {
        return bounds + (-value);
    }

    public static Range operator +(Range bounds1, Range bounds2)
    {
        return new Range(bounds1.Min + bounds2.Min, bounds1.Max + bounds2.Max);
    }

    public static Range operator -(Range bounds1, Range bounds2)
    {
        return bounds1 + (-bounds2);
    }

    #endregion

    #region Methods
    /// <summary>
    /// Clamp a value between the bounds
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float Clamp(float value)
    {
        return Mathf.Clamp(value, Min, Max);
    }

    /// <summary>
    /// Calculates the linear parameter t that produces the interpolant value within the range [a, b].
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float InverseLerp(float value)
    {
        return Mathf.InverseLerp(Min, Max, value);
    }

    /// <summary>
    /// Linearly interpolate value between the bounds
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float Lerp(float value)
    {
        return Mathf.Lerp(Min, Max, value);
    }

    /// <summary>
    /// Same as Lerp but makes sure the values interpolate correctly when they wrap around
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float LerpAngle(float value)
    {
        return Mathf.LerpAngle(Min, Max, value);
    }

    /// <summary>
    /// Linearly interpolate value between the bounds
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float LerpUnclamped(float value)
    {
        return Mathf.LerpUnclamped(Min, Max, value);
    }

    /// <summary>
    /// Increase the value of both bounds by the specificated value
    /// </summary>
    /// <param name="value"></param>
    public void Translate(float value)
    {
        m_min += value;
        m_max += value;
    }
    #endregion
}
