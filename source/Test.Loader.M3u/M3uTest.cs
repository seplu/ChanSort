﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ChanSort.Api;

namespace Test.Loader.M3u
{
  [TestClass]
  public class M3uTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      var m3uFile = TestUtils.DeploymentItem("Test.Loader.M3u\\TestFiles\\example.m3u");
      var refFile = TestUtils.DeploymentItem("Test.Loader.M3u\\TestFiles\\example-ref.txt");

      var loader = new ChanSort.Loader.M3u.M3uPlugin();
      var ser = loader.CreateSerializer(m3uFile);
      ser.Load();
      Assert.IsNotNull(ser);
      var root = ser.DataRoot;
      Assert.IsNotNull(root);

      root.ApplyCurrentProgramNumbers();
      var lists = root.ChannelLists.ToList();
      Assert.AreEqual(1, lists.Count);
      var chans = lists[0].Channels;
      Assert.AreEqual(6, chans.Count);
      Assert.AreEqual("Russia Today", chans[0].Name);
      Assert.AreEqual(1, chans[0].NewProgramNr);
      Assert.AreEqual("MP4", chans[5].Name);
      Assert.AreEqual(6, chans[5].NewProgramNr);


      var refLoader = new RefSerializerPlugin();
      var refSer = refLoader.CreateSerializer(refFile);
      refSer.Load();
      var ed = new Editor();
      ed.DataRoot = ser.DataRoot;
      ed.ChannelList = lists[0];
      //ed.ApplyReferenceList(refSer.DataRoot);
      ed.ApplyReferenceList(refSer.DataRoot, refSer.DataRoot.ChannelLists.First(), lists[0], false, 0, null, true, false);

      Assert.AreEqual(1, chans[5].NewProgramNr);
      Assert.AreEqual(2, chans[4].NewProgramNr);
    }
  }
}