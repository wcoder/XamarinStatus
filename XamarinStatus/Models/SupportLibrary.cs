namespace XamarinStatus.Models
{
    public class SupportLibrary
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public int SdkVersion { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}