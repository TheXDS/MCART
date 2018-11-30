/*
MainWindow.xaml.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Windows;
using TheXDS.MCART.Dialogs;
using static TheXDS.MCART.UI;

namespace TheXDS.LightChat
{
    public interface ITerminal
    {
        void Write(string text);
        void WriteLine(string text);
    }

    /// <inheritdoc cref="Window"/>
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ITerminal
    {
        private readonly LightChatClient _client;
        public MainWindow()
        {
            InitializeComponent();
            _client = new LightChatClient(this);
            this.HookHelp((sender, e) => AboutBox.ShowDialog(GetType().Assembly));
        }

        private async void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            if (await _client.ConnectAsync(TxtServer.Text, 51220))
                _client.Login(TxtSend.Text, new byte[0]);
            else
                WriteLine("No fue posible conectarse al servidor.");
        }
        private void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtSend.Text.StartsWith("#logout"))
            {
                var x = TxtSend.Text.Split(new[] { ' ' }, 2);
                if (x.Length == 2)
                {
                    DoSend(x[1]);
                }
                _client.Logout();
                return;
            }

            if (TxtSend.Text == "#list")
            {
                _client.TalkToServer(Command.List);
                return;
            }
            DoSend(TxtSend.Text);
        }

        private void DoSend(string text)
        {
            if (text.StartsWith("@"))
            {
                var x = text.Split(new[] { ' ' }, 2);
                if (x.Length < 2) return;
                _client.SayTo(x[0].Substring(1), x[1]);
            }
            else _client.Say(text);
        }

        public void Write(string text)
        {
            if (CheckAccess()) TxtChat.Text += text;
            else Dispatcher.Invoke(() => Write(text));
        }

        public void WriteLine(string text) => Write($"{text}\n");
    }
}