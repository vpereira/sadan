Imports System.Xml
Imports System.Xml.Schema

Public Class Bot

    Public WithEvents oSkype As New SKYPE4COMLib.Skype
    Public gdoc As New XmlDocument()

    Private Sub Bot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim AppName As String

        Dim oApplication As New SKYPE4COMLib.Application
        Dim oApplicationStream As New SKYPE4COMLib.ApplicationStream
        Dim user As SKYPE4COMLib.User

        ''Init some variables

        Dim cUserStatus_Offline = oSkype.Convert.TextToUserStatus("OFFLINE")
        Dim cUserStatus_Online = oSkype.Convert.TextToUserStatus("ONLINE")


        ''Start Skype
        If Not oSkype.Client.IsRunning Then
            Try
                oSkype.Client.Start(True, True)
            Catch ex As Exception
                Debug.WriteLine("Skype Start Failed:" + ex.Message)
            End Try

        End If

        '' Wait Skype startup and attach
        System.Threading.Thread.Sleep(1000)
        Try
            oSkype.Attach(5, True)
        Catch ex As Exception
            Debug.WriteLine("Attach failed:" + ex.Message)
        End Try


        ''Create application
        oApplication = oSkype.Application("SSD")
        oApplication.Create()
        AppName = oApplication.Name

        Debug.WriteLine(AppName)

        '' End of the copy and paste programming! LOL
    End Sub





    Private Sub oSkype_ApplicationDatagram(ByVal pApp As SKYPE4COMLib.Application, ByVal pStream As SKYPE4COMLib.ApplicationStream, ByVal Text As String) Handles oSkype.ApplicationDatagram
        Dim aCall, bcall, ccall As New SKYPE4COMLib.Call
        Dim tString As New System.Text.StringBuilder

        If XML_verify(Text) = True Then
            MsgBox(gdoc.ToString)
        End If
    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        SSDClientNotifyIcon.Visible = False
        End
    End Sub

    Public Function XML_verify(ByVal text As String)

        Try

            Dim Xml_StringReader As New IO.StringReader(text)
            Dim vrsettings As New XmlReaderSettings

            vrsettings.Schemas.Add("http://www.fucs.org/ssd_message_schema.xsd", New XmlTextReader(Me.GetType.Assembly.GetManifestResourceStream("SSD_Client.ssd_message_schema.xsd")))
            vrsettings.ValidationType = ValidationType.Schema
            AddHandler vrsettings.ValidationEventHandler, AddressOf ValidationEventHandler

            gdoc.Load(XmlReader.Create(New XmlTextReader(Xml_StringReader), vrsettings))
            gdoc.Validate(AddressOf ValidationEventHandler)

            Return True

        Catch ex As Exception
            'Hmmm, something wrong with the XML...
            MsgBox("Exception: " + ex.Message.ToString)
            Return False
        End Try

    End Function

    Public Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
        Select Case args.Severity
            Case XmlSeverityType.Error
                MsgBox("Error: " + args.Message)
            Case XmlSeverityType.Warning
                MsgBox("Warning: " + args.Message)
        End Select

    End Sub



End Class
