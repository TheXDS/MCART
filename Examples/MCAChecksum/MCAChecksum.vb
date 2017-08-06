'
' MCAChecksum.vb
'
' Este archivo es un ejemplo sobre creación de plugins para MCART.
' MCAChecksum requiere MCART 0.6 (cualquier plataforma), pero ha sido compilado
' para Win32.
'
' Author:
'      César Andrés Morgan <xds_xps_ivx@hotmail.com>
'
' Copyright (c) 2011 - 2017 César Andrés Morgan
'
' MCART Is free software: you can redistribute it And/Or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, Or
' (at your option) any later version.
'
' MCART Is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If Not, see <http://www.gnu.org/licenses/>.

Imports System.IO
Imports MCART
Imports MCART.Common
Imports MCART.Attributes
Imports MCART.PluginSupport
Imports MCART.Security.Checksum
Imports MCART.Types.TaskReporter
Imports Microsoft.VisualBasic

<Description("CRC32 Calculator")>
<Beta>
<MinMCARTVersion(0, 6)>
<TargetMCARTVersion(0, 6)>
Public Class CRC32
    Inherits ChecksumPlugin
    Implements IDisposable
    Private disposedValue As Boolean
    Private CRC32_Tab(255) As UInteger
    Private CRCCheck As UInteger
    Private Const Seed As UInteger = 3988292384
    Private re As New TimeSpan(0, 0, 0, 0, 250)
    Public Overrides Function Compute(X() As Byte) As Byte()
        Try
            Reporter.Begin()
            Dim CRC As UInteger = UInteger.MaxValue
            Dim tm As Date = Date.Now
            Dim pr As Double
            Dim progrss As Integer = 1
            For Each BT As Byte In X
                pr = (progrss / X.Length) * 100
                If Reporter.CancelPending Then
                    Reporter.Stop(New ProgressEventArgs(pr))
                    Return {}
                Else
                    CRC = (CRC >> 8) Xor CRC32_Tab((CRC And &HFF) Xor BT)
                    progrss += 1
                    If Date.Now - tm > re Then
                        Reporter.Report(New ProgressEventArgs(pr))
                        tm = Date.Now
                    End If
                End If
            Next
            Reporter.End()
            Return BitConverter.GetBytes(Not CRC)
        Catch ex As Exception
            If Reporter.OnDuty Then Reporter.EndWithError(ex)
            Return {}
        End Try
    End Function
    Public Overrides Function Compute(X As Stream) As Byte()
        Try
            Reporter.Begin()
            Dim CRC As UInteger = UInteger.MaxValue
            Dim bt As Integer = X.ReadByte
            Dim tm As Date = Now
            Dim pr As Double
            Do Until bt = -1
                pr = (X.Position / X.Length) * 100
                If Reporter.CancelPending Then
                    Reporter.Stop(New ProgressEventArgs(pr))
                    Return {}
                Else
                    CRC = (CRC >> 8) Xor CRC32_Tab((CRC And &HFF) Xor bt)
                    bt = X.ReadByte
                    If Now - tm > re Then
                        Reporter.Report(New ProgressEventArgs(pr))
                        tm = Now
                    End If
                End If
            Loop
            Reporter.End()
            Return BitConverter.GetBytes(Not CRC)
        Catch ex As Exception
            If Reporter.OnDuty Then Reporter.EndWithError(ex)
            Return {}
        End Try
    End Function
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            Erase CRC32_Tab
        End If
        disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
    Public Sub New()
        Dim Seed As UInteger = CRC32.Seed
        Dim CRC As UInteger
        For i As Short = 0 To 255
            CRC = i
            For j As Byte = 0 To 7
                If (CRC And &H1) = &H1 Then
                    CRC = (CRC >> 1) Xor Seed
                Else
                    CRC = CRC >> 1
                End If
            Next
            CRC32_Tab(i) = CRC
        Next
#If DumpTable = True Then
        Dim x As New StreamWriter("CRCTab.txt")
        For Each j As UInteger In CRC32_Tab
            x.WriteLine("0x" & Hex(j))
        Next
        x.Flush()
        x.Close()
        x.Dispose()
        x = Nothing
#End If
        MyMenu.Add(New InteractionItem(AddressOf SampleCompute, "Calcular CRC...", "Permite calcular la CRC32 de una cadena de texto"))
        MyMenu.Add(New InteractionItem(Sub(a, b) About(Me), "Acerca de " & Name))
    End Sub
    Private Sub SampleCompute(a As Object, b As EventArgs)
        MsgBox(ToHex(Compute(InputBox("Introduzca una cadena"))))
    End Sub
End Class