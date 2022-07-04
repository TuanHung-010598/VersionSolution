Attribute VB_Name = "mdlPort"
Option Explicit
Public strRom As String
Public strRoomNo As String
Public strInTime As String
Public strOutTime As String

Public Declare Function TP_Configuration Lib "LockSDK.dll" (ByVal iLockType As Integer) As Integer 'ѡ����������
'/*=============================================================================
'������:                         TP_Configuration
'int __stdcall TP_Configuration(int lock_type);
'�� ��: ��̬���ʼ������ , �����������ѡ�� / ���������ӵ�
'�� ��: lock_type --��������(Ҳ����ʹ�õĿ�Ƭ����)
'�� ��: ��
'����ֵ: ��������
'=============================================================================*/

Public Declare Function TP_MakeGuestCard Lib "LockSDK.dll" (ByVal card_snr As String, ByVal Room_No As String, ByVal checkinTime As String, ByVal checkOutTime As String, ByVal iFlags As Integer) As Integer
'/*=============================================================================
'������:                         TP_MakeGuestCard
'int __stdcall TP_MakeGuestCard(char *card_snr, char *room_no, char *checkin_time,char *checkout_time, int iflags);
'�� ��: �������Ϳ�
'��  �룺room_no         --  ����:       �ַ���, ���� "001.002.003.A"
 '       checkin_time    --  ��סʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
  '      checkout_time   --  Ԥ��ʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
   '     iFlags --���Ϳ�ѡ��, �μ�Defines�е�GUEST_FLAGS����, һ����0
'�� ��: card_snr --����:                 �ַ��� , ����Ԥ����20�ֽ�
'��  ��: Room="001.002.003.A", SDateTime="2008-06-06 12:30:59", EDateTime="2008-06-07 12:00:00"
 '       iFlags = 0
'����ֵ: ��������
'=============================================================================*/


Public Declare Function TP_CancelCard Lib "LockSDK.dll" (ByVal card_snr As String) As Integer
'/*=============================================================================
'������:                         TP_CancelCard
'int __stdcall TP_CancelCard(char *card_snr);;
'�����ܣ�ע����Ƭ/��Ƭ����
'�� ��: ��
'�� ��:
'�� ��: card_snr --����: �ַ��� , ����Ԥ����20�ֽ�
'����ֵ: ��������
'=============================================================================*/


Public Declare Function TP_ReadGuestCard Lib "LockSDK.dll" (ByVal card_snr As String, ByVal Room_No As String, ByVal checkinTime As String, ByVal checkout_time As String) As Integer
'/*=============================================================================
'������:                         TP_ReadGuestCard
'int __stdcall   TP_ReadGuestCard(char *card_snr,char *room_no, char *checkin_time, char *checkout_time);
'�� ��: �����Ϳ���Ϣ
'�� ��: ��?
'�� ��: card_snr --����:                 �ַ��� , ����Ԥ����20�ֽ�
 '       room_no --����:                 �ַ��� , ����Ԥ����20�ֽ�
  '      checkin_time    --  ��סʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss", ����Ԥ����30�ֽ�
   '     checkout_time   --  Ԥ��ʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss", ����Ԥ����30�ֽ�
'����ֵ: ��������
'=============================================================================*/


Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal pFileName As String) As Long
Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
 
'*************************************
   'Ŀ��:д��������Ini�ļ�
    
   '����: FileName �ļ���
   '      AppName  ��Ŀ��
   '      In_Key   ����
   '      In_Data  �����ϵ���ֵ
    
   '����:  д��ɹ� True
   '       д��ʧ�� False
    
'*************************************
 
Public Function WriteIniStr(ByVal FileName As String, ByVal AppName As String, ByVal In_Key As String, ByVal In_Data As String) As Boolean
On Error GoTo WriteIniStrErr
WriteIniStr = True
If VBA.Trim(In_Data) = "" Or VBA.Trim(In_Key) = "" Or VBA.Trim(AppName) = "" Then
   GoTo WriteIniStrErr
Else
   WritePrivateProfileString AppName, In_Key, In_Data, FileName
End If
Exit Function
WriteIniStrErr:
   Err.Clear
   WriteIniStr = False
End Function
 
 
'*************************************
   'Ŀ��:��Ini�ļ��ж�ȡ����
    
   '����: FileName �ļ���
   '      AppName  ��Ŀ��
   '      In_Key   ����
    
   '����: ȡ�ø��������ϵ�����
    
'*************************************
 
Public Function GetIniStr(ByVal FileName As String, ByVal AppName As String, ByVal In_Key As String) As String
On Error GoTo GetIniStrErr
If VBA.Trim(In_Key) = "" Then
   GoTo GetIniStrErr
End If
Dim GetStr As String
  GetStr = VBA.String(128, 0)
  GetPrivateProfileString AppName, In_Key, "", GetStr, 256, FileName
  GetStr = VBA.Replace(GetStr, VBA.Chr(0), "")
If GetStr = "" Then
   GoTo GetIniStrErr
Else
   GetIniStr = GetStr
   GetStr = ""
End If
Exit Function
GetIniStrErr:
   Err.Clear
   GetIniStr = ""
   GetStr = ""
End Function

 

Function CheckErr(ByVal st As Integer)
        '------------------------------------------------------------
        ' �������ܿ�:CheckErr(���غ�)
        '------------------------------------------------------------
    Select Case st
        Case 1
            MsgBox g_LoadString_Ex("IDS_STRING_SUCCESS"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
       Case -1
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_NOCARD"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -2
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_NOREADE"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -3
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_INVALIDCARD"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -4
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_CARDTYPE"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -5
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_READCARD"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -8
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_INPUT"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case -29
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR_REG"), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
       Case Else
          MsgBox g_LoadString_Ex("IDS_STRING_ERROR") + g_LoadString_Ex("IDS_STRING_ERROR_CODE") + CStr(st), vbInformation, g_LoadString_Ex("IDS_STRING_MSG")
          Exit Function
   End Select
End Function
