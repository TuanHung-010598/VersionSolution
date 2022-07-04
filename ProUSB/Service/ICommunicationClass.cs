using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProLock.Service
{
    internal interface ICommunicationClass
    {
        int ProGetDLLVersion();
        bool checkVersionIsCorrect(string version);
        void showVersion();
        bool WriteCard();
    }
}
