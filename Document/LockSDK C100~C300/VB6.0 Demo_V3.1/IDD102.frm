VERSION 5.00
Begin VB.Form IDD102 
   Caption         =   "Demo"
   ClientHeight    =   5040
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   9000
   BeginProperty Font 
      Name            =   "����"
      Size            =   12
      Charset         =   134
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "IDD102.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5040
   ScaleWidth      =   9000
   StartUpPosition =   3  '����ȱʡ
   Begin VB.CommandButton IDD102_1011 
      Caption         =   "��סʱ��"
      Height          =   375
      Left            =   720
      TabIndex        =   16
      Top             =   2640
      Width           =   1215
   End
   Begin VB.CommandButton IDD102_1009 
      Caption         =   "��    ��"
      Enabled         =   0   'False
      Height          =   495
      Left            =   6120
      TabIndex        =   10
      Top             =   4200
      Width           =   1815
   End
   Begin VB.CommandButton IDD102_1005 
      Caption         =   "��   ��"
      Enabled         =   0   'False
      Height          =   495
      Left            =   3600
      TabIndex        =   9
      Top             =   4200
      Width           =   1815
   End
   Begin VB.CommandButton IDD102_1003 
      Caption         =   "��    ס"
      Enabled         =   0   'False
      Height          =   495
      Left            =   1080
      TabIndex        =   8
      Top             =   4200
      Width           =   1815
   End
   Begin VB.TextBox txtOutTime 
      Height          =   375
      Left            =   2160
      TabIndex        =   7
      Top             =   3360
      Width           =   6255
   End
   Begin VB.TextBox txtInTime 
      Height          =   375
      Left            =   2160
      TabIndex        =   5
      Top             =   2640
      Width           =   6255
   End
   Begin VB.TextBox txtRoomNo 
      Height          =   375
      Left            =   2160
      TabIndex        =   4
      Text            =   "001.002.028"
      Top             =   1920
      Width           =   6255
   End
   Begin VB.CommandButton IDD102_1002 
      Caption         =   "����SDK"
      Height          =   495
      Left            =   6600
      TabIndex        =   2
      Top             =   360
      Width           =   1815
   End
   Begin VB.OptionButton IDD102_1001 
      Caption         =   "5-RF50��"
      Height          =   375
      Left            =   4080
      TabIndex        =   1
      Top             =   360
      Value           =   -1  'True
      Width           =   2415
   End
   Begin VB.OptionButton IDD102_1000 
      Caption         =   "4-T57��"
      Height          =   375
      Left            =   960
      TabIndex        =   0
      Top             =   360
      Width           =   2295
   End
   Begin VB.Label IDD102_1012 
      AutoSize        =   -1  'True
      Caption         =   "Please enter the Lock Number here, not the Room Number! (Please refer to the help documents)"
      Height          =   480
      Left            =   720
      TabIndex        =   17
      Top             =   960
      Width           =   7800
      WordWrap        =   -1  'True
   End
   Begin VB.Label lblMsg3 
      AutoSize        =   -1  'True
      Height          =   240
      Left            =   5880
      TabIndex        =   15
      Top             =   5040
      Width           =   120
   End
   Begin VB.Label lblMsg2 
      AutoSize        =   -1  'True
      Height          =   240
      Left            =   5880
      TabIndex        =   14
      Top             =   4680
      Width           =   120
   End
   Begin VB.Label lblMsg1 
      AutoSize        =   -1  'True
      Height          =   240
      Left            =   5880
      TabIndex        =   13
      Top             =   4320
      Width           =   120
   End
   Begin VB.Label lblMsg 
      AutoSize        =   -1  'True
      Height          =   240
      Left            =   5880
      TabIndex        =   12
      Top             =   3960
      Width           =   120
   End
   Begin VB.Label lblCardNO 
      AutoSize        =   -1  'True
      Height          =   240
      Left            =   960
      TabIndex        =   11
      Top             =   4680
      Width           =   120
   End
   Begin VB.Label IDD102_1014 
      AutoSize        =   -1  'True
      Caption         =   "Ԥ��ʱ��"
      Height          =   240
      Left            =   720
      TabIndex        =   6
      Top             =   3360
      Width           =   960
   End
   Begin VB.Label IDD102_1013 
      AutoSize        =   -1  'True
      Caption         =   "������"
      Height          =   240
      Left            =   720
      TabIndex        =   3
      Top             =   1920
      Width           =   720
   End
End
Attribute VB_Name = "IDD102"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim iLockType As Integer '��������
Dim iret As Integer '����ֵ
Dim sRoomNo As String '����
Dim sInTime As String '��סʱ��
Dim sOutTime As String 'Ԥ��ʱ��
Dim sCardNo As String '����
Dim strMsg As String
 

 

 

Private Sub IDD102_1002_Click()
'���ö�̬�⣬���ӷ�����
'�ڶ�̬������Int����,��������Integer����
    If IDD102_1000.Value = True Then
        iLockType = 4
    Else
        If IDD102_1001.Value = True Then
            iLockType = 5
        End If
    End If
    
    
    iret = TP_Configuration(iLockType)
    
        CheckErr (iret)
  If iret = 1 Then
        Me.IDD102_1003.Enabled = True
        Me.IDD102_1005.Enabled = True
        Me.IDD102_1009.Enabled = True
        Me.IDD102_1011.Enabled = True
    End If
End Sub

Private Sub IDD102_1003_Click()
    '��ס
    '�ڶ�̬�������룬���char*�ͣ���������string�� ������Ҫ��space����ռ�
    sRoomNo = Space(100)
    sInTime = Space(100)
    sOutTime = Space(100)
    sCardNo = Space(100)
    
    lblMsg.Caption = ""
    lblMsg1.Caption = ""
    lblMsg2.Caption = ""
    lblMsg3.Caption = ""
    
    sRoomNo = Me.txtRoomNo.Text
    sInTime = Me.txtInTime.Text
    sOutTime = Me.txtOutTime.Text
    iret = TP_MakeGuestCard(sCardNo, sRoomNo, sInTime, sOutTime, 0)
     
    CheckErr (iret)
End Sub

Private Sub IDD102_1005_Click()
    '���������տ������ˣ�ע����)
     lblMsg.Caption = ""
    lblMsg1.Caption = ""
    lblMsg2.Caption = ""
    lblMsg3.Caption = ""
    sCardNo = Space(100)
    iret = TP_CancelCard(sCardNo)
     
    CheckErr (iret)
End Sub

 

Private Sub IDD102_1009_Click()
     sRoomNo = Space(100)
    sInTime = Space(100)
    sOutTime = Space(100)
    sCardNo = Space(100)
    Dim str As String
    Dim strtemp As String
    iret = TP_ReadGuestCard(sCardNo, sRoomNo, sInTime, sOutTime)
    If iret = 1 Then
       strtemp = Trim(sCardNo)
       strtemp = Left(strtemp, Len(strtemp) - 1)
       str = g_LoadString_Ex("IDS_STRING_CARDNO") & strtemp & vbCrLf
       strtemp = Trim(sRoomNo)
       strtemp = Left(strtemp, Len(strtemp) - 1)
       str = str & g_LoadString_Ex("IDS_STRING_LOCKNO") & strtemp & vbCrLf
       strtemp = Trim(sInTime)
       strtemp = Left(strtemp, Len(strtemp) - 1)
       str = str & g_LoadString_Ex("IDS_STRING_INTIME") & strtemp & vbCrLf
       strtemp = Trim(sOutTime)
       strtemp = Left(strtemp, Len(strtemp) - 1)
       str = str & g_LoadString_Ex("IDS_STRING_OUTTIME") & strtemp & vbCrLf
       MsgBox str, vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
      
    Else
        CheckErr (iret)
    End If
    
End Sub

Private Sub IDD102_1011_Click()
  txtInTime.Text = Format(Date, "yyyy-mm-dd") & " " & Format(Time, "hh:MM:ss")
End Sub

Private Sub Form_Load()
    Dim sHour As String
    Dim sMinute As String
    
    Me.IDD102_1000.Value = False
    Me.IDD102_1001.Value = True
   'txtInTime.Text = DateTime.Date + DateTime.Time
 txtInTime.Text = Format(Date, "yyyy-mm-dd") & " " & Format(Time, "hh:MM:ss")
    sMinute = " 12:00:00"
    
    txtOutTime.Text = Mid(txtInTime.Text, 1, 8) + CStr(DateTime.Day(DateTime.Date) + 1) + sMinute
    Call g_SetFormString_Ex(IDD102)
     
End Sub
 

Private Sub IDD102_1001_Click()
     iLockType = 5
    Me.IDD102_1000.Value = False
End Sub

Private Sub IDD102_1000_Click()
    iLockType = 4
    Me.IDD102_1001.Value = False
End Sub
