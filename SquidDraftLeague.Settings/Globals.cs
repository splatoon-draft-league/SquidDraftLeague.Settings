﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace SquidDraftLeague.Settings
{
    public static class Globals
    {
        public static List<ulong> SuperUsers => GetSuperUsers();

        private static List<ulong> GetSuperUsers()
        {
            if (!File.Exists(Path.Combine(AppPath, "Data", "superusers.json")))
            {
                File.WriteAllText(Path.Combine(AppPath, "Data", "superusers.json"), JsonConvert.SerializeObject(new List<ulong>(), Formatting.Indented));
            }

            return JsonConvert.DeserializeObject<List<ulong>>(File.ReadAllText(Path.Combine(AppPath, "Data", "superusers.json")));
        }

        /// <summary>
        /// Gets or sets the bots settings.
        /// </summary>
        public static Settings BotSettings { get; set; }

        /// <summary>
        /// Returns the root directory of the application.
        /// </summary>
        public static readonly string AppPath = Directory.GetParent(new Uri(Assembly.GetEntryAssembly()?.CodeBase).LocalPath).FullName;

        /// <summary>
        /// My implementation of a static random; as close to fully random as possible.
        /// </summary>
        public static Random Random => local ?? (local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));

        [ThreadStatic] private static Random local;
    }
}
