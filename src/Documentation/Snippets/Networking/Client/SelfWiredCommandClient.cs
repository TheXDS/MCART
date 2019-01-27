/*
SelfWiredCommandClient.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it And/Or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, Or
(at your option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If Not, see <http:'www.gnu.org/licenses/>.

======================== N O T A   I M P O R T A N T E ========================

Este módulo contiene ejemplos de código para MCART, y no debe ser compilado ni
incluido en ninguna distribución de MCART. Su propósito es el de brindar un
espacio que permita la creación de documentación para compilar, por lo que el
código contenido aquí no está pensado para su distribución o uso en un entorno
de producción, sino más bien como una referencia a partir de la cual el
el desarrollador podrá aprender a utilizar la librería y crear su propio
código.
 */

#region example1
public enum CommandsEnum
{
    Hello,
    Message,
    Bye
}

public enum ResponsesEnum
{
    Ok,
    MessageResponse,
    [ErrorResponse] Fail
}

// Es necesario crear este atributo, debido a limitaciones de C#.
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
sealed class ResponseAttribute : Attribute, IValueAttribute<ResponsesEnum>
{
    public ResponseAttribute(ResponsesEnum value)
    {
        Value = value;
    }

    public ResponsesEnum Value { get; }
}

public class MyClient : SelfWiredCommandClient<CommandsEnum, ResponsesEnum>
{
    [Response(ResponsesEnum.Ok)]
    public void ServerOk(BinaryReader br)
    {
        Console.WriteLine("Comando ejecutado correctamente.");
    }

    [Response(ResponsesEnum.MessageResponse)]
    public void ServerMessage(BinaryReader br)
    {
        Console.WriteLine(br.ReadString());
    }

    [Response(ResponsesEnum.Fail)]
    public void ServerFail(BinaryReader br)
    {
        Console.WriteLine("El servidor ha encontrado un error.");
    }
}
#endregion
