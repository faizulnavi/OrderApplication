using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Domain.Entities
{
    public class Membership
    {
        public int MemberID { get; set; }
        public string OrderNumber { get; set; }
        public string MembershipType { get; set; }

        public string IsActive { get; set; }

        public List<Membership> member = new List<Membership>();
        public List<Membership> Updatemember = new List<Membership>();

        public void CreateMembership(string membertype)
        {
            try { 
            member.Add(
            new Membership
            {
                MemberID = 1,
                OrderNumber = "1Test",
                MembershipType = membertype,
                IsActive = "Yes"
            }
            );
            }
            catch { }
        }

        // Upgrade membership

        public void UpdaradMembership(string membertype, int id)
        {
            try { 
            foreach (var type in Updatemember.Where(w => w.MemberID == id))
            {
                MembershipType = membertype;
            }
            }
            catch { }
        }





    }
}
