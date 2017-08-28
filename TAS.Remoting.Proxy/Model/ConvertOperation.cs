﻿using System;
using TAS.Common;
using TAS.Common.Interfaces;

namespace TAS.Remoting.Model
{
    public class ConvertOperation : FileOperation, IConvertOperation
    {
        public TAspectConversion AspectConversion { get { return Get<TAspectConversion>(); } set { Set(value); } }
        public TAudioChannelMappingConversion AudioChannelMappingConversion { get { return Get<TAudioChannelMappingConversion>(); } set { Set(value); } }
        public decimal AudioVolume { get { return Get<decimal>(); } set { Set(value); } }
        public TFieldOrder SourceFieldOrderEnforceConversion { get { return Get<TFieldOrder>(); } set { Set(value); } }
        public TimeSpan StartTC { get { return Get<TimeSpan>(); } set { Set(value); } }
        public TimeSpan Duration { get { return Get<TimeSpan>(); } set { Set(value); } }
        public bool Trim { get { return Get<bool>(); } set { Set(value); } }
        public bool LoudnessCheck { get { return Get<bool>(); } set { Set(value); } }
        
    }
}
