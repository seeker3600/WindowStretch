﻿using System;
using WindowStretch.Model;

#pragma warning disable IDE1006 // 命名スタイル

namespace WindowStretch.Main
{
    public partial class MainForm
    {
        private readonly StartModel SttVm = new StartModel();

        private void SetupStartModel()
        {
            appUriTxt.DataBindings.Add(Bind(nameof(appUriTxt.Text), SttVm.Uri));
            startWithMeChk.DataBindings.Add(Bind(nameof(startWithMeChk.Checked), SttVm.StartWithMe));
            SttVm.StatusMsg.Subscribe(StatusDrain);

            SttVm.Load();

            FormClosed += (_, __) => SttVm.Save();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            SttVm.Start();
        }
    }
}
