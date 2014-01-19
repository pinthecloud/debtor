using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debtor
{
    class Person
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "person_live_id")]
        public string person_live_id { get; set; }

        [JsonProperty(PropertyName = "person_name")]
        public string person_name { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string token { get; set; }

        private Bank bankInstance { get; set; }
        private FriendManager friendManagerInstance { get; set; }

        public Person(string person_live_id, string person_name, string token, Bank bankInstance, FriendManager friendManager)
        {
            this.person_live_id = person_live_id;
            this.person_name = person_name;
            this.token = token;
            this.bankInstance = bankInstance;
            this.friendManagerInstance = friendManagerInstance;
        }
    }
}
