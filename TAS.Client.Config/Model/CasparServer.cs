﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TAS.Server.Common;
using TAS.Server.Common.Interfaces;

namespace TAS.Client.Config.Model
{
    public class CasparServer: IPlayoutServerProperties
    {
        [XmlIgnore]
        public bool IsNew = true;
        public string ServerAddress { get; set; }
        public int OscPort { get; set; }
        public string MediaFolder { get; set; }
        public string AnimationFolder { get; set; }
        [XmlIgnore]
        public ulong Id { get; set; }
        public TServerType ServerType { get; set; }
        public List<CasparServerChannel> Channels { get; set; } = new List<CasparServerChannel>();
        public List<CasparRecorder> Recorders { get; set; } = new List<CasparRecorder>();
        public override string ToString()
        {
            return ServerAddress;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }

}
