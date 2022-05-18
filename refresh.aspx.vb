
Partial Class refresh
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.AddHeader("Refresh", "120; URL=Refresh.aspx")
    End Sub
End Class
