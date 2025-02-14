# 🎮 ScenePilot - AI-Powered Unity Scene Control 🚀

ScenePilot is a Unity-based system that allows developers to control their Unity scenes using **natural language commands** powered by **LLamaSharp (Local LLMs)**. With ScenePilot, you can ask an AI to:
-  Highlight objects
-  Move & focus the camera
-  Control scene objects dynamically
-  Easily add new AI-driven actions

## 🌟 Features
 - **Local AI (LLamaSharp)** – No external API required  
 - **Fast LLM Inference with CUDA GPU Acceleration**  
 - **Customizable AI Commands** – Expandable for more Unity actions  
 - **Supports Unity 2022+**  

---

## 🚀 Getting Started

### 1️⃣ Clone the Repo
```sh
git clone https://github.com/RamiShehadeh/scenepilot.git
cd scenepilot
```

### 2️⃣ Install Dependencies
Make sure you have **Unity 2022.3+** installed and add **LLamaSharp** to your project.
Refer to the **[LLamaSharp Github](https://github.com/SciSharp/LLamaSharp)** for more details on how to install and use LLamaSharp.

### 3️⃣ Download the AI Model

Place a compatible **GGUF** model in:
```
/Assets/StreamingAssets/models/
```
Get models from **[Hugging Face](https://huggingface.co/TheBloke/Llama-2-7B-GGUF)**.

### 4️⃣ Run the Project
1. Open the project in **Unity 2022.3+**.
2. Press **Play**.
3. Enter a command like:
   ```
   Show me the sphere
   ```
   And the AI will automatically control the scene!

---

## ⚙️ How It Works

- The **UI** collects user input.
- The **LLMManager** processes the prompt using a **local LLM**.
- The **AICommandController** executes Unity actions based on AI output.

---


## 🏆 Roadmap

🔜 **Voice Input Support**   
🔜 **Multimodal AI (Images + Text)**   
🔜 **More Custom Unity Commands**   

---

## 🎉 Contributing
 Feel free to contribute to this however you like.

---

## 🛠️ License
📜 MIT License – Free for commercial & personal use.
