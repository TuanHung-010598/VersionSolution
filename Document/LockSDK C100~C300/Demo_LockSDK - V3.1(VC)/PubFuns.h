#ifdef __cplusplus
   extern "C" {
#endif

#include <time.h>

void __stdcall PF_MyTrim(char *dat);

/*=============================================================================
函数名：void __stdcall PF_AsciiConvert(int mode, char *dstData, char *srcData)	

功能描述：  ASCII码转换

输入参数： mode     -- 模式: 1--加密; 2--解密
           srcData  -- 源数据字符串
输出参数： dstData  -- 转换后的字符串 (dstData缓冲区必须>20)
返回值：   
=============================================================================*/
void __stdcall PF_AsciiConvert(int mode, char *dstData, char *srcData);

/*=============================================================================
函数名：void __stdcall PF_SimpleXOR(BYTE *dat, UINT len, BYTE seed)	

功能描述：  简单异或加密

输入参数：  dat -- 数据指针
            len -- 要加密的长度
            seed -- 加密种子
输出参数： 
返回值：   
=============================================================================*/
void __stdcall PF_SimpleXOR(BYTE *dat, UINT len, BYTE seed);

/*=============================================================================
函数名：void __stdcall PF_SimpleEncrypt(BYTE *dat, UINT len, BYTE seed)	

功能描述：  简单加密

输入参数：  dat -- 数据指针
            len -- 要加密的长度
            seed -- 加密种子
输出参数： 
返回值：   
=============================================================================*/
void __stdcall PF_SimpleEncrypt(BYTE *dat, UINT len, BYTE seed);


/*=============================================================================
函数名：void __stdcall PF_SimpleDecrypt(BYTE *dat, UINT len, BYTE seed)

功能描述：  简单解密

输入参数：  dat -- 数据指针
            len -- 要解密的长度
            seed -- 加密种子
输出参数： 
返回值：   
=============================================================================*/
void __stdcall PF_SimpleDecrypt(BYTE *dat, UINT len, BYTE seed);

/*=============================================================================
函数名：	void PF_Time_tm2SYSTEMTIME(SYSTEMTIME *st, const tm _tm)
	
功能描述：   tm转换为SYSTEMTIME

输入参数： _tm -- tm结构体

输出参数： st -- SYSTEMTIME 结构体

  
返回值：   
=============================================================================*/
void __stdcall PF_Time_tm2SYSTEMTIME(SYSTEMTIME *st, const tm _tm);

/*=============================================================================
函数名：	void PF_Time_SYSTEMTIME2tm(tm *_tm, const SYSTEMTIME st)
	
功能描述： SYSTEMTIME 转换为tm 

输入参数： st -- SYSTEMTIME 结构体

输出参数： _tm -- tm结构体
  
返回值：   
=============================================================================*/
void __stdcall PF_Time_SYSTEMTIME2tm(tm *_tm, const SYSTEMTIME st);

/*=============================================================================
函数名：	void PF_Time_tm2String(char *str, const tm _st)
	
功能描述： tm 转换为字符串

输入参数： _st -- tm 结构体

输出参数： str -- 日期时间字符串，格式：YYYY-MM-DD hh:mm:ss
  
返回值：   
=============================================================================*/
void __stdcall PF_Time_tm2String(char *str, const tm _st);

/*=============================================================================
函数名： void PF_Time_String2tm(tm *_st, const char *str)
	
功能描述： tm 转换为字符串

输入参数： 日期时间字符串，格式：YYYY-MM-DD hh:mm:ss

输出参数： _st -- tm 结构体
  
返回值：   
=============================================================================*/
void __stdcall PF_Time_String2tm(tm *_st, const char *str);

/*=============================================================================
函数名：		void PF_Time_AddMinutes(tm *tm1, long ndays)	

功能描述：  一个日期加上一定的分钟, 得到新的日期

输入参数：  tm1 -- 原日期
            nMinutes -- 分钟

输出参数： tm1 -- 新的日期

返回值：   
=============================================================================*/
void __stdcall PF_Time_AddMinutes(tm *tm1, long nMinutes);

/*=============================================================================
函数名：		void PF_Time_SubbMinutestm *tm1, long ndays)	

功能描述：  一个日期减去一定的分钟, 得到新的日期

输入参数：  tm1 -- 原日期
            nMinutes -- 分钟

输出参数： tm1 -- 新的日期

返回值：   
=============================================================================*/
void __stdcall PF_Time_SubbMinutes(tm *tm1, long nMinutes);

/*=============================================================================
函数名：			PF_Hex2LongBE	

  
功能描述：  把1~4字节Hex转换为long

输入参数：  byteSrc -- 1~4个字节, 采用大端模式(高字节在前)

输出参数：  longDsr -- 一个长整型数
返回值：   无
=============================================================================*/
void __stdcall PF_Hex2LongBE(OUT UINT *longDsr, BYTE *byteSrc, UINT byteCnt);

/*=============================================================================
函数名：			PF_Long2HexBE	


功能描述：  把long转换为1~4字节

输入参数：  longSrc -- 一个长整型数

输出参数：  byteDst -- 1~4个字节, 采用大端模式(高字节在前)
返回值：   无
=============================================================================*/
void __stdcall PF_Long2HexBE(OUT BYTE *byteDst, UINT longSrc, UINT byteCnt);

           
/*=============================================================================
函数名：			PF_SetIniPath_Ex	

功能描述：  设置扩展ini文件名, 带扩展的ini函数统一使用此文件名, 而不带扩展的则使用lockinfo.dll

输入参数： iniFile -- ini文件名, 为应用程序路径下的
输出参数： 无
返回值：   节名数
=============================================================================*/
short __stdcall PF_SetIniPath_Ex(char *iniFile);
       
/*=============================================================================
函数名：			PF_WriteIniString_Ex	

功能描述：  从PF_SetIniPath_Ex指定的文件中写入字符串

输入参数：  appName -- 节名
            keyName -- 字段
            srcString -- 数据

输出参数： 无
返回值：   无
=============================================================================*/
void PF_WriteIniString_Ex(char *appName, char *keyName, char *srcString);


/*=============================================================================
函数名：			PF_ReadIniString_Ex	


功能描述：  从PF_SetIniPath_Ex指定的文件中读取字符串

输入参数：  appName -- 节名
            keyName -- 字段
            nBytes  -- 缓冲区长度
输出参数：  dstString -- 输出字符串
返回值：   无
=============================================================================*/
void __stdcall PF_ReadIniString_Ex(char *appName, char *keyName, char *dstString, unsigned int nBufLength);

/*=============================================================================
函数名：			PF_AccurateDelayMs	

功能描述：  精确延时N mS

输入参数： dlyMs -- 延时的毫秒数
输出参数： 
返回值：   
=============================================================================*/
short __stdcall PF_AccurateDelayMs(double dlyMs);


/*=============================================================================
函数名：			PF_GetSections	

功能描述：  枚举LockInfo.dll中所有前缀为secPrefix的节名, 如果secPrefix为空字符串,
            则枚举出所有的节名.

输入参数： secPrefix -- 节名的前缀
输出参数： secNames -- 一个节名对应一个字符串
返回值：   节名数
=============================================================================*/
short __stdcall PF_GetSections(char secNames[][100], char *secPrefix);

/*=============================================================================
函数名：			PF_ReadIniEnc	

功能描述：  从ini文件中读取加密保存的数据

输入参数：  appName -- 节名
            keyName -- 字段
            dat     -- 数据，全部以二进制看待
            nBytes  -- 缓冲区长度
输出参数： 无
返回值：   无
=============================================================================*/
void PF_ReadIniEnc(char *appName, char *keyName, BYTE *buf, long nBufLength);

/*=============================================================================
函数名：			PF_WriteIniEnc	

功能描述：  把数据以加密方式保存到ini文件

输入参数：  appName -- 节名
            keyName -- 字段
            dat     -- 数据，全部以二进制看待
            nBytes  --  保存的字节数
输出参数： 无
返回值：   无
=============================================================================*/
void PF_WriteIniEnc(char *appName, char *keyName, BYTE *dat, long nBytes);

/*=============================================================================
函数名：			void CopyToClipboard(char * str)	

功能描述：  把字符串保存到剪贴板上

输入参数：  pszSrc -- 以NULL结尾的字符串

输出参数： 无

返回值：   无
=============================================================================*/
void CopyToClipboard(char * pszSrc);

/*=============================================================================
函数名：				void Short2Time(short* nTime, char* dst)	

功能描述：  计算两个SYSTEMTIME型日期时间相差的天数

输入参数：  st1 -- 日期时间1
            st1 -- 日期时间2

输出参数： 无

返回值：   两个日期时间相差的天数, 如果st1 < st2, 则返回值是负数
=============================================================================*/
long DaysDiff(SYSTEMTIME *st1, SYSTEMTIME *st2);

/*=============================================================================
函数名：				void Short2Time(short* nTime, char* dst)	

功能描述：  SYSTEMTIME型日期时间比较

输入参数：  st1 -- 日期时间1
            st1 -- 日期时间2
            cmpLen  -- 比较的长度, 为1比较年, 为2比较年月, 为6比较年月日时分秒

输出参数： 无

返回值：    -1: st1 < st2; 0: st1==st2; 1: st1 > st2
=============================================================================*/
short SystemTimeCmp(SYSTEMTIME *st1, SYSTEMTIME *st2, short cmpLen);


/*=============================================================================
函数名：				void Short2Time(short* nTime, char* dst)	

功能描述：  时间转换为字符串

输入参数： nDTime -- short型时间，nTime[0]~[2]依次为时分秒

输出参数： dst -- 时间字符串，格式：hh:mm:ss

返回值：    
=============================================================================*/
void Short2Time(short* nTime, char* dst);

/*=============================================================================
函数名：				void Short2DateTime(short* nDateTime, char* dst)	

功能描述：  日期时间转换为short型

输入参数： nDateTime -- short型日期时间，nDateTime[0]~[5]依次为年月日时分秒

输出参数： dst -- 日期时间字符串，格式：YYYY-MM-DD hh:mm:ss

返回值：    
=============================================================================*/
void Short2DateTime(short* nDateTime, char* dst);

/*=============================================================================
函数名：				void Time2Short(char* ptime, short* dst)	

功能描述：  时间转换为short型

输入参数： ptime -- 时间字符串，格式：hh:mm:ss

输出参数： dst -- short型时间，dst[0]~[2]依次为时分秒

返回值：    
=============================================================================*/
void Time2Short(char* ptime, short* dst);

/*=============================================================================
函数名：				void Datetime2Short(char* datetime, short* dst)	

功能描述：  日期时间转换为short型

输入参数： datetime -- 日期时间字符串，格式：YYYY-MM-DD hh:mm:ss

输出参数： dst -- short型日期时间，dst[0]~[5]依次为年月日时分秒

返回值：    
=============================================================================*/
void Datetime2Short(char* datetime, short* dst)	;

/*=============================================================================
函数名：				BYTE GenChekSum(BYTE *dat, short len)	

功能描述：  产生累加和

输入参数： dat -- 要累加的数据
           len -- 要累加的字节数

输出参数： dat中len字节数据的累加和

返回值：    
=============================================================================*/
BYTE GenChekSum(BYTE *dat, short len);


/*=============================================================================
函数名：				BYTE GenChekSumInvert(BYTE *dat, short len)	

功能描述：  产生累加和取反

输入参数： dat -- 要累加的数据
           len -- 要累加的字节数

输出参数： dat中len字节数据的累加和的取反

返回值：      
=============================================================================*/
BYTE GenChekSumInvert(BYTE *dat, short len)	;


/*=============================================================================
函数名：				int FindChar(char *str, char c, int strLen)	

功能描述： 在数组中查找一个字符的位置, 从最左边开始查找.这个数组不一定是以'\0'结尾的

输入参数： str  -- 数组
           c    --  要查找的字符
           strLen - 数组长度

输出参数：  无

返回值：   字符在数组中的位置, 如果 < 0则表示没有查找到
=============================================================================*/
int FindChar(char *str, char c, int strLen);	


/*=============================================================================
函数名：	void Hex2Char(BYTE *hexStr, char *dstBuf, short hexLen)

功能描述： 把16进制数转成可见字符串，方便显示

输入参数： hexStr -- 16进制数组
           hexLen -- 16进制数的字节数

输出参数： dstBuf -- 输出字符串， 长度为16进制数组的长度的2倍 + 1

返回值：   无
=============================================================================*/
void Hex2Char(BYTE *hexStr, char *dstBuf, long hexLen);


/*=============================================================================
函数名：	void Str2Hex(char *str, BYTE *hex, short strLen)

功能描述： 把字符串转成16进制

输入参数： str -- 字符串
           strLen -- 字符串的字节数

输出参数： hex -- 16进制数组，长度为strLen/2

返回值：   无
=============================================================================*/
void Str2Hex(char *str, BYTE *hex, short strLen);

/*=============================================================================
函数名：	void hex2BCD(BYTE *srcHex, BYTE *dstBCD, short hexLen)

功能描述： 把16进制数转成BCD码, 例如18(0x12) 转成0x18

输入参数： srcHex -- 待转的16进制数
           hexLen -- 字符串的字节数

输出参数： dstBCD -- BCD码

返回值：   无
=============================================================================*/
void hex2BCD(BYTE *srcHex, BYTE *dstBCD, short hexLen);

/*=============================================================================
函数名：	void BCD2hex(BYTE *srcBCD, BYTE *dstHex, short bcdLen)

功能描述： 把16进制数转成BCD码, 例如18(0x12) 转成0x18

输入参数： srcBCD -- 待转的16进制数BCD码
           bcdLen -- 字符串的字节数

输出参数： dstHex -- 16进制数

返回值：   无
=============================================================================*/
void BCD2hex(BYTE *srcBCD, BYTE *dstHex, short bcdLen);

/*=============================================================================
函数名：	void Hex2StrEx(BYTE *hexStr, char *dstBuf)

功能描述： 把8字节十六进制数据转化成123456-123456-123456-123456这样的字符串

输入参数： hexStr -- 16进制数组

输出参数： dstBuf -- 输出字符串，27个字符

返回值：   无
=============================================================================*/
void Hex2StrEx(BYTE *hexStr, char *dstBuf);

/*=============================================================================
函数名：	void StrEx2Hex(char *srcStr, BYTE *dstHex)

功能描述： 把123456-123456-123456-123456这样的字符串转化成8字节十六进制数据

输入参数： srcStr -- 字符串，27个字符

输出参数： dstBuf -- 16进制数组, 8字节

返回值：   无
=============================================================================*/
void StrEx2Hex(char *srcStr, BYTE *dstHex);


/*=============================================================================
函数名：	void nMemSet(short *buf, short ndat, short len)

功能描述： short 型数组赋值

输入参数： buf  -- 缓冲区 
           ndat -- 要赋的值
           len  -- short数组大小

输出参数： 无

返回值：   无
=============================================================================*/
void nMemSet(short *buf, short ndat, short len);

/*=============================================================================
函数名：	void nMemCpy(short *dst, short *src, short len)

功能描述： short 型数组拷贝

输入参数： dst  -- 目的缓冲区 
           src  -- 源缓冲区
           len  -- short数组大小

输出参数： 无

返回值：   无
=============================================================================*/
void nMemCpy(short *dst, short *src, short len);

/*=============================================================================
函数名：	short nMemCmp(short *str1, short *str2, short len)

功能描述： short 型数组比较

输入参数： str1 -- short 数组1
           str2 -- short 数组2
           len  -- short数组大小

输出参数： 无

返回值：   0    --  str1 == str2
           1    --  str1 > str2
           -1   --  str1 < str2
=============================================================================*/
short nMemCmp(short *str1, short *str2, short len);
#ifdef __cplusplus
   }
#endif