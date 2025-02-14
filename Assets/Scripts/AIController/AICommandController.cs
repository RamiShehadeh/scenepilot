using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AICommandController : MonoBehaviour
{
    [SerializeField] private Camera focusCamera;

    // Optionally assign parts by name or have a registry of parts in the scene
    // For simplicity, we assume that each part name is the object name.

    private void Awake()
    {
        GrabReferences();
    }

    // This function can be called by a UI button
    public void ProcessUserQuery(string userQuery)
    {
        StartCoroutine(HandleUserQuery(userQuery));
    }

    private IEnumerator HandleUserQuery(string query)
    {
        string llmOutput = query;

        List<AICommand> commands = CommandParser.ParseCommands(llmOutput);
        Debug.Log("Command Count: " + commands.Count);

        foreach (var cmd in commands)
        {
            ExecuteCommand(cmd);
        }

        yield return null;
    }

    private void ExecuteCommand(AICommand cmd)
    {
        // If the command is "error", log it and return early.
        if (cmd.command.Equals("error", System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"LLM Error: {cmd.message}");
            return;
        }

            switch (cmd.command.ToLower())
        {
            case "highlight":
                HighlightObject(cmd.@object);
                break;
            case "focus_camera":
                FocusCameraOnObject(cmd.@object);
                break;
            case "move_object":
                MoveObject(cmd.@object, cmd.TranslationAmount);
                break;
            default:
                Debug.LogWarning("Unknown command: " + cmd.command);
                break;
        }
    }

    private void HighlightObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            // glowing effect
            Renderer rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.EnableKeyword("_EMISSION");
                rend.material.SetColor("_EmissionColor", Color.yellow);
                Debug.Log("Highlighted object: " + objectName);
            }
        }
        else
        {
            Debug.LogWarning("Object not found: " + objectName);
        }
    }

    private void FocusCameraOnObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null && focusCamera != null)
        {
            Vector3 targetPos = obj.transform.position;
            focusCamera.transform.position = targetPos + new Vector3(0, 2, -5);
            focusCamera.transform.LookAt(obj.transform);
            Debug.Log("Focusing camera on: " + objectName);
        }
        else
        {
            Debug.LogWarning("Object or camera not found for focus: " + objectName);
        }
    }

    private void MoveObject(string objectName, Vector3 translation)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.transform.position += translation;
            Debug.Log($"Moved object {objectName} by {translation}");
        }
        else
        {
            Debug.LogWarning("Object not found: " + objectName);
        }
    }
    private void GrabReferences()
    {
        if (focusCamera == null) 
        {
            focusCamera = Camera.main;
        }
    }
}
