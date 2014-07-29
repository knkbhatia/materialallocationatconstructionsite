Imports Microsoft.VisualBasic
Imports System.IO

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net
Imports CrystalDecisions.Web

Public Class GenerateReport
    Public Shared Sub print(ByVal treport As String, ByVal ttype As String, ByVal ParamArray parameters() As String)
        Dim MyTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim MyLogin As CrystalDecisions.Shared.TableLogOnInfo
        Dim crReportDocument As ReportDocument
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim Fname As String = ""

        Dim tFilePath As String = HttpContext.Current.Server.MapPath("") & "\Reports\" & HttpContext.Current.Session("sUserID")

        crReportDocument = New ReportDocument
        crReportDocument.Load(HttpContext.Current.Server.MapPath("") + "\" & treport & ".rpt ")

        For Each MyTable In crReportDocument.Database.Tables
            MyLogin = MyTable.LogOnInfo
            MyLogin.ConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings("OUserId")
            MyLogin.ConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings("OPwd")
            MyLogin.ConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings("OServer")
            MyTable.ApplyLogOnInfo(MyLogin)
        Next

        Dim i As Integer
        For i = LBound(parameters) To UBound(parameters) Step 2
            Dim p_name As String = parameters(i).ToString
            Dim p_value As String = parameters(i + 1).ToString

            crReportDocument.SetParameterValue("" & p_name & "", "" & p_value & "")
        Next


        If ttype = "pdf" Then
            Fname = tFilePath & "\" & treport & ".pdf"
        ElseIf ttype = "xls" Or ttype = "xls-formated" Then
            Fname = tFilePath & "\" & treport & ".xls"
        End If

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If ttype = "pdf" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            ElseIf ttype = "xls" Then
                .ExportFormatType = ExportFormatType.ExcelRecord
            ElseIf ttype = "xls-formated" Then
                .ExportFormatType = ExportFormatType.Excel
            End If
        End With

        crReportDocument.Export()
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.ClearHeaders()
        crReportDocument.Dispose()

        Dim strFileName As String = ""
        If ttype = "pdf" Then
            strFileName = treport & ".pdf"
        ElseIf ttype = "xls" Or ttype = "xls-formated" Then
            strFileName = treport & ".xls"
        End If
        ViewReport(tFilePath, strFileName)
    End Sub

    Public Shared Sub ViewReport(ByVal reportPath As String, ByVal reportName As String)
        Dim Fname As String

        'reportPath = "F:\DCMSDevelopemnt\DCMS\Uploads\6922\ 000-081-083-084\ 16-49\ 001"
        'reportName = " ASP.NET2.0EverydayAppsforDummies.pdf"

        Dim intFileNameLength As Integer
        Dim file_ext, pos As String

        intFileNameLength = reportName.Length
        pos = InStr(1, StrReverse(reportName), ".")
        file_ext = Trim(Mid(reportName, (Len(reportName) - pos) + 2))

        Fname = reportPath & "\" & reportName & ""

        Dim file As FileInfo = New FileInfo(Fname)
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + "" & reportName & "")
        HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/octet-stream"

        HttpContext.Current.Response.TransmitFile(Fname)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.Close()
        HttpContext.Current.Response.End()
    End Sub
    Public Shared Sub PrintWoView(ByVal treport As String, ByVal ttype As String, ByVal ParamArray parameters() As String)
        Dim MyTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim MyLogin As CrystalDecisions.Shared.TableLogOnInfo
        Dim crReportDocument As ReportDocument
        Dim crExportOptions As ExportOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim Fname As String = ""

        Dim tFilePath As String = HttpContext.Current.Server.MapPath("") & "\Reports\" & HttpContext.Current.Session("sUserID")

        crReportDocument = New ReportDocument
        crReportDocument.Load(HttpContext.Current.Server.MapPath("") + "\" & treport & ".rpt ")

        For Each MyTable In crReportDocument.Database.Tables
            MyLogin = MyTable.LogOnInfo
            MyLogin.ConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings("OUserId")
            MyLogin.ConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings("OPwd")
            MyLogin.ConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings("OServer")
            MyTable.ApplyLogOnInfo(MyLogin)
        Next

        Dim i As Integer
        For i = LBound(parameters) To UBound(parameters) Step 2
            Dim p_name As String = parameters(i).ToString
            Dim p_value As String = parameters(i + 1).ToString

            crReportDocument.SetParameterValue("" & p_name & "", "" & p_value & "")
        Next


        If ttype = "pdf" Then
            Fname = tFilePath & "\" & treport & ".pdf"
        ElseIf ttype = "xls" Then
            Fname = tFilePath & "\" & treport & ".xls"
        End If

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            If ttype = "pdf" Then
                .ExportFormatType = ExportFormatType.PortableDocFormat
            ElseIf ttype = "xls" Then
                .ExportFormatType = ExportFormatType.ExcelRecord
            End If
        End With

        crReportDocument.Export()
        HttpContext.Current.Response.ClearContent()
        HttpContext.Current.Response.ClearHeaders()
        crReportDocument.Dispose()
    End Sub
End Class
