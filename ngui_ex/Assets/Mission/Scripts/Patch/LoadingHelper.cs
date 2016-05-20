﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

public class LoadingHelper : MonoBehaviour
{

    //private readonly object lockObject = new object();

    private Dictionary<string, int> progressDic = new Dictionary<string, int>();
    int count = 0;
    int beforeCount = 0;
    int max = 0;
    bool updated = false;

    public void SetDownloadList(string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            progressDic.Add(list[i], 0);
        }
        max = progressDic.Count * 100;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Repo(int percent, string name)
    {
        //lock (lockObject)
        //{
            progressDic[name] = percent;
        //}
    }

    public void ReportDownloaded(string name)
    {
            updated = true;
            //progressDic[name] = 100;
    }

    private int test() {
        //lock (lockObject)
        //{
            int count = 0;
            foreach (int percent in progressDic.Values)
            {
                count += percent;
            }
            return count;
        //}
    }
    
    void FixedUpdate()
    {
        try
        {

            count = 0;
            if (max != 0)
            {

                count = test();


                if (beforeCount != count)
                {
                    GuiMgr.GetInst().OnPatchProgressChanged((float)count / (float)max);

                    if (count >= max)
                    {
                        AssetBundleMgr.GetInst().Complete();
                        GuiMgr.GetInst().OnPatchCompleted();
                        Destroy(this.gameObject);
                        return;
                    }
                    else if (updated)
                    {
                        AssetBundleMgr.GetInst().Down();
                        updated = false;
                    }
                    beforeCount = count;
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally {

        }


    }

}