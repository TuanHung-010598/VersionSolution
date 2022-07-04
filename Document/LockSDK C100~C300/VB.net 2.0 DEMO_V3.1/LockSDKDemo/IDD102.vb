Imports System.Text

Public Class IDD102
    '*************************************************************************************************
    '���ö�̬����ĺ���
    Declare Function TP_Configuration Lib "LockSDK.dll" (ByVal DoorType As Integer) As Integer '����SDK

    Declare Function TP_MakeGuestCard Lib "LockSDK.dll" (ByVal sCardNo As StringBuilder, ByVal sRoomNo As String, ByVal strInTime As String, ByVal strOutTime As String, ByVal iFlags As Integer) As Integer '��ס

    Declare Function TP_ReadGuestCard Lib "LockSDK.dll" (ByVal sCardNo As StringBuilder, ByVal sRoomNo As StringBuilder, ByVal strInTime As StringBuilder, ByVal strOutTime As StringBuilder) As Integer '����

    Declare Function TP_CancelCard Lib "LockSDK.dll" (ByVal sCardNo As StringBuilder) As Integer '����

    '*************************************************************************************************
    '����

    Dim st As Integer
    Dim DoorType As Integer
    Dim strInTime As String
    Dim strOutTime As String
    Dim strRoomNo As String
    Dim strCardNo As String
    Dim strMsg As String
    Dim la As Language = New Language
    '****************************************************************************************************
    '��������
    Private Function CheckErr(ByVal intErr As Integer) As String
        Dim strMsg As String = ""
        Select Case intErr
            Case 1
                strMsg = la.g_LoadString_Ex("IDS_STRING_SUCCESS")
            Case -1
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_NOCARD")
            Case -2
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_NOREADE")
            Case -3
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_INVALIDCARD")
            Case -4
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_CARDTYPE")
            Case -5
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_READCARD")
            Case -8
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_INPUT")
            Case -29
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR_REG")
            Case Else
                strMsg = la.g_LoadString_Ex("IDS_STRING_ERROR")
        End Select
        Return strMsg
    End Function
 
 

    Private Sub IDD102_1002_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDD102_1002.Click
        '���ö�̬��, �����ӷ�����
        '�ڶ�̬����int���� ��������Integer����
        If (IDD102_1000.Checked) Then
            DoorType = 4
        ElseIf (IDD102_1001.Checked) Then
            DoorType = 5
        End If
        st = TP_Configuration(DoorType)
        If (st <> 1) Then
            MsgBox(CheckErr(st), vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
            Return
        End If
        IDD102_1003.Enabled = True
        IDD102_1005.Enabled = True
        IDD102_1009.Enabled = True
        IDD102_1011.Enabled = True
        MsgBox(CheckErr(st), vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
    End Sub

    Private Sub IDD102_1003_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDD102_1003.Click
        '��ס
        '�ڶ�̬�������char*�� ����������StringBUilder
        '�ڶ�̬��������char*�ͣ���������string��
        Dim sCardNo As StringBuilder = New StringBuilder()
        strRoomNo = Me.txtRoomNo.Text
        strInTime = Me.txtInTime.Text
        strOutTime = Me.txtOutTime.Text
        st = TP_MakeGuestCard(sCardNo, strRoomNo, strInTime, strOutTime, 0)
        'If (st = 1) Then
        '    lblMsg.Text = la.g_LoadString_Ex("IDS_STRING_CARDNO") + sCardNo.ToString()
        'End If
        MsgBox(CheckErr(st), vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
    End Sub

    Private Sub IDD102_1005_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDD102_1005.Click
        '����
        '�ڶ�̬�������char*�� ����������StringBUilder
        Dim sCardNo As StringBuilder = New StringBuilder()
        st = TP_CancelCard(sCardNo)
        If (st = 1) Then
            'lblMsg.Text = la.g_LoadString_Ex("IDS_STRING_CARDNO") + sCardNo.ToString()
        End If
        MsgBox(CheckErr(st), vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
    End Sub

    Private Sub IDD102_1009_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDD102_1009.Click
        '����
        '�ڶ�̬�������char*�� ����������StringBUilder
        '�ڶ�̬��������char*�ͣ���������string��
        Dim sInTime As StringBuilder = New StringBuilder()
        Dim sOutTime As StringBuilder = New StringBuilder()
        Dim sRoomNo As StringBuilder = New StringBuilder()
        Dim sCardNo As StringBuilder = New StringBuilder()
        st = TP_ReadGuestCard(sCardNo, sRoomNo, sInTime, sOutTime)

        strMsg = la.g_LoadString_Ex("IDS_STRING_CARDNO") + sCardNo.ToString() + Chr(10)
        strMsg += la.g_LoadString_Ex("IDS_STRING_LOCKNO") + sRoomNo.ToString() + Chr(10)
        strMsg += la.g_LoadString_Ex("IDS_STRING_INTIME") + sInTime.ToString() + Chr(10)
        strMsg += la.g_LoadString_Ex("IDS_STRING_OUTTIME") + sOutTime.ToString() + Chr(10)
        If st = 1 Then
            MsgBox(strMsg, vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
            Return
        Else
            MsgBox(CheckErr(st), vbInformation, la.g_LoadString_Ex("IDS_STRING_MSG"))
        End If

    End Sub

    Private Sub IDD102_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sHour As String

        la.g_SetFormStrings_Ex(Me)

        DoorType = 5
        Me.txtRoomNo.Text = "001.002.00028"
        Me.txtInTime.Text = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        sHour = "12:00:00"
        Me.txtOutTime.Text = Date.Now.Year.ToString + "-" + Date.Now.Month.ToString() + "-" + (Date.Now.Day + 1).ToString() + " " + sHour
    End Sub

    Private Sub IDD102_1011_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDD102_1011.Click
        Me.txtInTime.Text = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub
End Class
