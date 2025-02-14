using LLama.Common;
using LLama;
using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using LLama.Native;
using static LLama.StatefulExecutorBase;

public class LLMManager : MonoBehaviour
{
    public static LLMManager Instance { get; private set; }

    private readonly string modelLocalPath = "models/llama-2-7b-chat.Q4_0.gguf";

    [Header("Model Settings")]
    [SerializeField] private uint contextSize = 1024;
    [SerializeField] private int gpuLayerCount = 20;

    // LLamaSharp objects
    private ModelParams modelParams;
    private LLamaWeights weights;
    private LLamaContext context;
    private InteractiveExecutor executor;
    private bool isInitialized = false;

    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        await InitializeModelAsync();
    }

    /// <summary>
    /// Initializes the LLamaSharp model.
    /// </summary>
    private async Task InitializeModelAsync()
    {
        string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, modelLocalPath);
        Debug.Log("[LLMManager] Loading model from: " + modelPath);

        // Enable backend logging
        /*NativeLibraryConfig.All.WithLogCallback((LLamaLogLevel level, string message) =>
        {
            Debug.Log($"[NativeLog][{level}]: {message}");
        });*/

        try
        {
            // Configure model parameters
            modelParams = new ModelParams(modelPath)
            {
                ContextSize = contextSize,
                GpuLayerCount = gpuLayerCount,
                MainGpu = 0
            };

            // Load model weights (synchronous operation)
            weights = LLamaWeights.LoadFromFile(modelParams);
            // Create the inference context
            context = weights.CreateContext(modelParams);
            // Create the interactive executor for chat‐style sessions
            executor = new InteractiveExecutor(context);
            isInitialized = true;
            Debug.Log("[LLMManager] Model loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("[LLMManager] Failed to load model: " + ex.Message);
        }

        // Optional: await a small delay to ensure everything’s settled
        await Task.Yield();
    }

    /// <summary>
    /// Sends a prompt to the model and returns its output.
    /// The prompt is wrapped with system instructions so the LLM outputs a JSON array of commands.
    /// </summary>
    /// <param name="prompt">The natural language query (e.g. "What is the engine?")</param>
    /// <returns>Model output as a string (expected to be JSON)</returns>
    public async Task<string> GetResponseAsync(string prompt)
    {
        if (!isInitialized)
        {
            Debug.LogError("[LLMManager] Model is not initialized yet.");
            return "";
        }

        string systemInstructions = @"You are an assistant that converts natural language instructions about a 3D scene into a JSON array of commands.
Each command must be an object with one of these formats:
  1. { ""command"": ""highlight"", ""object"": ""<objectName>"" }
  2. { ""command"": ""focus_camera"", ""object"": ""<objectName>"" }
  3. { ""command"": ""move_object"", ""object"": ""<objectName>"", ""x"": <float>, ""y"": <float>, ""z"": <float> }

If the instruction is unclear or invalid, output a JSON object with an error field, for example: { ""error"": ""Could not interpret the command."" }.

Follow these examples exactly:
Example 1:
User: Show me the engine.
Output: [ { ""command"": ""highlight"", ""object"": ""engine"" }, { ""command"": ""focus_camera"", ""object"": ""engine"" } ]

Example 2:
User: Move the door by 1 0 -1.
Output: [ { ""command"": ""move_object"", ""object"": ""door"", ""x"": 1, ""y"": 0, ""z"": -1 } ]

DO NOT include any extra text, prompts, or commentary. Only output the JSON response.
";

        // Build a chat history with only the system instructions.
        ChatHistory chatHistory = new ChatHistory();
        chatHistory.AddMessage(AuthorRole.System, systemInstructions);

        // Create a chat session using the chat history.
        ChatSession session = new ChatSession(executor, chatHistory);

        // Inference parameters (adjust MaxTokens as needed).
        InferenceParams inferenceParams = new InferenceParams()
        {
            MaxTokens = 128, // Increase if necessary.
            AntiPrompts = new List<string>() { "User:" } // Stop generation if "User:" appears.
        };

        // Pass the user message as the first argument.
        StringBuilder resultBuilder = new StringBuilder();
        try
        {
            await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), inferenceParams))
            {
                resultBuilder.Append(text);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[LLMManager] Inference error: " + ex.Message);
        }

        string output = resultBuilder.ToString();
        Debug.Log("[LLMManager] LLM Response: " + output);
        return output;
    }

}
