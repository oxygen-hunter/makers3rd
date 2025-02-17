﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class NpcIllustration : MonoBehaviour
{
    static public NpcIllustration Instance;
    public List<Item> Items;
    public GameObject IllustrationPanel; //拖动赋值
    public Button OpenBtn; //拖动赋值
    public Button CloseBtn; //拖动赋值
    public Image[] ItemImages; //拖动赋值
    public Image DetailImage; //拖动赋值
    public Text DetailDescription; //拖动赋值

    //Item[] ItemDatabase;
    public Dictionary<int, Item> ItemDatabase;

    Tuple<int, int, string, string, string, string>[] AllItem = 
    {
        new Tuple<int, int, string, string, string, string>(100, 20, "精卫", "炎帝最小的女儿，溺水而亡，化作精卫鸟。拜访精卫后再找找她的父亲，也许有意想不到的惊喜。", "精卫", "发鸠山"),
        new Tuple<int, int, string, string, string, string>(101, 20, "毕方", "有鸟焉，其状如鹤，一足，赤文青质而白喙，名曰毕方。", "毕方", "章莪山"),
        new Tuple<int, int, string, string, string, string>(102, 20, "狰", "有兽焉，其状如赤豹，五尾一角，其音如击石，其名曰狰。", "狰", "章莪山"),
        new Tuple<int, int, string, string, string, string>(103, 20, "狸力", "有兽焉，其状如豚，有距，其音如狗吠，其名曰狸力；见则其县多土功。", "狸力", "柜山"),
        new Tuple<int, int, string, string, string, string>(104, 20, "神农", "姜姓部落首领，善用火，称炎帝，又号神农氏，尝百草。", "神农", "炎帝部落"),
        new Tuple<int, int, string, string, string, string>(105, 20, "九尾狐", "有兽焉，其状如狐而九尾，其音如婴儿，能食人。", "九尾狐", "青丘"),
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //打开按钮监听事件
            OpenBtn.onClick.AddListener(ShowIllustration);
            CloseBtn.onClick.AddListener(HideIllustration);
            //初始化背包物品数据库
            Items = new List<Item>();
            ItemDatabase = new Dictionary<int, Item>();
            InitIllstration();
            //添加监听事件
            for (int i = 0; i < ItemImages.Length; i++)
            {
                int temp = i; //处理delegate的问题，二次赋值
                ItemImages[i].GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    ShowDetail(temp);
                });
            }
            //隐藏图鉴
            HideIllustration();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("illustration start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowIllustration()
    {
        if (IllustrationPanel.activeInHierarchy)
        {   //如果开着，就关了
            IllustrationPanel.SetActive(false);
        }
        else
        {   //否则打开物品栏
            IllustrationPanel.SetActive(true);
        }
    }

    void HideIllustration()
    {
        IllustrationPanel.SetActive(false);
    }

    public void ShowDetail(int index)
    {
        if (index < Items.Count)
        {
            Sprite sr = Resources.Load<Sprite>("Npc/" + Items[index].name + "头像");
            DetailImage.sprite = sr;
            DetailDescription.text = "名称: " + Items[index].name + "\n" +
                                     "描述: " + Items[index].description;
        }
    }

    public void InitIllstration()
    {
        for (int i = 0; i < AllItem.Length; i++)
        {
            ItemDatabase[i] = new Item(AllItem[i].Item1, AllItem[i].Item2, AllItem[i].Item3,
                                        AllItem[i].Item4, AllItem[i].Item5, AllItem[i].Item6);
            Items.Add(ItemDatabase[i]);
        }
        UpdateItemImage();
        ShowDetail(0);
    }

    //刷新背包里的图片显示
    public void UpdateItemImage()
    {
        for (int i = 0; i < ItemImages.Length; i++)
        {
            ItemImages[i].sprite = null;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Sprite sr = Resources.Load<Sprite>("Npc/" + Items[i].name + "头像");
            ItemImages[i].sprite = sr;
        }
    }
}
