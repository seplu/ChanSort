﻿using System.Collections.Generic;
using ChanSort.Api;

namespace ChanSort.Loader.M3u
{
   internal class Channel : ChannelInfo
   {
     public List<string> Lines { get; }
     public int ExtInfTrackNameIndex { get; set; }
     public int ExtInfParamIndex { get; set; }

     public Channel(int index, int progNr, string name, List<string> lines) : base(SignalSource.IpAll, index, progNr, name)
     {
       this.Lines = lines;
     }
   }
}
