using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debtor
{
    public class Debt
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "host_person_name")]
        public string host_person_name { get; set; }

        [JsonProperty(PropertyName = "friend_person_name")]
        public string friend_person_name { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int amount { get; set; }

        public Debt(string host_person_name, string friend_person_name, int amount)
        {
            this.host_person_name = host_person_name;
            this.friend_person_name = friend_person_name;
            this.amount = amount;
        }

        public void addDebt(int amount)
        {
            this.amount += amount;
        }
    }
}
