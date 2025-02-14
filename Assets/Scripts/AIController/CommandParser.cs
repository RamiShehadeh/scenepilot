using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
public static class CommandParser
{
    /// <summary>
    /// Attempts to parse the LLM output into a list of AICommand objects.
    /// Returns an empty list if parsing fails.
    /// </summary>
    public static List<AICommand> ParseCommands(string rawOutput)
    {
        List<AICommand> commands = new List<AICommand>();
        string jsonSubstring = StripFirstJsonArray(rawOutput);
        if (string.IsNullOrEmpty(jsonSubstring))
        {
            Debug.LogError("No valid JSON array found in the output!");
            return commands;
        }

        try
        {
            commands = JsonConvert.DeserializeObject<List<AICommand>>(jsonSubstring);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to parse commands: " + ex.Message);
        }

        return commands;
    }

    /// <summary>
    /// Returns the first complete JSON array substring [ ... ] found in 'text',
    /// or null if no balanced array is found.
    /// </summary>
    public static string StripFirstJsonArray(string text)
    {
        int startIdx = text.IndexOf('[');
        if (startIdx == -1) return null;

        // We'll keep a balance counter to find the matching ']' for the first '['
        int balance = 0;
        for (int i = startIdx; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                balance++;
            }
            else if (text[i] == ']')
            {
                balance--;
                if (balance == 0)
                {
                    // Return the substring from startIdx to i inclusive
                    return text.Substring(startIdx, i - startIdx + 1);
                }
            }
        }

        // If we never return inside the loop, no complete array was found
        return null;
    }
}
