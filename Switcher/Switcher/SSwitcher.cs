using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcher.utils;
namespace Switcher
{
    class SSwitcher
    {
        private readonly string ip;
        public SSwitcher(String address)
        {
            this.ip = address;
        }

        public void SwitchToMinase()
        {
            var lines = HostsFile.AllLines();
            var res = lines.Where(x => !x.Contains("osu.ppy.sh")).ToList();
            List<string> a = new List<string>();
            a.Add(ip + " osu.ppy.sh");
            a.Add(ip + " c.ppy.sh");
            a.Add(ip + " c1.ppy.sh");
            a.Add(ip + " c2.ppy.sh");
            a.Add(ip + " c3.ppy.sh");
            a.Add(ip + " c4.ppy.sh");
            a.Add(ip + " c5.ppy.sh");
            a.Add(ip + " c6.ppy.sh");
            a.Add(ip + " ce.ppy.sh");
            a.Add(ip + " a.ppy.sh");
            a.Add(ip + " i.ppy.sh");
            a.Add(ip + " s.ppy.sh");
            /*
             *  айпишники которые надо добавить A.K.A кастыль
                ip + "c1.ppy.sh",
                ip + "c2.ppy.sh",
                ip + "c3.ppy.sh",
                ip + "c4.ppy.sh",
                ip + "c5.ppy.sh",
                ip + "c6.ppy.sh",
                ip + "ce.ppy.sh",
                ip + "a.ppy.sh",
                ip + "i.ppy.sh",
                ip + "s.ppy.sh"
             */
            res.AddRange(a);

            HostsFile.WriteAllLines(res);
        }

        public void SwitchToOff()
        {
            HostsFile.WriteAllLines(HostsFile.AllLines().Where(x => !x.Contains("ppy.sh")));
        }

        public Task<Servers> getCurrsrvAsync()
        {
            return Task.Run<Servers>(() => getCurrsrv());
        }
        public Servers getCurrsrv()
        {
            bool isminase = HostsFile.AllLines().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
            return isminase ? Servers.minase : Servers.Off;
        }
        public enum Servers
        {
            Off,
            minase
        }
    }
}
