using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions {

    public static void SetLayer(this GameObject gameObject, string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    public static void SetLayer(this GameObject gameObject, int layerID)
    {
        gameObject.layer = layerID;
    }

}
