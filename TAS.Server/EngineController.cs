﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using TAS.Common;
using TAS.Common.Interfaces;
using TAS.Server.Security;

namespace TAS.Server
{
    public static class EngineController
    {

        private static List<CasparServer> _servers;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger(nameof(EngineController));

        public static List<Engine> Engines { get; private set; }

        public static IDatabase Database { get; private set; }

        public static void Initialize()
        {
            FtpTrace.AddListener(new NLog.NLogTraceListener());
            Logger.Info("Engines initializing");
            ConnectionStringSettings connectionStringPrimary = ConfigurationManager.ConnectionStrings["tasConnectionString"];
            ConnectionStringSettings connectionStringSecondary = ConfigurationManager.ConnectionStrings["tasConnectionStringSecondary"];
            Database = DatabaseProviderLoader.LoadDatabaseProvider();
            Logger.Debug("Connecting to database");
            Database.Open(connectionStringPrimary?.ConnectionString, connectionStringSecondary?.ConnectionString);
            _servers = Database.DbLoadServers<CasparServer>();
            _servers.ForEach(s =>
            {
                s.ChannelsSer.ForEach(c => c.Owner = s);
                s.RecordersSer.ForEach(r => r.SetOwner(s));
            });

            AuthenticationService authenticationService = new AuthenticationService(Database.DbLoad<User>(), Database.DbLoad<Group>());
            Engines = Database.DbLoadEngines<Engine>(ulong.Parse(ConfigurationManager.AppSettings["Instance"]));
            foreach (var e in Engines)
                e.Initialize(_servers, authenticationService);
            Logger.Debug("Engines initialized");
        }

        public static void ShutDown()
        {
            if (Engines != null)
                foreach (var e in Engines)
                    e.Dispose();
            Logger.Info("Engines shutdown");
        }

        public static int GetConnectedClientCount() => Engines.Sum(e => e.Remote?.ClientCount ?? 0);
    }
}
