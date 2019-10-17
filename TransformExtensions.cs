using UnityEngine;

public static class TransformExtensions
{
    //Clear transform children
    public static Transform ClearChildren(this Transform parent)
    {
        foreach(Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
        return parent;
    }
}