using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
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
        }

        private void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            client.Connect("localhost",51220);
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
                client.SayTo(x[0], x[1]);
            }
            client.Say(text);
        }
    }

    public class LightChatClient : SelfWiredCommandClient<Command, RetVal>
    {
        private MainWindow wnd;

        private void Write(string text)
        {
            wnd.TxtChat.Text += $"{text}\n";
        }
        
        public LightChatClient(MainWindow wnd)
        {
            this.wnd = wnd;
        }

        [Response(RetVal.Msg)]
        public void DoMsg(BinaryReader br)
        {
            Write(br.ReadString());
        }
        
        [Response(RetVal.Err)]
        [Response(RetVal.Unknown)]
        [Response(RetVal.InvalidCommand)]
        [Response(RetVal.InvalidLogin)]
        [Response(RetVal.InvalidInfo)]
        public void DoErr(BinaryReader br)
        {
            Write("El servidor ha encontrado un error.");
        }

        [Response(RetVal.Ok)]
        public void DoOk(BinaryReader br)
        {
            if (br.PeekChar() < 0) return;
            var c = br.ReadInt32();
            Write($"Hay {c} usuarios conectados:");
            for (var j = 0; j < c; j++)
            {
                Write(br.ReadString());
            }
        }
        
        [Response(RetVal.NoLogin)]
        public void DoNoLogin(BinaryReader br)
        {
            Write("No has iniciado sesión.");
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
                TalkToServer(Command.SayTo,ms.ToArray());
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
