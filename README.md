# 🎮 ScenePilot - AI-Powered Unity Scene Control 🚀

ScenePilot is a Unity-based system that allows developers to control their Unity scenes using **natural language commands** powered by **LLamaSharp (Local LLMs)**. With ScenePilot, you can ask an AI to:
- 🎯 Highlight objects
- 📷 Move & focus the camera
- 🎮 Control scene objects dynamically
- ✨ Easily add new AI-driven actions

## 🌟 Features
✅ **Local AI (LLamaSharp)** – No external API required  
✅ **Fast LLM Inference with CUDA GPU Acceleration**  
✅ **Customizable AI Commands** – Expandable for more Unity actions  
✅ **Supports Unity 2022+**  

---

## 🚀 Getting Started

### 1️⃣ Clone the Repo
```sh
git clone https://github.com/YOUR_GITHUB_USERNAME/ScenePilot.git
cd ScenePilot
```

### 2️⃣ Install Dependencies
Make sure you have **Unity 2022.3+** installed and add **LLamaSharp** to your project:

```sh
dotnet add package LLamaSharp.Backend.Cuda12
```

### 3️⃣ Download the AI Model
Place a compatible **GGUF** model in:
```
/Assets/StreamingAssets/LLM/llama-2-7b-chat.Q4_K_M.gguf
```
Get models from **[Hugging Face](https://huggingface.co/TheBloke/Llama-2-7B-GGUF)**.

### 4️⃣ Run the Project
1. Open the project in **Unity 2022.3+**.
2. Press **Play**.
3. Enter a command like:
   ```
   Highlight the Sphere
   ```
   And the AI will automatically control the scene!

---

## ⚙️ How It Works
- The **UI** collects user input.
- The **LLMManager** processes the prompt using a **local LLM**.
- The **AICommandController** executes Unity actions based on AI output.
- Uses **Cancellation Tokens** to allow stopping inference.

---

## 🛠️ Project Structure
```
/Assets/Scripts
  ├── LLM/
  │   ├── LLMManager.cs  # Handles AI Inference
  │   ├── AICommandController.cs  # Executes AI-generated commands
  │   ├── CommandParser.cs  # Converts AI text into Unity actions
  │
  ├── UI/
  │   ├── UIInputHandler.cs  # Manages UI input and loading spinner
  │
  ├── Utils/
  │   ├── JsonUtils.cs  # JSON Parsing for AI responses

/Assets/StreamingAssets/LLM/
  ├── llama-2-7b-chat.Q4_K_M.gguf  # AI Model
```

---

## 🏆 Roadmap
🔜 **Voice Input Support** 🎙️  
🔜 **Multimodal AI (Images + Text)** 🖼️  
🔜 **More Custom Unity Commands** 🎮  

---

## 🎉 Contributing
1. **Fork the repository**.
2. **Create a new feature branch** (`git checkout -b my-feature`).
3. **Commit your changes** (`git commit -m "Added new feature"`).
4. **Push to GitHub** (`git push origin my-feature`).
5. **Create a Pull Request**.

---

## 🛠️ License
📜 MIT License – Free for commercial & personal use.

---

## 🔥 Join the Community
💬 Discord: [Join Here](https://discord.gg/example)  
🐦 Twitter: [@ScenePilotAI](https://twitter.com/ScenePilotAI)  
