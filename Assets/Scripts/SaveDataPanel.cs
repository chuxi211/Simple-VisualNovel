using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataPanel : MonoBehaviour
{
    private GameObject BoxPrefab;
    private List<LoadDataBox> Lboxs= new List<LoadDataBox>();
    private List<SaveDataBox> Sboxs= new List<SaveDataBox>();
    
    private void Awake()
    {
        BoxPrefab = Resources.Load<GameObject>("SaveData/S.L.DPrefab");
    }
    private void Start()
    {
        CreatBoxs();
    }
    
    private void CreatBoxs()
    {
        Sboxs.Clear();
        Lboxs.Clear();
        for (int i = 0; i < SaveManager.slotsPerPage; i++)
        {
            
            GameObject obj=Instantiate(BoxPrefab, transform);
            SaveDataBox sbox = obj.GetComponentInChildren<SaveDataBox>();
            LoadDataBox lbox = obj.GetComponentInChildren<LoadDataBox>();
            if (sbox == null)
            {
                Debug.LogError("SaveDataBox component is missing in the prefab.");
                continue;
            }
            sbox.Init(this, i);
            lbox.Init(this, i);
            Sboxs.Add(sbox);
            Lboxs.Add(lbox);
        }
    }
    public void Save(int visualIndex)
    {
        SaveManager.SaveData(SaveManager.currentPage, visualIndex);
    }
    public void Load(int visualIndex)
    {
        SaveManager.LoadData(SaveManager.currentPage, visualIndex);
        Debug.Log("LoadDataPanel Load called, visualIndex: " + visualIndex);
    }
}
