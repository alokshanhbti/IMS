Imports System.Configuration.ConfigurationManager
Imports System.Data
Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim connectionString As String = AppSettings("IMSConnectionString")
    Dim logger As log4net.ILog
    Dim Version As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
    Dim DomainName As String = System.Environment.UserDomainName
    Dim UserName As String = System.Environment.UserName
    Dim UserInteractive As Boolean = System.Environment.UserInteractive

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCellPhone.Focus()
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        logger = log4net.LogManager.GetLogger("IMS")
        log4net.Config.XmlConfigurator.Configure()
        logger.InfoFormat("IMS LookUp started - Ver: {0}; UserName:{1}/{2} [Interactive:{3}]", Version, DomainName, UserName, UserInteractive)

        lblError.Text = ""
        lblStatus.Text = ""
        lblStatus.ForeColor = Drawing.Color.Blue
        grdResults.DataSource = ""
        Dim searchText As String = txtCellPhone.Text
        grdResults.DataBind()
        logger.Info("The search text enterd is " + txtCellPhone.Text + ".")

        If IsNumeric(searchText) = False Then
            lblStatus.ForeColor = Drawing.Color.Red
            lblStatus.Text = "Cell phone number has invalid characters."
            logger.Info("The warning message is " + lblStatus.Text + ".")
            Exit Sub
        End If
        If searchText.Length > 10 Then
            lblStatus.ForeColor = Drawing.Color.Red
            lblStatus.Text = "Cell phone number cannot be greater than 10 digits."
            logger.Info("The warning message is " + lblStatus.Text + ".")
            Exit Sub
        End If
        If searchText.Length < 10 Then
            lblStatus.ForeColor = Drawing.Color.Red
            lblStatus.Text = "Cell phone number cannot be less than 10 digits."
            logger.Info("The warning message is " + lblStatus.Text + ".")
            Exit Sub
        End If

        Dim connection As New SqlClient.SqlConnection(connectionString)
        Try
            connection.Open()

            Dim sP As SqlClient.SqlCommand = New SqlClient.SqlCommand("sp_SearchLandtoWireless", connection)
            sP.CommandType = CommandType.StoredProcedure
            sP.CommandTimeout = 200

            Dim searchTextParam As SqlClient.SqlParameter = sP.Parameters.Add("@searchText", Data.SqlDbType.NVarChar)
            searchTextParam.Direction = Data.ParameterDirection.Input
            searchTextParam.Value = searchText

            grdResults.DataSource = sP.ExecuteReader
            grdResults.DataBind()

            connection.Close()

        Catch ex As Exception
            lblError.Text = ex.Message
            logger.Error("Exception occured : " + ex.Message)
            Exit Sub
        Finally
            connection.Close()
        End Try

        If grdResults.Rows.Count = 0 Then
            lblStatus.Text = "Match(es) on " & searchText & " for area code and prefix:"

            Try
                connection.Open()

                Dim sP As SqlClient.SqlCommand = New SqlClient.SqlCommand("sp_SearchBlockID", connection)
                sP.CommandType = CommandType.StoredProcedure
                sP.CommandTimeout = 200

                Dim npaParam As SqlClient.SqlParameter = sP.Parameters.Add("@NPA", Data.SqlDbType.NVarChar)
                npaParam.Direction = Data.ParameterDirection.Input
                npaParam.Value = searchText.Remove(3, 7)

                Dim nxxParam As SqlClient.SqlParameter = sP.Parameters.Add("@NXX", Data.SqlDbType.NVarChar)
                nxxParam.Direction = Data.ParameterDirection.Input
                nxxParam.Value = searchText.Remove(0, 3)
                nxxParam.Value = nxxParam.Value.Remove(3, 4)

                Dim xParam As SqlClient.SqlParameter = sP.Parameters.Add("@X", Data.SqlDbType.NVarChar)
                xParam.Direction = Data.ParameterDirection.Input
                xParam.Value = searchText.Remove(0, 6)
                xParam.Value = xParam.Value.remove(1, 3)

                grdResults.DataSource = sP.ExecuteReader
                grdResults.DataBind()

                If grdResults.Rows.Count = 0 Then
                    lblStatus.Text = "No match(es)."
                    logger.Info("No match(es).")
                Else
                    txtCellPhone.Text = ""
                    grdResults.HeaderRow.Cells(1).Text = "Area Code:"
                    grdResults.HeaderRow.Cells(2).Text = "Prefix:"
                    logger.Info("Matches for area code and prefix are displayed.")
                End If

                connection.Close()
            Catch ex As Exception
                lblError.Text = ex.Message
                logger.Error("Exception occured :" + ex.Message)
                Exit Sub
            End Try

        Else
            lblStatus.Text = grdResults.Rows.Count & " exact match(es) on phone number " & searchText & "."
            txtCellPhone.Text = ""
            logger.Info("The result is displayed .")
        End If
        logger.Info("[IMS LookUp END]")

    End Sub

End Class