Attribute VB_Name = "Lanaguage"
Option Explicit
Public g_szCurPath As String
Public g_szLanguagePath As String
Public sLanguage As String

'ȡ��ǰ·��
Public Sub GetLanguagePath_Ex()
    g_szCurPath = App.Path
   ' sLanguage = GetIniStr(g_szCurPath + "\Config.ini", "System", "Language")
    g_szLanguagePath = g_szCurPath + "\ToolsLanguage.ini"
End Sub

'<summary>
'���ݱ�ʶszID��ѡ���������ļ��м����ַ���
'</summary>
'<param name="szID"></param>
'<returns></returns>
Public Function g_LoadString_Ex(ByVal szID As String) As String
    Dim szValue As String
    Dim iret As Integer
    
    If g_szLanguagePath = "" Then
        Call GetLanguagePath_Ex
    End If
    
    szValue = GetIniStr(g_szLanguagePath, "String", szID)
    g_LoadString_Ex = szValue
End Function

'<summary>
'���Ի�������ʱ��ȡ�����пɵõ����ַ����������浽�����ļ���
'ÿ���ؼ��öԻ���ID�Ϳؼ�IDΨһ��ʶ
'</summary>
'<param name="frm"></param>
Public Sub g_SetFormString_Ex(ByVal frm As Form)
    Dim szSection As String '���ڵ�����
    Dim szKey As String '�ӿؼ�������
    Dim szText As String '�ӿؼ���Caption
    Dim iret As Integer
    Dim bSetText As Boolean 'true ���ļ��ж������ô��ڣ�false:�ӶԻ��򱣴浽�ļ�
    Dim cl As Control
    
    szSection = frm.Name
    bSetText = True
    If g_szLanguagePath = "" Then
        Call GetLanguagePath_Ex
    End If
    
   ' iret = PF_SetIniPath_Ex(g_szLanguagePath)
    If bSetText Then '���ļ��ж������ڿؼ���ȥ
        '���ڱ���
        szKey = szSection + "_Title"
        szText = GetIniStr(g_szLanguagePath, "LockSDKDemo", szKey)
        If szText = "" Then
            szText = "Not Found"
        End If
        frm.Caption = szText
        
        '�����ӿؼ�����
        For Each cl In frm.Controls
            szKey = cl.Name ' szSection + "_" + cl.Name
            szText = GetIniStr(g_szLanguagePath, "LockSDKDemo", szKey)
            If szText <> "" Then
                cl.Caption = szText
            End If
        Next
    Else '�Ӵ��ڱ��浽�ļ�
        szKey = szSection + "_Title"
        szText = frm.Caption
        If WriteIniStr(g_szLanguagePath, "LockSDKDemo", szKey, szText) Then
            For Each cl In frm.Controls
                szKey = szSection + "_" + cl.Name
                szText = cl.Caption
              If WriteIniStr(g_szLanguagePath, "LockSDKDemo", szKey, szText) = False Then
                Exit For
              End If
            Next
        End If
    End If
    
    
End Sub


