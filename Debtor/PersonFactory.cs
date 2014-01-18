using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debtor
{
    static class PersonFactory
    {
        private static Bank bankInstance { get; set; }
        private static FriendManager friendManagerInstance { get; set; }


        public static Person makePerson(MobileServiceUser user)
        {
            // Set Instance
            bankInstance = new TestBank();
            friendManagerInstance = new TestFriendManager();
            Person person = new Person(user.UserId, bankInstance, friendManagerInstance);
            return person;
        }
    }
}
