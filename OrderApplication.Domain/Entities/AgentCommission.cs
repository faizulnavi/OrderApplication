using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Domain.Entities
{
    public class AgentCommission
    {
        public string OrderNumber { get; set; }
        public int Commission { get; set; }


        public List<AgentCommission> agent = new List<AgentCommission>();
        public void Generate_AgentCommision(string ordernum)
        {
            try { 
            agent.Add(
            new AgentCommission
            {
                OrderNumber = ordernum,
                Commission = 100
            }
            );
            }
            catch { }
        }
    }
}
