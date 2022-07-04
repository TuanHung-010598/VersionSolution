#include "stdafx.h"
#include "PubFuns.h"

#define RESIDTOSTR(nResID) #nResID
CString g_szCurPath="";
char szLanguage[100];
//ȡ����Ҫ�õ������Ե�ini�ļ�
void GetLanguagePath_Ex(void)
{  
	g_szLanguagePath ="ToolsLanguage.ini";
 	strcpy(szLanguage,g_szLanguagePath);
  
}
/*********************************************************************
 * ��������:g_LoadString_Ex
 * ˵��:	���ݱ�ʶ szID��ѡ���������ļ��м����ַ���
 * ����:	zs
*********************************************************************/
CString g_LoadString_Ex(char* szID)
{
	char szValue[100]; 
	if (g_szLanguagePath == "") 
		GetLanguagePath_Ex();  
	PF_SetIniPath_Ex(szLanguage);
	PF_ReadIniString_Ex("String",szID,szValue,sizeof(szValue));
	if(szValue=="")
	{
		PF_ReadIniString_Ex("String","IDS_STRING_NOTFOUND",szValue,sizeof(szValue));
	}
	return szValue;
}
/*********************************************************************
 * ��������:g_SetDialogStrings_Ex(CDialog *pDlg,UINT uDlgID)
 * ˵��:	���Ի�������ʱ��ȡ�����пɵõ����ַ����������浽�����ļ���
			ÿ���ؼ��öԻ���ID�Ϳؼ�IDΨһ��ʶ

 * ��ڲ���:
 * CDialog *pDlg -- �Ի����ָ��
 *  UINT uDlgID -- �öԻ����ID
 * ����: zs 
*********************************************************************/
void g_SetDialogStrings_Ex(CDialog *pDlg,UINT uDlgID)
{
	char szSection[20] = "LockSDKDemo";
	CString szKey,szText;
	char cBuf[100]={0};
	char cTemp[100]={0};
	bool bSetText = 1;	//1:���ļ��������öԻ���
	//0:�ӶԻ���������浽�ļ�
	if (g_szLanguagePath == "") 
		GetLanguagePath_Ex();
 
	PF_SetIniPath_Ex(szLanguage);
	if(bSetText)	//1:���ļ��������öԻ���
	{
		CString szDefault = "";
		DWORD dwSize = 1000;
		char* pData = (char*)malloc(dwSize);
		
		//���Ի������
		szKey.Format("IDD%d_Title",uDlgID); 
		strcpy(cBuf,szKey);
		PF_ReadIniString_Ex(szSection,cBuf,cTemp,sizeof(cTemp));
		if(cTemp=="")
		{
			PF_ReadIniString_Ex(szSection,"IDS_STRING_NOTFOUND",cTemp,sizeof(cTemp));
		}
		pDlg->SetWindowText(cTemp);

		//д������ӿؼ��ı�������
		CWnd* pWnd = pDlg->GetWindow(GW_CHILD);
		while(pWnd != NULL)
		{
			szKey.Format("IDD%d_%d",uDlgID,pWnd->GetDlgCtrlID()); 
		 
			 
			memset(cBuf,0,sizeof(cBuf));
			strcpy(cBuf,szKey);
			PF_ReadIniString_Ex(szSection,cBuf,cTemp,sizeof(cTemp));
			if(cTemp=="")
			{
				PF_ReadIniString_Ex(szSection,"IDS_STRING_NOTFOUND",cTemp,sizeof(cTemp));
			}
			pWnd->SetWindowText(cTemp);
			pWnd = pWnd->GetWindow(GW_HWNDNEXT);
		}
		
		//�ͷ��ڴ�
		free(pData);
	}
	else	//0:�ӶԻ���������浽�ļ�
	{
		//д��Ի������
		szKey.Format("IDD%d_Title",uDlgID);
		pDlg->GetWindowText(szText);
		WritePrivateProfileString(szSection,szKey,szText,g_szLanguagePath);
		
		//д������ӿؼ��ı�������
		CWnd* pWnd = pDlg->GetWindow(GW_CHILD);
		while(pWnd != NULL)
		{
			szKey.Format("IDD%d_%d",uDlgID,pWnd->GetDlgCtrlID());
			pWnd->GetWindowText(szText);
			WritePrivateProfileString(szSection,szKey,szText,g_szLanguagePath);
			
			pWnd = pWnd->GetWindow(GW_HWNDNEXT);
		}
	}
}
  
