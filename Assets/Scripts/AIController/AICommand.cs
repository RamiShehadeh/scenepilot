using UnityEngine;

[System.Serializable]
public class AICommand
{
    public string command;    // "highlight", "focus_camera", "move_object"
    public string @object;    // The target object’s name
    public float x;
    public float y;
    public float z;

    public string message;

    public Vector3 TranslationAmount
    {
        get { return new Vector3(x, y, z); }
    }
}

