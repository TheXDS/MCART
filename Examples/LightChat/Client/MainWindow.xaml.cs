using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using TheXDS.MCART.Dialogs;
using TheXDS.MCART.Networking.Client;

namespace TheXDS.LightChat
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LightChatClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new LightChatClient(this);
            TheXDS.MCART.UI.HookHelp(this, (sender, e) =>
            {
                AboutBox.ShowDialog(GetType().Assembly);
            });
        }

        private void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            client.Connect(TxtServer.Text,51220);
            client.Login(TxtSend.Text, new byte[0]);
        }
        private void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtSend.Text.StartsWith("#logout"))
            {
                var x = TxtSend.Text.Split(new []{' '}, 2);
                if (x.Length == 2)
                {
                    DoSend(x[1]);
                }
                client.Logout();
                return;
            }

            if (TxtSend.Text == "#list")
            {
                client.TalkToServer(Command.List);
                return;
            }
            DoSend(TxtSend.Text);
        }

        private void DoSend(string text)
        {
            if (text.StartsWith("@"))
            {
                var x = text.Split(new []{' '}, 2);
                if (x.Length < 2) return;
                client.SayTo(x[0].Substring(1), x[1]);
            }
            else client.Say(text);
        }
    }

    public class LightChatClient : SelfWiredCommandClient<Command, RetVal>
    {
        private MainWindow wnd;

        private static void Write(LightChatClient instance, string text)
        {
            instance.wnd.Dispatcher.Invoke(() =>
            {
                instance.wnd.TxtChat.Text += $"{text}\n";
            });
        }
        
        public LightChatClient(MainWindow wnd)
        {
            this.wnd = wnd;
        }

        [Response(RetVal.Msg)]
        public static void DoMsg(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, br.ReadString());
        }
        
        [Response(RetVal.Err)]
        [Response(RetVal.Unknown)]
        [Response(RetVal.InvalidCommand)]
        [Response(RetVal.InvalidLogin)]
        [Response(RetVal.InvalidInfo)]
        public static void DoErr(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, "El servidor ha encontrado un error.");
        }

        [Response(RetVal.Ok)]
        public static void DoOk(object instance, BinaryReader br)
        {
            if (br.PeekChar() < 0) return;
            var c = br.ReadInt32();
            Write(instance as LightChatClient, $"Hay otros {c} usuarios conectados:");
            for (var j = 0; j < c; j++)
            {
                Write(instance as LightChatClient, br.ReadString());
            }
        }
        
        [Response(RetVal.NoLogin)]
        public static void DoNoLogin(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, "No has iniciado sesión.");
        }

        public void Say(string text)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(text);
                TalkToServer(Command.Say, ms);
            }
        }
        public void SayTo(string dest,string text)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(dest);
                bw.Write(text);
                TalkToServer(Command.SayTo, ms.ToArray());
            }
        }
        public void Logout()
        {
            TalkToServer(Command.Logout);
        }

        public void Login(string user, IEnumerable<byte> password)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(user);
                bw.Write(password.ToArray());
                TalkToServer(Command.Login, ms);
            }
        }
    }
}