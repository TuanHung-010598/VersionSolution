// rwini.cpp: implementation of the Crwini class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include <stdlib.h>
#include "rwini.h"


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

Crwini::Crwini()
{

}

Crwini::~Crwini()
{

}


// ����Ini�ļ�·��
void Crwini::SetPath(char *path)
{
    strcpy(m_strPath, path);
}

// ɾ����
void Crwini::DeleteKey(char* appname, char* keyname)

{

         WritePrivateProfileString(appname, keyname, NULL, m_strPath);
}

// д���ַ���
bool Crwini::WriteString(char* appname, char* keyname,char* s)
{
	WritePrivateProfileString(appname,keyname,s,m_strPath);
	return 1;
}


// ��ȡ����������
int Crwini::GetInt(char* appname, char* keyname)
{
   return GetPrivateProfileInt(appname,keyname,1,m_strPath);
}

// ��ȡ�ַ���
void Crwini::ReadString(char *appname, char *keyname, char str[], long bufSize)
{
	::GetPrivateProfileString(appname,keyname,NULL,str,bufSize,m_strPath);
}


// д�����������
void Crwini::WriteInt(char *appname, char *keyname, int i)
{
	char r[10];
	itoa(i,r,10);
    WritePrivateProfileString(appname,keyname,r,m_strPath);
}


// д�볤��������
void Crwini::WriteLong(char *appname, char *keyname, long int i)
{
	char r[10];
	ltoa(i,r,10);
    WritePrivateProfileString(appname,keyname,r,m_strPath);
}



// ��ȡ����������
long Crwini::GetLong(char* appname, char* keyname)
{
    char buf[100];
    GetPrivateProfileString(appname, keyname, NULL, buf, 100, m_strPath );
    return atol(buf);
}


// ö��ǰ׺ΪsecPrefix�Ķ���
// ���ض���
short Crwini::GetSections(char secNames[][100], char *secPrefix)
{ 
/* 
������������ 
GetPrivateProfileSectionNames - �� ini �ļ��л�� Section ������
��� ini �������� Section: [sec1] �� [sec2]���򷵻ص��� 'sec1',0,'sec2',0,0 �����㲻֪��  
ini ������Щ section ��ʱ���������� api ����ȡ���� 
*/ 
    int i;  
    int iPos=0;  
    int iMaxCount; 
    int iCnt;
    const int MAX_ALLSECTIONS = 1000;
    const int MAX_SECTION = 100;
    TCHAR chSectionNames[MAX_ALLSECTIONS]={0}; //�ܵ���������ַ��� 
    TCHAR chSection[MAX_SECTION]={0}; //���һ�������� 
    GetPrivateProfileSectionNames(chSectionNames,MAX_ALLSECTIONS,m_strPath); 

    //����ѭ�����ضϵ�����������0 
    for(i=0;i<MAX_ALLSECTIONS;i++) 
    { 
        if (chSectionNames[i]==0) 
           if (chSectionNames[i]==chSectionNames[i+1]) 
           break; 
    } 

    iMaxCount=i+1; //Ҫ��һ��0��Ԫ�ء����ҳ�ȫ���ַ����Ľ������֡� 
    //rSection.RemoveAll();//���ԭ���� 

    iCnt = 0;
    for(i=0;i<iMaxCount;i++) 
    { 
        chSection[iPos++]=chSectionNames[i]; 
        if(chSectionNames[i]==0) 
        {  
           if (strstr(chSection, secPrefix) == chSection)
           {
                strcpy(secNames[iCnt], chSection);
                iCnt++;
           }           
           memset(chSection,0,MAX_SECTION); 
           iPos=0; 
        } 
    } 

    return iCnt;
} 
 




/*
//////////////////////////////////////////////////////////////////////////

// File: IniFile.h

// Date: October 2004

// Author: lixiaosan

// Email: airforcetwo@163.com

// Copyright (c) 2004. All Rights Reserved.

//////////////////////////////////////////////////////////////////////////

#if !defined(AFX_INIFILE_H__B5C0D7F7_8353_4C93_AAA4_38A688CA253C__INCLUDED_)

#define AFX_INIFILE_H__B5C0D7F7_8353_4C93_AAA4_38A688CA253C__INCLUDED_

#if _MSC_VER > 1000

#pragma once

#endif // _MSC_VER > 1000

class CIniFile  

{

public:

         CIniFile();

         virtual ~CIniFile();

         //    ����ini�ļ�·��
    //    �ɹ�����TRUE;���򷵻�FALSE

         BOOL         SetPath(CString strPath);

         

         //    ���section�Ƿ����
    //    ���ڷ���TRUE;���򷵻�FALSE

         BOOL         SectionExist(CString strSection);

         //    ��ָ����Section��Key��ȡKeyValue
        //    ����KeyValue

         CString         GetKeyValue(CString    strSection,

                                                        CString    strKey);         

         

         //    ����Section��Key�Լ�KeyValue����Section����Key�������򴴽�

         void          SetKeyValue(CString    strSection, 

                                                   CString    strKey, 

                                                   CString    strKeyValue);

         //    ɾ��ָ��Section�µ�һ��Key

         void          DeleteKey(CString strSection,

                                               CString strKey);

         //    ɾ��ָ����Section�Լ����µ�����Key

         void          DeleteSection(CString strSection);

         //    ������е�Section
        //    ����Section��Ŀ

         int              GetAllSections(CStringArray& strArrSection);

         

         //    ����ָ��Section�õ����µ�����Key��KeyValue
        //    ����Key����Ŀ

         int              GetAllKeysAndValues(CString strSection,

                                                                     CStringArray& strArrKey,

                                                                     CStringArray& strArrKeyValue);

         //       ɾ������Section

         void          DeleteAllSections();

         

private:

         //       ini�ļ�·��

         CString m_strPath;

         

};





*/




/*


//////////////////////////////////////////////////////////////////////////

// File: IniFile.cpp

// Date: October 2004

// Author: lixiaosan

// Email: airforcetwo@163.com

// Copyright (c) 2004. All Rights Reserved.

//////////////////////////////////////////////////////////////////////////

��i nclude "stdafx.h"

//��i nclude "test6.h"

��i nclude "IniFile.h"



#ifdef _DEBUG

#undef THIS_FILE

static char THIS_FILE[]=__FILE__;

#define new DEBUG_NEW

#endif

#define         MAX_SECTION                260        //Section��󳤶�

#define         MAX_KEY                         260        //KeyValues��󳤶�

#define         MAX_ALLSECTIONS     65535    //����Section����󳤶�

#define         MAX_ALLKEYS              65535    //����KeyValue����󳤶�

//////////////////////////////////////////////////////////////////////

// Construction/Destruction

//////////////////////////////////////////////////////////////////////

CIniFile::CIniFile()

{

}

CIniFile::~CIniFile()

{

}

//////////////////////////////////////////////////////////////////////////

//   Public Functions

//////////////////////////////////////////////////////////////////////////

BOOL CIniFile::SetPath(CString strPath)

{

         m_strPath = strPath;

         

         //       ����ļ��Ƿ����

         DWORD  dwFlag = GetFileAttributes((LPCTSTR)m_strPath);

         

         //       �ļ�����·�������ڣ�����FALSE

         if( 0xFFFFFFFF == dwFlag )

                   return FALSE;

         

         //       ·����Ŀ¼������FALSE

         if (  FILE_ATTRIBUTE_DIRECTORY & dwFlag )

                   return FALSE;

         return TRUE;

}



CString CIniFile::GetKeyValue(CString strSection,

                                                         CString strKey)

{

         TCHAR         chKey[MAX_KEY];

         DWORD         dwRetValue;

         CString strKeyValue=_T("");

         dwRetValue = GetPrivateProfileString(

                  (LPCTSTR)strSection,

                  (LPCTSTR)strKey,

                   _T(""),

                   chKey,

                  sizeof(chKey)/sizeof(TCHAR),

                  (LPCTSTR)m_strPath);       

         

         strKeyValue = chKey;

         

         return strKeyValue;

}

void CIniFile::SetKeyValue(CString strSection,

                                                 CString strKey,

                                                 CString strKeyValue)

{

         WritePrivateProfileString(

                  (LPCTSTR)strSection, 

                  (LPCTSTR)strKey, 

                  (LPCTSTR)strKeyValue, 

                  (LPCTSTR)m_strPath);

}

void CIniFile::DeleteKey(CString strSection, CString strKey)

{

         WritePrivateProfileString(

                  (LPCTSTR)strSection, 

                  (LPCTSTR)strKey, 

                   NULL,          //       ����дNULL,��ɾ��Key

                  (LPCTSTR)m_strPath);

}

void CIniFile::DeleteSection(CString strSection)

{

         WritePrivateProfileString(

                  (LPCTSTR)strSection, 

                   NULL,          

                   NULL,          //       ���ﶼдNULL,��ɾ��Section

                  (LPCTSTR)m_strPath);

}

int CIniFile::GetAllSections(CStringArray& strArrSection)

{

         int dwRetValue, i, j, iPos=0;

         TCHAR chAllSections[MAX_ALLSECTIONS];

         TCHAR chTempSection[MAX_SECTION];

         ZeroMemory(chAllSections, MAX_ALLSECTIONS);

         ZeroMemory(chTempSection, MAX_SECTION);

         dwRetValue = GetPrivateProfileSectionNames(

                  chAllSections,

                  MAX_ALLSECTIONS,

                  m_strPath);

         //       ��ΪSection�������еĴ����ʽΪ��Section1����0����Section2����0��0��
    //       ���������⵽��������0����break

         for(i=0; i<MAX_ALLSECTIONS; i++) 

         { 

                  if( chAllSections[i] == NULL ) 

                   {

                            if( chAllSections[i] == chAllSections[i+1] )

                                     break; 

                   }

         } 

         

         i++; //         ��֤���ݶ���

         strArrSection.RemoveAll(); //         �������

         

         for(j=0; j<i; j++) 

         { 

                  chTempSection[iPos++] = chAllSections[j]; 

                  if( chAllSections[j] == NULL ) 

                   {  

                            strArrSection.Add(chTempSection); 

                            ZeroMemory(chTempSection, MAX_SECTION);

                            iPos = 0; 

                   } 

         }

         

         return strArrSection.GetSize();

}

int CIniFile::GetAllKeysAndValues(CString  strSection, 

                                                            CStringArray&         strArrKey, 

                                                            CStringArray& strArrKeyValue)

{

         int dwRetValue, i, j, iPos=0;

         TCHAR chAllKeysAndValues[MAX_ALLKEYS];

         TCHAR chTempkeyAndValue[MAX_KEY];

         CString strTempKey;

         ZeroMemory(chAllKeysAndValues, MAX_ALLKEYS);

         ZeroMemory(chTempkeyAndValue, MAX_KEY);

         dwRetValue = GetPrivateProfileSection(

                  strSection,

                  chAllKeysAndValues,

                  MAX_ALLKEYS,

                  m_strPath);

         //       ��ΪSection�������еĴ����ʽΪ��Key1=KeyValue1����0����Key2=KeyValue2����0
         //       ���������⵽��������0����break

         for(i=0; i<MAX_ALLSECTIONS; i++) 

         { 

                  if( chAllKeysAndValues[i] == NULL ) 

                   {

                            if( chAllKeysAndValues[i] == chAllKeysAndValues[i+1] )

                                     break; 

                   }

         } 

         

         i++;

         strArrKey.RemoveAll();

         strArrKeyValue.RemoveAll();

         

         for(j=0; j<i; j++) 

         { 

                  chTempkeyAndValue[iPos++] = chAllKeysAndValues[j]; 

                  if( chAllKeysAndValues[j] == NULL ) 

                   {  

                            strTempKey = chTempkeyAndValue; 

                            strArrKey.Add( strTempKey.Left(strTempKey.Find('=')) );

                            strArrKeyValue.Add( strTempKey.Mid(strTempKey.Find('=')+1) );

                            ZeroMemory(chTempkeyAndValue, MAX_KEY);

                            iPos = 0; 

                   } 

         }

         

         return strArrKey.GetSize();

}

void CIniFile::DeleteAllSections()

{

         int nSecNum; 

         CStringArray strArrSection; 

         nSecNum = GetAllSections(strArrSection); 

         for(int i=0; i<nSecNum; i++) 

         { 

                  WritePrivateProfileString(

                            (LPCTSTR)strArrSection[i],

                            NULL,

                            NULL,

                            (LPCTSTR)m_strPath);       

         }

}


*/
