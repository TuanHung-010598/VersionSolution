using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ProLock.Service
{
    internal class CommunicationWrapper
    {
        public void WriteCard()
        {
            // check xem đã tìm được version chính xác chưa
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["version"]))
            {
                var communacation = Activator.CreateInstanceFrom(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ConfigurationManager.AppSettings["version"]) as ICommunicationClass;
                communacation.WriteCard();
            }
            else
            {
                // tìm version đúng của khách sạn
                foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ICommunicationClass).IsAssignableFrom(type)))
                {
                    var s = Activator.CreateInstance(t) as ICommunicationClass;
                    s.ProGetDLLVersion();
                    if (s.checkVersionIsCorrect("ProUsb-20190530"))
                    {
                        ConfigurationManager.AppSettings["version"] = t.Name;
                        s.WriteCard();
                    }
                }

            }
        }
        public void DeleteCard()
        {
            // làm tương tụ hàm WriteCard
        }
    }
}
