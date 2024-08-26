using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebPacket 
{
    [Serializable]
    public class ScrapPacketReq
    {
        public string userId;
        public string token;
        public int scrap;
    }

    [Serializable]
    public class ScrapPacketRes
    {
        public bool success;
    }
}
