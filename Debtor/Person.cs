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

        [JsonProperty(PropertyName = "idText")]
        public string idText { get; set; }

        private Bank bankInstance { get; set; }
        private FriendManager friendManagerInstance { get; set; }

        public Person(string idText, Bank bankInstance, FriendManager friendManager)
        {
            this.idText = idText;
            this.bankInstance = bankInstance;
            this.friendManagerInstance = friendManagerInstance;
        }
    }
}
