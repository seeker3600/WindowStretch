﻿using Reactive.Bindings;
using System;
using System.Diagnostics;
using WindowStretch.Properties;

namespace WindowStretch.Model
{
    using static Settings;

    public class StartModel
    {
        public ReactivePropertySlim<string> Uri { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<bool> StartWithMe { get; } = new ReactivePropertySlim<bool>();

        public ReactivePropertySlim<string> Status { get; } = new ReactivePropertySlim<string>();

        public void Load()
        {
            Uri.Value = Default.StartAppUri;
            StartWithMe.Value = Default.StartWithMe;

            if (StartWithMe.Value) Start();
        }

        public void Save()
        {
            Default.StartAppUri = Uri.Value;
            Default.StartWithMe = StartWithMe.Value;

            Default.Save();
        }

        public void Start()
        {
            try
            {
                var info = new ProcessStartInfo(Uri.Value)
                {
                    UseShellExecute = true
                };

                Process.Start(info);

                Status.Value = "アプリを起動しました。";
            }
            catch (Exception)
            {
                Status.Value = "アプリの起動に失敗しました。タイプミス、管理者権限などを確認してください。";
            }
        }
    }
}
