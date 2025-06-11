using UnityEngine;

public class MinAttribute : PropertyAttribute
{
    public float min;

    public MinAttribute(float min)
    {
        this.min = min;
    }
}