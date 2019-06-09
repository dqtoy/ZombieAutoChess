﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ty
{
    //同步 位置 和 旋转 朝向 没有同步
    class msgRoomSyncObject : baseUserMsg
    {
        static msgRoomSyncObject g_msglogin;
        public static msgRoomSyncObject getMe()
        {
            if (msgRoomSyncObject.g_msglogin == null)
            {
                msgRoomSyncObject.g_msglogin = new msgRoomSyncObject();
            }
            return msgRoomSyncObject.g_msglogin;
        }

        public msgRoomSyncObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_SyncObject;
        }
        /// <returns>当前系统时间所对应的毫秒数</returns>
        public static ulong GetCurrentTimeByMiliSec()
        {
            DateTime t = DateTime.Now;
            ulong millisecond = ((((((ulong)t.Year * 12 + (ulong)t.Month) * 30 + (ulong)t.Day) * 24 + (ulong)t.Hour) * 60 + (ulong)t.Minute) * 60
                  + (ulong)t.Second) * 1000 + (ulong)(t.Millisecond);
            return millisecond;
        }


        // 
        public override void handle(JObject jo)
        {
            if (tyConofig.isRoomOwer() == true)
            {
                return;
            }

            msgRoomCreateObject.getMe().handle(jo);

            int srcid = Convert.ToInt32((string)jo["srcid"]);
            int objid = Convert.ToInt32((string)jo["objid"]);
            double x = Convert.ToDouble((string)jo["x"]);
            double y = Convert.ToDouble((string)jo["y"]);
            double z = Convert.ToDouble((string)jo["z"]);
            Vector3 pos = new Vector3((float)x, (float)y, (float)z);
          //  logMgr.log("myid" + tyConofig.g_roomid + "srcid" + srcid + "objid" + objid);


            //objid

            baseSyncObject _baseSyncObject = syncObjectMgr.getMe().getObject(objid);
            


            double rx = Convert.ToDouble((string)jo["qx"]);
            double ry = Convert.ToDouble((string)jo["qy"]);
            double rz = Convert.ToDouble((string)jo["qz"]);
            double rw = Convert.ToDouble((string)jo["qw"]);


            Quaternion rot = new Quaternion((float)rx, (float)ry, (float)rz, (float)rw);


             



            double sx = Convert.ToDouble((string)jo["sx"]);
            double sy = Convert.ToDouble((string)jo["sy"]);
            double sz = Convert.ToDouble((string)jo["sz"]);




            float   jiangetime       = (float)Convert.ToDouble((string)jo["jg"]);
            //long    _sendttnowServer = Convert.ToInt64((string)jo["now"]);


           // ulong _tttnow = GetCurrentTimeByMiliSec();
           // logMgr.log("间隔时间：" + (_tttnow - _sendttnowServer)/1000f/1000f);


            Vector3 scale = new Vector3((float)sx, (float)sy, (float)sz);
            if (_baseSyncObject != null)
            {
                _baseSyncObject.setFrame( pos,rot,  scale, jiangetime);
            }


        }



    }

}

