using System;
using TheXDS.MCART.Cmd;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Examples.Consoleer
{
    internal static class Program
    {
        private class HelpArgument : HelpArgumentBase
        {
            public override void Run(CmdLineParser args)
            {
                base.Run(args);
                Environment.Exit(0);
            }
        }
        
        private class DetailArgument : Argument
        {
            public override string Summary => "Establece el nivel de detalle de la salida. Los valores válidos son: Little, ALot. Si no se establece un valor, se utilizará una longitud media.";
            public override ValueKind Kind => ValueKind.ValueRequired;
        }
        private class VersionArgument : Argument
        {
            public override string Summary => "Muestra la versión del programa y sale.";
            public override char? ShortName => 'v';
            public override void Run(CmdLineParser args)
            {
                Helpers.About();
                Environment.Exit(0);
            }
        }

        private static void Main(string[] args)
        {
            var a = new CmdLineParser(args);
            a.AutoRun();

            switch (a.Value<DetailArgument>()?.ToLower())
            {
                case "little":
                    Console.WriteLine("Hola");
                    break;
                case "alot":
                    Console.WriteLine("Hola, usuario. Bienvenido a Consoleer. Este programa no realiza ninguna acción importante.");
                    break;
                case "":
                    Console.WriteLine("Hola, mundo.");
                    break;
                default:
                    Console.WriteLine("Nivel de detalle inválido.");
                    
                    break;
            }
        }
    }
}
