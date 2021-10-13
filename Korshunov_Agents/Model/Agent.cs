using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korshunov_Agents
{
    public partial class Agent
    {
        public string CorrectLogo 
        {
            get
            {
                if (String.IsNullOrEmpty(Logo) || String.IsNullOrWhiteSpace(Logo))
                {
                    return @"..\..\Agents\picture.png";
                }
                else
                {
                    return Logo;
                }    
            }
        }
    }
}
