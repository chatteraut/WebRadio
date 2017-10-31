using System;

namespace WebRadio.Model
{
    [Serializable]
    public class RadioChannel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image_Url { get; set; }
        public string Group_Name { get; set; }

        public RadioChannel(string name = "Name", string url = "http://randomurl", string group_name = "default")
        {
            Name = name;
            Url = url;
            Group_Name = group_name;
        }

        public override bool Equals(object obj)
        {
            if(obj is RadioChannel)
            {
                RadioChannel rc = (RadioChannel)obj;
                if(rc == null)
                {
                    return false;
                }
                else if(rc.Name == Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Group_Name.GetHashCode();
                return hash;
            }
        }
    }
}
