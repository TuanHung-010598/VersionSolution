// Demo_LockSDKDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Demo_LockSDK.h"
#include "Demo_LockSDKDlg.h"
#include "LockSDK.h"
#include "Language.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDemo_LockSDKDlg dialog

CDemo_LockSDKDlg::CDemo_LockSDKDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDemo_LockSDKDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDemo_LockSDKDlg)

	m_iLockType = 1;
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDemo_LockSDKDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDemo_LockSDKDlg)
	DDX_Text(pDX, IDC_ED_CHECKIN_TIME, m_strCheckinTime);
	DDX_Text(pDX, IDC_ED_CHECKOUT_TIME, m_strCheckoutTime);
	DDX_Text(pDX, IDC_ED_ROOM_NO, m_strRoomNo);
	DDX_Radio(pDX, IDC_RD_LOCK_TYPE, m_iLockType);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDemo_LockSDKDlg, CDialog)
	//{{AFX_MSG_MAP(CDemo_LockSDKDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BN_CONFIG, OnBnConfig)
	ON_BN_CLICKED(IDC_BN_CHECKIN, OnBnCheckin)
	ON_BN_CLICKED(IDC_BN_CHECKOUT, OnBnCheckout)
	ON_BN_CLICKED(IDC_BN_READ_CARD, OnBnReadCard) 
	ON_BN_CLICKED(IDC_BN_CHECKINTIME, OnBnCheckintime)
	ON_WM_CLOSE() 
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDemo_LockSDKDlg message handlers
void _getLocalTimeStr(char *datetime )
{
    int buf6[6]; 
    char cTime[20]; 
    
    time_t ltime;
    struct tm *today;     //TM结构
    
    
    
    time(&ltime);
    today = localtime( &ltime );      //取得当前时间  
    buf6[0]=today->tm_year+1900;
    buf6[1]=today->tm_mon+1;
    buf6[2]=today->tm_mday;
    buf6[3]=today->tm_hour;
    buf6[4]=today->tm_min;
    buf6[5]=today->tm_sec;


    
    sprintf(cTime, "%04d-", buf6[0]);
    sprintf(&cTime[5], "%02d-", buf6[1]);
    sprintf(&cTime[8], "%02d ", buf6[2]);
    sprintf(&cTime[11], "%02d:", buf6[3]);
    sprintf(&cTime[14], "%02d:", buf6[4]);
    sprintf(&cTime[17], "%02d", buf6[5]);
    
    strcpy(datetime, cTime);  
    
}
BOOL CDemo_LockSDKDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	g_SetDialogStrings_Ex(this,IDD);
 
	char cbuf[20];
    
     CTime m_Date = CTime::GetCurrentTime() + CTimeSpan( 1, 0, 0, 0 );
	m_strCheckoutTime.Format("%04d-%02d-%02d",m_Date.GetYear(),m_Date.GetMonth(),m_Date.GetDay());
	m_strCheckoutTime +=" 12:00:00";
    _getLocalTimeStr(cbuf);
    m_strCheckinTime = cbuf; 	
	m_strRoomNo = _T("001.002.0028");
	UpdateData(FALSE);
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CDemo_LockSDKDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CDemo_LockSDKDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}





void CDemo_LockSDKDlg::CheckErr(int st)    //检测函数
{
    char msg[100];
	char Title[100];
    switch( st ) 
    {
    case 1:

        strcpy(msg,g_LoadString_Ex("IDS_STRING_SUCCESS"));
        break;
    case -1:
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_NOCARD"));
        break;
    case -2 :
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_NOREADE"));
        break;
    case -3 :
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_INVALIDCARD"));
        break;
    case -4:
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_CARDTYPE"));
        break;
    case -5 :
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_READCARD"));
        break;
    case -8 :
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_INPUT"));
        break;
    case -29 :
        strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_REG"));
        break;

    default :
        strcpy(msg, g_LoadString_Ex("IDS_STRING_ERROR"));
		strcpy(msg," \r\n");
		strcpy(msg,g_LoadString_Ex("IDS_STRING_ERROR_CODE"));
        sprintf(&msg[strlen(msg)], "%d", st); 
    }
	strcpy(Title,g_LoadString_Ex("IDS_STRING_MSG"));
	::MessageBox(m_hWnd,msg, Title ,MB_ICONASTERISK);
}



void CDemo_LockSDKDlg::OnBnConfig() 
{
    UpdateData();

    int lockType;
    int iRet;
         
// 选择门锁类型
    if (m_iLockType == 0)
    {
        lockType = 4;
    }
    else 
    {
        lockType = 5;
    }

// 配置动态库, 并连接发卡器
    iRet = TP_Configuration(lockType);

    if (iRet == OPR_OK)
    {
        GetDlgItem(IDC_BN_READ_CARD)->EnableWindow(TRUE);
        GetDlgItem(IDC_BN_CHECKIN)->EnableWindow(TRUE);
        GetDlgItem(IDC_BN_CHECKOUT)->EnableWindow(TRUE);
    }
    else {
        GetDlgItem(IDC_BN_READ_CARD)->EnableWindow(FALSE);
        GetDlgItem(IDC_BN_CHECKIN)->EnableWindow(FALSE);
        GetDlgItem(IDC_BN_CHECKOUT)->EnableWindow(FALSE);
    }

    CheckErr(iRet);
    UpdateData(FALSE);
	
}


// 入住
void CDemo_LockSDKDlg::OnBnCheckin() 
{
    UpdateData();

    char roomNo[100];
    char checkinTime[100];
    char checkoutTime[100];
    char cardSnr[100];
    int iret;


///////////
    strcpy(roomNo, (char*)(LPCTSTR)m_strRoomNo);
    strcpy(checkinTime, (char*)(LPCTSTR)m_strCheckinTime);
    strcpy(checkoutTime, (char*)(LPCTSTR)m_strCheckoutTime);

    iret = TP_MakeGuestCard(cardSnr, roomNo, checkinTime, checkoutTime, 0);
    CheckErr(iret);

    UpdateData(FALSE);	
}


// 销卡(注销卡片, 回收卡片, 退房等)
void CDemo_LockSDKDlg::OnBnCheckout() 
{
    UpdateData();

    int iret;
    char cardSnr[100];

    iret = TP_CancelCard(cardSnr);
    CheckErr(iret);

    UpdateData(FALSE);
	
}

// 读卡
void CDemo_LockSDKDlg::OnBnReadCard() 
{
    UpdateData();

    char roomNo[100];
    char checkinTime[100];
    char checkoutTime[100];
    char cardSnr[100];
    int iret;
    char msg[400]; 
	char Title[100];
    iret = TP_ReadGuestCard(cardSnr, roomNo, checkinTime, checkoutTime);
    if (iret == OPR_OK)
    { 
		strcpy(msg,g_LoadString_Ex("IDS_STRING_CARDNO")); 
        strcat(msg, cardSnr);
  		strcat(msg,"\r\n");
        strcat(msg,g_LoadString_Ex("IDS_STRING_LOCKNO"));
        strcat(msg, roomNo);
  		strcat(msg,"\r\n");
        strcat(msg,g_LoadString_Ex("IDS_STRING_INTIME")); 
        strcat(msg, checkinTime);
		strcat(msg,"\r\n");
        strcat(msg, g_LoadString_Ex("IDS_STRING_OUTTIME"));
        strcat(msg, checkoutTime);
	
        strcpy(Title,g_LoadString_Ex("IDS_STRING_MSG"));
		::MessageBox(m_hWnd,msg, Title ,MB_ICONASTERISK);
    }
    else {
        CheckErr(iret);
    }

    UpdateData(FALSE);
	
} 




void CDemo_LockSDKDlg::OnBnCheckintime() 
{

    UpdateData();
    // TODO: Add your control notification handler code here
    // TODO: Add extra initialization here
    char cbuf[20];
    
    
    _getLocalTimeStr(cbuf);
    m_strCheckinTime = cbuf;
    
	UpdateData(false);		
}
 
 
