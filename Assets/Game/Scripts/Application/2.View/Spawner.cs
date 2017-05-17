using UnityEngine;
using System.Collections;

public class Spawner : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    Map m_Map = null;
    Luobo m_Luobo = null;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Spanwner; }
    }
    #endregion

    #region 方法
    public void SpawnLuobo(Vector3 position)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn("Luobo");
        m_Luobo = go.GetComponent<Luobo>();
        m_Luobo.Dead += luobo_Dead;
        m_Luobo.transform.position = position;
    }

    public void SpawnMonster(int MonsterType)
    {
        //创建怪物
        string prefabName = "Monster" + MonsterType;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        Monster monster = go.GetComponent<Monster>();
        monster.Reached += monster_Reached;
        monster.HpChanged += monster_HpChanged;
        monster.Dead += monster_Dead;
        monster.Load(m_Map.Path);
    }

    void luobo_Dead(Role luobo)
    {
        //回收萝卜
        Game.Instance.ObjectPool.Unspawn(luobo.gameObject);

        //游戏胜利
        GameModel gm = GetModel<GameModel>();
        SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelID, IsWin = false });
    }

    void monster_Reached(Monster monster)
    {
        //萝卜掉血
        m_Luobo.Damage(1);

        //怪物死亡
        monster.Hp = 0;
    }

    void monster_HpChanged(int arg1, int arg2)
    {

    }

    void monster_Dead(Role monster)
    {
        //回收怪物
        Game.Instance.ObjectPool.Unspawn(monster.gameObject);

        //失败判断
        GameModel gm = GetModel<GameModel>();
        RoundModel rm = GetModel<RoundModel>();
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (!m_Luobo.IsDead             //萝卜没死
            && monsters.Length <= 0     //场景上已没有怪物
            && rm.AllRoundsComplete)    //所有怪物已出完
        {
            SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelID, IsWin = true });
        }
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_SpawnMonster);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e0 = data as SceneArgs;
                if (e0.SceneIndex == 3)
                {   
                    //获取数据
                    GameModel gModel = GetModel<GameModel>();

                    //加载地图
                    m_Map = GetComponent<Map>();
                    m_Map.LoadLevel(gModel.PlayLevel);

                    //加载萝卜
                    Vector3[] path = m_Map.Path;
                    Vector3 pos = path[path.Length - 1];
                    SpawnLuobo(pos);
                }
                break;
            case Consts.E_SpawnMonster:
                SpawnMonsterArgs e1 = data as SpawnMonsterArgs;
                SpawnMonster(e1.MonsterType);
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
