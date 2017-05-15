using UnityEngine;

public static class Consts
{
    //目录
    public static readonly string LevelDir = Application.dataPath + @"\Game\Res\Levels";
    public static readonly string MapDir = Application.dataPath + @"\Game\Res\Maps";

    //存档


    //Model ---> 以M开头表示数据模型（Model）


    //View ---> 以V开头表示视图（View）
    public const string V_Start = "V_Start";
    public const string V_Select = "V_Select";
    public const string V_Board = "V_Board";
    public const string V_CountDown = "V_CountDown";
    public const string V_Win = "V_Win";
    public const string V_Lost = "V_Lost";
    public const string V_Sytem = "V_Sytem";
    public const string V_Complete = "V_Complete";

    //Controller --- 以E开头表示事件（Event)
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene";  // SceneArg
    public const string E_ExitScene = "E_ExitScene";    // SceneArg

    public const string E_StartLevel = "E_StartLevel";  // StartLevelArg
    public const string E_EndLevel = "E_EndLevel";    // EndLevelArg

    public const string E_CountDownComplete = "E_CountDownComplete";
}

public enum GameSpeed
{
    One,
    Two
}

