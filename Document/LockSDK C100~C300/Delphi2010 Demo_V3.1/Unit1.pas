unit Unit1;

interface
 //����IniFiles
uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,shellapi,IniFiles;

type
  TIDD102 = class(TForm)
    IDD102_1003: TButton;
    IDD102_1005: TButton;
    IDD102_1011: TButton;
    IDD102_1012: TLabel;
    IDD102_1000: TRadioButton;
    IDD102_1001: TRadioButton;
    IDD102_1002: TButton;
    IDD102_1013: TLabel;
    IDD102_1014: TLabel;
    IDD102_1009: TButton;
    etRoom: TEdit;
    etInTime: TEdit;
    etOutTime: TEdit;
    procedure IDD102_1002Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure IDD102_1003Click(Sender: TObject);
    procedure IDD102_1005Click(Sender: TObject);
    procedure IDD102_1009Click(Sender: TObject);
    procedure IDD102_1011Click(Sender: TObject);
  private
    Door_Path: string;
    iRet: LongInt;
    Rom: array[0..64] of  AnsiChar;//����
    LockSDKDLL: LongWord;
    iDoorType: LongInt;
  public
    { Public declarations }
  end;

var
  IDD102: TIDD102;
  MyIniFile:TIniFile;
  g_szLanguagePath: string;//������Դ·�����ļ���
  g_szText:string; //���ݱ�ʶ szID��ѡ���������ļ��м����ַ���
  g_szfrmText:string;//���ڱ���
implementation

uses Unit2;
{$R *.dfm}

//�õ�ini�ļ�·��
procedure GetLanguagePath_Ex();
var
  fileNm: string;
  vs    : string;

Begin
 // fileNm :=ExtractFilePath(Paramstr(0))+'Config.ini';
 // myinifile:=Tinifile.Create(fileNm);
 // vs:=myinifile.Readstring('System','Language','Chinese' );
  g_szLanguagePath :=ExtractFilePath(Paramstr(0))+'ToolsLanguage.ini';
end;

///*********************************************************************
 //* ��������:g_LoadString_Ex
 //* ˵��:	���ݱ�ʶ szID��ѡ���������ļ��м����ַ���
// * ����:	zs
//*********************************************************************/
function g_LoadString_Ex(szID: string):string;
var
  vs    : string;
Begin
  if  g_szLanguagePath='' then
  begin
     GetLanguagePath_Ex();
  end;
  myinifile:=Tinifile.Create(g_szLanguagePath);
   vs:=myinifile.Readstring('String',szID,'Not found' );
   g_LoadString_Ex:=vs;
end;

///*********************************************************************
 //* ��������:g_SetDialogStrings_Ex(frm:TForm)
 //* ˵��:	���Ի�������ʱ��ȡ�����пɵõ����ַ����������浽�����ļ���
 //			ÿ���ؼ��öԻ���ID�Ϳؼ�IDΨһ��ʶ
 //* ��ڲ���:
 //* frm:TForm -- �Ի������
 //* ����: zs
//*********************************************************************/
procedure g_SetDialogStrings_Ex(const Cp : TForm);
var
  szKey : string;
  szText: string;
  I     : integer;
Begin
     if  g_szLanguagePath='' then
     begin
        GetLanguagePath_Ex();
     end;
     myinifile:=Tinifile.Create(g_szLanguagePath);

     //�����ڱ���
     szKey :=Cp.Name+'_Title';
     szText:=myinifile.Readstring('LockSDKDemo',szKey,'Not found' );
     Cp.Caption :=szText;


     //�����ؼ��ı���
     for I := Cp.ComponentCount-1 downto 0 do
     Begin
       szKey :=Cp.Components[I].Name;// Cp.Name+'_'+Cp.Components[I].Name;
       szText:=myinifile.Readstring('LockSDKDemo',szKey,'Not found' );
       if Cp.Components[I] is TButton  then
           TButton(Cp.Components[I]).Caption :=szText;
       if Cp.Components[I] is TLabel  then
           TLabel(Cp.Components[I]).Caption :=szText;
       if Cp.Components[I] is TRadioButton then
          TRadioButton(Cp.Components[I]).Caption :=szText;
     end;
end;


Procedure CheckErr(Ret: smallint);
var
  MsgStr: String;
   MsgTitle: String;
Begin
  case Ret of
    1:
       MsgStr := g_LoadString_Ex('IDS_STRING_SUCCESS');
    -1:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_NOCARD');
    -2:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_NOREADE');
    -3:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_INVALIDCARD');
    -4:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_CARDTYPE');
    -5:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_READCARD');
    -8:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_INPUT');
    -29:
      MsgStr := g_LoadString_Ex('IDS_STRING_ERROR_REG');
  else
    MsgStr := g_LoadString_Ex('IDS_STRING_ERROR');
  end;
  MsgTitle := g_LoadString_Ex('IDS_STRING_MSG');
  Application.MessageBox(Pchar(MsgStr),Pchar(MsgTitle), MB_OK + MB_ICONINFORMATION);
end;

procedure TIDD102.IDD102_1005Click(Sender: TObject);
var
  //����
  //�ڶ�̬���������char*���ڶ��庯��ʱ��PAnsiChar
  TP_CancelCard :function(sRom:PAnsiChar):LongInt;stdcall;
begin
  TP_CancelCard :=GetProcAddress(LockSDKDLL,'TP_CancelCard');
  iRet :=  TP_CancelCard(Rom);
  CheckErr(iRet);
end;

//��ס
procedure TIDD102.IDD102_1003Click(Sender: TObject);
var
  //�ڶ�̬����������char*���ڶ��庯��ʱ��PAnsiChar
  TP_MakeGuestCard:function(sRom:PAnsiChar;sRoomNo:PAnsiChar;sInTime:PAnsiChar;sOutTime:PAnsiChar;iFlags:LongInt):LongInt;stdcall;

  begin
  TP_MakeGuestCard:=GetProcAddress(LockSDKDLL,'TP_MakeGuestCard');
  //��������ʱֱ����PAnsichar(Ansistring())��string����ֵת��ΪPAnSIchar��
  iRet :=TP_MakeGuestCard(Rom,PAnsiChar(AnsiString(etRoom.Text)),PAnsiChar(AnsiString(etInTime.Text)),PAnsiChar(AnsiString(etOutTime.Text)),0);
  if iRet=1 then
  begin
    //lblMsg.Caption :='���ţ�'+Rom;
  end;
  CheckErr(iRet);
end;





procedure TIDD102.IDD102_1009Click(Sender: TObject);
var
  aRoomNo :array[0..64] of  AnsiChar;
  aInTime :array[0..64] of  AnsiChar;
  aOutTime :array[0..64] of  AnsiChar;
  strMsg : string;
  MsgTitle: string;
  TP_ReadGuestCard:function(sRom:PAnsiChar;sRoomNo:PAnsiChar;sInTime:PAnsiChar;sOutTime:PAnsiChar):LongInt;stdcall;
begin
  TP_ReadGuestCard :=GetProcAddress(LockSDKDLL,'TP_ReadGuestCard');
  iRet :=TP_ReadGuestCard(Rom,aRoomNo,aInTime,aOutTime);
  if iRet=1 then
  begin
    strMsg :=g_LoadString_Ex('IDS_STRING_CARDNO')+Rom+#13#10+g_LoadString_Ex('IDS_STRING_LOCKNO')+aRoomNo +#13#10;
    strMsg :=strMsg+g_LoadString_Ex('IDS_STRING_INTIME')+aInTime+#13#10 +g_LoadString_Ex('IDS_STRING_OUTTIME')+aOutTime;

    MsgTitle := g_LoadString_Ex('IDS_STRING_MSG');
    Application.MessageBox(Pchar(strMsg),Pchar(MsgTitle), MB_OK + MB_ICONINFORMATION);
  end
  else
  begin
    CheckErr(iRet);
  end;
end;

procedure TIDD102.IDD102_1002Click(Sender: TObject);
var
  TP_Configuration:function(iDoorType: LongInt):LongInt;stdcall;
begin
  TP_Configuration :=GetProcAddress(LockSDKDLL,'TP_Configuration');
  if IDD102_1000.Checked then
  begin
    iDoorType :=4;
  end;
  if IDD102_1001.Checked then
  begin
       iDoorType :=5;
  end;
  iRet :=TP_Configuration(iDoorType);    //����SDK
   CheckErr(iRet);//������
  if iRet<>1 then
  begin
    Exit;
  end;
  IDD102_1003.Enabled :=True;
  IDD102_1005.Enabled :=True;
  IDD102_1009.Enabled :=True;
  IDD102_1011.Enabled :=True
end;

procedure TIDD102.IDD102_1011Click(Sender: TObject);
var
  DateTime: TDateTime;
begin
 etInTime.Text :=FormatDateTime('yyyy', Now + 1)+'-'+  FormatDateTime('mm', Now + 1)+'-'+ FormatDateTime('dd', Now);//DateToStr(DateTime);

  etInTime.Text :=etInTime.Text+' '+ FormatDateTime('HH', Now + 1) +':'+   FormatDateTime('MM', Now + 1)+':'+FormatDateTime('SS', Now + 1);
end;

procedure TIDD102.FormCreate(Sender: TObject);
var
  sdllfile: string;
  DateTime: TDateTime;
  Msg: String;
  Msgstr:String;
begin
  Door_Path :=ExtractFilePath(Application.ExeName);
  sdllfile :=Door_Path+'LockSDK.dll';
 // if FileExists(sdllfile) then
 // begin
     LockSDKDLL :=LoadLibrary(PChar(sdllfile));
  //  if LockSDKDLL<=0 then
  //   begin
  //    Msg :=g_LoadString_Ex('IDS_STRING_ERROR_LOADDLL');
   //   MsgStr :=g_LoadString_Ex('IDS_STRING_ERROR_HANDLE');
   //   Application.MessageBox(Pchar(Msg),Pchar(MsgStr),MB_OK);
   //  end;
  //end
  //else
 // begin
  //  Msg :=g_LoadString_Ex('IDS_STRING_NOTFOUND');
  //  Application.MessageBox(Pchar(sdllfile),Pchar(Msg),MB_OK);
  //end;
  g_SetDialogStrings_Ex(IDD102);
  etRoom.Text :='001.002.00028';
  etInTime.Text :=FormatDateTime('yyyy', Now + 1)+'-'+  FormatDateTime('mm', Now + 1)+'-'+ FormatDateTime('dd', Now);//DateToStr(DateTime);

  etInTime.Text :=etInTime.Text+' '+ FormatDateTime('HH', Now + 1) +':'+   FormatDateTime('MM', Now + 1)+':'+FormatDateTime('SS', Now + 1);

  etOutTime.Text :=FormatDateTime('yyyy', Now + 1)+'-'+  FormatDateTime('mm', Now + 1)+'-'+ FormatDateTime('dd', Now + 1)+' 12:00:00';
end;

end.
