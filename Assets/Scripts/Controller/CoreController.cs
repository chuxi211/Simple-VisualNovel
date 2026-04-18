using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//作为状态机入口存在，提供逻辑，调用迁移至InitState
public class CoreController : MonoBehaviour
{
    public static CoreController Instance { get; private set; }
    public static GameStateMachine GameStateMachine { get; private set; }
    public StoryController StoryController { get; private set; }
    public StoryLoader StoryLoader { get;private set; }
    public GlobalUIManager GlobalUIManager { get; private set; }
    public SaveManager SaveManage { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        InitGameStateMachine();
        GameStateMachine.ChangeState<InitState>();
        //InitSystems();
        //SceneManager.LoadScene("Home");
    }
    public void InitSystems()
    {
        StoryLoader = new StoryLoader();
        SaveManage =new SaveManager();
        GameObject StoryController = new GameObject("StoryController");
        GameObject UIManager = new GameObject("UIManager");
        StoryController.transform.SetParent(transform);//设置为子物体
        UIManager.transform.SetParent(transform);
        StoryController.AddComponent<StoryController>();//添加StoryController组件
        UIManager.AddComponent<GlobalUIManager>();
        this.StoryController = StoryController.GetComponent<StoryController>();
        this.GlobalUIManager = UIManager.GetComponent<GlobalUIManager>();
        EventBus.Subscribe<ChapterChangedEvent>(StoryLoader.LoadAllNode);
        Debug.Log("订阅完成");
    }
    private void InitGameStateMachine()
    {
        GameStateMachine = new();
        GameStateMachine.RegisterState(new InitState(GameStateMachine));
        GameStateMachine.RegisterState(new HomeState(GameStateMachine));
        GameStateMachine.RegisterState(new StoryState(GameStateMachine));
        GameStateMachine.RegisterState(new SaveLoadState(GameStateMachine));
    }
}
