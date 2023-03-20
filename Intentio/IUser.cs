using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intentio
{
    public record AttentionReport(
        int TimesDistracted,
        TimeSpan TimeToComplete,
        int LettersMistaken,
        int NumbersMistaken)
    {
        int TotalMistakes { get => LettersMistaken + NumbersMistaken; }
    }

    public class IUser
    {
        public bool IsChild { get; }
        public Device Identifier { get; }
        public List<AttentionReport> AttentionReports { get; }

        private IUser(Device device)
        {
            Identifier = device;
            AttentionReports = new List<AttentionReport>();
        }

        public IUser(Device identifier, bool isChild)
        {
            IsChild = isChild;
            Identifier = identifier;
            AttentionReports = new List<AttentionReport>();
        }

        [JsonConstructor]
        public IUser(bool isChild, Device identifier, List<AttentionReport> attentionReports)
        {
            IsChild = isChild;
            Identifier = identifier;
            AttentionReports = attentionReports;
        }

        public static IUser Child(Device Identifier) => new(Identifier, true);
        public static IUser Parent(Device Identifier) => new(Identifier, false);


        public Form StartActivity()
        {
            Form activity = IsChild ? new TrailMakingTest(this) : new ParentDashBoard(this);
            activity.ShowDialog();
            return activity;
        }

        public string Serialize() => JsonSerializer.Serialize<IUser>(this);
    }
}
