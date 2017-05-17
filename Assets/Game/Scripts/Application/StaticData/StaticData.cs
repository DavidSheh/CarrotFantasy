using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticData : Singleton<StaticData>
{
    Dictionary<int, LuoboInfo> m_Luobos = new Dictionary<int, LuoboInfo>();
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();

    protected override void Awake()
    {
        base.Awake();

        InitLuobos();
        InitMonsters();
        InitTowers();
        InitBullets();
    }

    void InitLuobos()
    {
        m_Luobos.Add(0, new LuoboInfo() { ID = 0, Hp = 4 });
    }

    void InitMonsters()
    {
        /*
        m_monsters.Add(0, new MonsterInfo() { Hp = 1, MoveSpeed = 1.5f });
        m_monsters.Add(1, new MonsterInfo() { Hp = 2, MoveSpeed = 1f });
        m_monsters.Add(2, new MonsterInfo() { Hp = 5, MoveSpeed = 1f });
        m_monsters.Add(3, new MonsterInfo() { Hp = 10, MoveSpeed = 1f });
        m_monsters.Add(4, new MonsterInfo() { Hp = 10, MoveSpeed = 1f });
        m_monsters.Add(5, new MonsterInfo() { Hp = 100, MoveSpeed = 0.5f });
        */

        m_Monsters.Add(0, new MonsterInfo() { ID = 0, Hp = 1, MoveSpeed = 5f });
        m_Monsters.Add(1, new MonsterInfo() { ID = 1, Hp = 2, MoveSpeed = 5f });
        m_Monsters.Add(2, new MonsterInfo() { ID = 2, Hp = 5, MoveSpeed = 5f });
        m_Monsters.Add(3, new MonsterInfo() { ID = 3, Hp = 10, MoveSpeed = 5f });
        m_Monsters.Add(4, new MonsterInfo() { ID = 4, Hp = 10, MoveSpeed = 5f });
        m_Monsters.Add(5, new MonsterInfo() { ID = 5, Hp = 100, MoveSpeed = 5f });
    }

    void InitTowers()
    {

    }

    void InitBullets()
    {

    }

    public LuoboInfo GetLuoboInfo()
    {
        return m_Luobos[0];
    }

    public MonsterInfo GetMonsterInfo(int monsterType)
    {
        return m_Monsters[monsterType];
    }
}