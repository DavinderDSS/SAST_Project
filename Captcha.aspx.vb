Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Partial Class Captcha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objBitmap As Bitmap = New Bitmap(130, 80)
        Dim objGraphics As Graphics = Graphics.FromImage(objBitmap)
        objGraphics.Clear(Color.White)
        Dim objRandom = New Random
        objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50))
        objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20))
        objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80))
        Dim objBrush As Brush = Nothing
        ''//create background style
        Dim aHatchStyles = New HatchStyle() {HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical, HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal, HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal}

        ''//create rectangular area
        Dim oRectangleF As RectangleF = New RectangleF(0, 0, 300, 300)
        objBrush = New HatchBrush(aHatchStyles(objRandom.Next(aHatchStyles.Length - 3)), Color.FromArgb(objRandom.Next(100, 255), objRandom.Next(100, 255), objRandom.Next(100, 255)), Color.White)
        objGraphics.FillRectangle(objBrush, oRectangleF)
        ''//Generate the image for captcha
        Dim captchaText = String.Format("{0:X}", objRandom.Next(1000000, 9999999))
        ''//add the captcha value in session
        Session("CaptchaVerify") = captchaText.ToLower
        Dim objFont As Font = New Font("Courier New", 15, FontStyle.Bold)
        ''//Draw the image for captcha  
        objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20)
        objBitmap.Save(Response.OutputStream, ImageFormat.Gif)

    End Sub
End Class
