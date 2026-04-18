# SimpleVNProject

## 中文

### 描述
这是一个基于 Unity 的视觉小说框架。

> 注意：项目当前仍不完善。由于时间有限，先上传当前版本，后续有时间会继续优化并更新。

---

### 开发环境
- Unity 2022.3.62f1c1  
- Microsoft Visual Studio 2022  
- Microsoft Visual Studio Code  

---

### 已实现功能
1. 线性剧情播放  
2. 基础选项系统  
3. BGM 播放  
4. 存档 / 读档  
5. 背景切换  
6. 角色立绘显示与移动  

---

### 项目结构
```
Assets
├── Plugins
├── Resources
│ ├── audio # 音频资源
│ ├── Chapter # 剧情文本（JSON）
│ ├── CharacterAsset # 角色资产
│ └── image # 图像资源（立绘 / CG）
├── Scenes # Unity 场景
├── Scripts
│ ├── Character # CharacterManager / Actor / View
│ ├── Controller
│ │ ├── CoreController.cs # GameStateMachine 入口
│ │ ├── StoryController.cs # StoryStateMachine 入口
│ │ └── StateMachine
│ │ ├── GameStateMachine
│ │ └── States
│ ├── Data # StoryNode / CharacterAssetData
│ ├── EventBus # 事件总线
│ ├── Story # 播放器 / 加载器
│ └── UI # UI 系统
├── Template # CSV / JSON 模板
```
---

### 核心结构说明
- **CoreController**  
  游戏主入口，驱动 GameStateMachine  

- **StoryController**  
  负责剧情推进，驱动 StoryStateMachine  

- **StateMachine**  
  使用层次状态机管理整体流程  

- **EventBus**  
  用于模块之间的解耦通信  

---

### 使用技术 / 设计模式
- 事件总线（EventBus）  
- 层次状态机（Hierarchical State Machine）  
- 单例模式（Singleton）  

---

### 运行方式
1. 使用 Unity Hub 打开项目  
2. 打开主场景（如 MainScene）  
3. 点击 Play 运行  

---

### 声明
本项目为个人开发实践，仅用于**学习、技术交流与展示**，不用于任何商业用途。

项目中部分资源来源于游戏《解神者》（X2-siva 制作组）。尽管该游戏已停止运营，但相关资源仍受版权保护，版权归原权利方所有。

如有相关权利方提出需求，本项目将及时删除或替换相关内容。请勿将本项目中的资源用于商业用途、AI 训练或其他可能侵犯版权的行为。
---

## English

### Description
This is a visual novel framework based on Unity.

> Note: This project is not fully complete. Due to limited time, the current version is uploaded first. It may be improved in the future.

---

### Development Environment
- Unity 2022.3.62f1c1  
- Microsoft Visual Studio 2022  
- Microsoft Visual Studio Code  

---

### Features
- Linear story playback  
- Basic choice system  
- BGM playback  
- Save / Load system  
- Background switching  
- Character display and movement  

---

### Project Structure
```
Assets
├── Plugins
├── Resources
│ ├── audio # audio assets
│ ├── Chapter # story data (JSON)
│ ├── CharacterAsset # character assets
│ └── image # images (character / CG)
├── Scenes
├── Scripts
│ ├── Character
│ ├── Controller
│ │ ├── CoreController.cs
│ │ ├── StoryController.cs
│ │ └── StateMachine
│ │ ├── GameStateMachine
│ │ └── States
│ ├── Data
│ ├── EventBus
│ ├── Story
│ └── UI
├── Template
```
---

### Core Architecture
- **CoreController**  
  Entry point of the game, drives the GameStateMachine  

- **StoryController**  
  Handles story progression, drives the StoryStateMachine  

- **StateMachine**  
  Manages game flow using hierarchical state machines  

- **EventBus**  
  Provides decoupled communication between systems  

---

### Design Patterns
- Event Bus  
- Hierarchical State Machine  
- Singleton  

---

### How to Run
1. Open the project with Unity Hub  
2. Open the main scene (e.g. MainScene)  
3. Press Play  

---

### Declaration
This project is an individual development practice intended solely for **learning, technical demonstration, and portfolio purposes**, and is not intended for any commercial use.

Some assets in this project are derived from the game *Eclipse* (developed by the X2-siva team). Although the game has been discontinued, the related assets remain protected by copyright, and all rights belong to their respective owners.

If any rights holder requests removal or modification of related content, it will be handled promptly. Please do not use any assets from this project for commercial purposes, AI training, or any other activities that may infringe copyright.