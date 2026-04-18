using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager 
{
    //逻辑类，调用SaveAndLoad类的方法，计算视觉索引和实际索引
    //是UI和SaveAndLoad的桥梁

    private const int totalSlots = 32;//总存档位数量
    private const int totalPages = 4;//总页数,暂定这两个数
    public const int slotsPerPage = totalSlots / totalPages;
    //从0开始计数
    public static int currentPage ;
    public static int RIndexToVIndex(int realindex)
    {
        int visualindex = realindex% slotsPerPage;
        return visualindex;
    }
    public static int VIndexToRIndex( int currentpage,int visualindex)
    {
        int realindex = currentpage* slotsPerPage + visualindex;
        return realindex;
    }
    //先处理页数，再处理索引
    public static int RIndexToPage(int realIndex)
    {
        return (int) realIndex / slotsPerPage;//显式转换向下取整
    }
    public static void NextPage()
    {
        if(currentPage < totalPages - 1)
        {
            currentPage++;
        }
        Debug.Log($"currentPage:{currentPage}");
    }
    public static void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
        Debug.Log($"currentPage:{currentPage}");
    }
    public static void SaveData(int pageIndex,int visualIndex)
    {
        SaveAndLoad.Save(VIndexToRIndex(pageIndex,visualIndex), CoreController.Instance.StoryController.currentID);
    }
    public static void LoadData(int pageIndex,int visualIndex)
    {
        DataOfSave dataOfSave= SaveAndLoad.Load(VIndexToRIndex(pageIndex, visualIndex));
        EventBus.Publish(new LoadDataEvent(dataOfSave.currentID));
    }
}
