using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISelect : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Button btnStart;

    List<Card> m_Cards = new List<Card>();
    int m_SelectedIndex = -1;
    GameModel m_GameModel = null;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Select; }
    }
    #endregion

    #region 方法
    //返回开始界面
    public void GoBack()
    {
        Game.Instance.LoadScene(1);
    }

    //选用关卡游戏
    public void ChooseLevel()
    {
        StartLevelArgs e = new StartLevelArgs()
        {
            LevelID = m_SelectedIndex
        };

        SendEvent(Consts.E_StartLevel, e);
    }

    void LoadCards()
    {
        // 获取Level集合
        List<Level> levels = m_GameModel.AllLevels;

        // 构建Card集合
        List<Card> cards = new List<Card>();
        for (int i = 0; i < levels.Count; i++)
        {
            Card card = new Card()
            {
                LevelID = i,
                CardImage = levels[i].CardImage,
                //IsLocked = i > m_GameModel.GameProgress - 1
                IsLocked = !(i <= m_GameModel.GameProgress + 1)
            };

            cards.Add(card);
        }
        // 保存
        this.m_Cards = cards;

        // 监听卡片点击事件
        UICard[] uiCards = this.transform.Find("UICards").GetComponentsInChildren<UICard>();
        for (int i = 0; i < uiCards.Length; i++)
        {
            uiCards[i].OnClick += (card) =>
            {
                SelectCard(card.LevelID);
            };
        }

        // 默认选择第一个卡片
        SelectCard(0);
    }

    // 选择卡片
    void SelectCard(int index)
    {
        if(m_SelectedIndex == index)
        {
            return;
        }

        m_SelectedIndex = index;

        // 计算索引号
        int left = m_SelectedIndex - 1;
        int current = m_SelectedIndex;
        int right = m_SelectedIndex + 1;

        // 绑定数据
        Transform container = this.transform.Find("UICards");

        // 左边
        if(left < 0)
        {
            container.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(0).gameObject.SetActive(true);
            container.GetChild(0).GetComponent<UICard>().IsTransparent = true;
            container.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[left]);
        }

        // 当前
        container.GetChild(1).GetComponent<UICard>().IsTransparent = false;
        container.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[current]);
        // 控制游戏开始按钮
        btnStart.gameObject.SetActive(!m_Cards[current].IsLocked);

        // 右边
        if (right >= m_Cards.Count)
        {
            container.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(2).gameObject.SetActive(true);
            container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
            container.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[right]);
        }
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e = data as SceneArgs;
                if(e.SceneIndex == 2)
                {
                    m_GameModel = GetModel<GameModel>();
                    // 初始化Card列表
                    LoadCards();
                }
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
