using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvantechDebug
{
    class MessageID
    {
    }
    public  enum MessageGroupId : byte
    {
        Alarm = 0x00,
        Warning = 0x01,
        Error = 0x02,
        Command = 0x03,
        Data = 0x04,
        Configuration = 0x05,
        FirmwareUpdate = 0x06,
    }

    public enum DeviceType : byte
    {
        SystemController = 0x01,
        ArrayController = 0x02,
        RelayController = 0x03,
        StringController = 0x04,
        BatteryPackController = 0x05,
    }

    public enum ArrayMessageID : byte
    {
        EnableBalancing = 0x57,
        UpgradeDatas = 0x78,
        UpgradeQueryVersion = 0x79,
        UpgradeSelectDevice = 0x80,     
    }
    public enum StringMessageID : byte
    {
        TargetVol = 0x20,
        SelectedBP1_4Vol = 0xA2,
        SelectedBP5_8Vol = 0xA3,
        SelectedBP9_12Vol = 0xA4,
        SelectedBP13_16Vol = 0xA5,
        SelectedBP1_4Temp = 0xA6,
        SelectedBP5_8Temp = 0xA7,
        SelectedBP9_12Temp = 0xA8,
        SelectedBP13_16Temp = 0xA9
    }
}
//var list = new List<Person>()
//{
//    new Person { Name = "Joe", Surname = "sdfs"},
//    new Person { Name = "Misha",Surname = "sdff" },
//};
//var bindingList = new BindingList<Person>(list);
//var source = new BindingSource(bindingList, null);
//dataGridView1.DataSource = source;

//bindingList.Clear();
//bindingList.Add(new Person { Name = "jimmy", Surname = "kk" });
//bindingList.Add(new Person { Name = "Jimmy", Surname = "KK" });
//bindingList[0] = new Person {Name = "sfsd",Surname = "sdfsd" };