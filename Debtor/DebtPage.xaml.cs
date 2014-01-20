using Debtor.Common;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 기본 페이지 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234237에 나와 있습니다.

namespace Debtor
{
    /// <summary>
    /// 대부분의 응용 프로그램에 공통되는 특성을 제공하는 기본 페이지입니다.
    /// </summary>
    public sealed partial class DebtPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        // Mobile Service
        private IMobileServiceTable<Debt> debtTable = App.MobileService.GetTable<Debt>();
        private IMobileServiceTable<FriendRelation> friendRelationTable = App.MobileService.GetTable<FriendRelation>();
        private IMobileServiceTable<Person> personTable = App.MobileService.GetTable<Person>();

        // Private
        private Person me;
        private string name;
        private int amount;


        /// <summary>
        /// 이는 강력한 형식의 뷰 모델로 변경될 수 있습니다.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper는 각 페이지에서 탐색 및 프로세스 수명 관리를 
        /// 지원하는 데 사용됩니다.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public DebtPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// 탐색 중 전달된 콘텐츠로 페이지를 채웁니다. 이전 세션의 페이지를
        /// 다시 만들 때 저장된 상태도 제공됩니다.
        /// </summary>
        /// <param name="sender">
        /// 대개 <see cref="NavigationHelper"/>인 이벤트 소스
        /// </param>
        /// <param name="e">다음에 전달된 탐색 매개 변수를 제공하는 이벤트 데이터입니다.
        /// <see cref="Frame.Navigate(Type, Object)"/>에 전달된 매개 변수와
        /// 이전 세션 동안 이 페이지에 유지된
        /// 유지됩니다. 페이지를 처음 방문할 때는 이 상태가 null입니다.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// 응용 프로그램이 일시 중지되거나 탐색 캐시에서 페이지가 삭제된 경우
        /// 이 페이지와 관련된 상태를 유지합니다.  값은
        /// <see cref="SuspensionManager.SessionState"/>의 serialization 요구 사항을 만족해야 합니다.
        /// </summary>
        /// <param name="sender"> 대개 <see cref="NavigationHelper"/>인 이벤트 소스</param>
        /// <param name="e">serializable 상태로 채워질
        /// 빈 사전입니다.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper 등록

        /// 이 섹션에서 제공되는 메서드는 NavigationHelper에서
        /// 페이지의 탐색 메서드에 응답하기 위해 사용됩니다.
        /// 
        /// 페이지별 논리는 다음에 대한 이벤트 처리기에 있어야 합니다.  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// 및 <see cref="GridCS.Common.NavigationHelper.SaveState"/>입니다.
        /// 탐색 매개 변수는 LoadState 메서드 
        /// 및 이전 세션 동안 유지된 페이지 상태에서 사용할 수 있습니다.

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Get parameter
            navigationHelper.OnNavigatedTo(e);
            me = e.Parameter as Person;

            // Set Listview
            await setDebtListView(me);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void friendLiveIdTextBox_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            name = friendLiveIdTextBox.Text.Trim();
            if (name.Length != 0)
                friendRegisterButton.IsEnabled = true;
            else
                friendRegisterButton.IsEnabled = false;
        }

        private async void friendRegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Make a Friend
            if ((await addFriend(me, friendLiveIdTextBox.Text)))
            {
                friendLiveIdTextBox.Text = "";
                await setDebtListView(me);
            }
        }

        private void friendListView_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            string amountString = amountTextBox.Text.Trim();
            if (amountString.Length != 0 && friendListView.SelectedItems.Count > 0)
                debtButton.IsEnabled = true;
            else
                debtButton.IsEnabled = false;
        }

        private void amountTextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if ((uint)e.Key >= (uint)Windows.System.VirtualKey.Number0
                    && (uint)e.Key <= (uint)Windows.System.VirtualKey.Number9)
                e.Handled = false;
            else e.Handled = true;  
        }

        private void amountTextBox_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            string amountString = amountTextBox.Text.Trim();
            if (amountString.Length != 0 && friendListView.SelectedItems.Count > 0)
            {
                debtButton.IsEnabled = true;
                amount = Convert.ToInt32(amountString);
            }
            else
            {
                debtButton.IsEnabled = false;
            } 
        }

        private async void debtButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            amountTextBox.Text = "";
            Person friend = (friendListView.SelectedItem as FriendListViewItem).person;
            await borrow(me, friend, amount);
            await borrow(friend, me, -amount);
            this.Frame.Navigate(typeof(TotalDebtPage), me);
        }

        // Set List View
        private async Task setDebtListView(Person me)
        {
            List<Person> people = await getFriends(me);

            // Set
            friendListView.Items.Clear();
            foreach (Person person in people)
            {
                FriendListViewItem friend = new FriendListViewItem(person);
                friendListView.Items.Add(friend);
            }
        }


        // Get Friends List
        private async Task<List<Person>> getFriends(Person me)
        {
            // Get Relations
            MobileServiceCollection<FriendRelation, FriendRelation> friendRelations = null;
            try
            {
                friendRelations = await friendRelationTable
                    .Where(f => f.host_person_live_id == me.person_live_id)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

            // Do job if he has friends
            List<Person> people = new List<Person>();

            // Get Friends
            try
            {
                foreach (FriendRelation friendRelation in friendRelations)
                {
                    MobileServiceCollection<Person, Person> friend = null;
                    try
                    {
                        friend = await personTable
                            .Where(p => p.person_live_id == friendRelation.friend_person_live_id)
                            .ToCollectionAsync();
                    }
                    catch (MobileServiceInvalidOperationException e)
                    {
                    }
                    people.Add(friend.First());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

            return people;
        }


        // Add Friend
        private async Task<bool> addFriend(Person me, string friendName)
        {
            bool result = false;

            MessageDialog dialog = null;
            string title = null;
            string message = null;

            Person friend = await isExistedPerson(friendName);

            if (friend == null)   // No registered friend
            {
                title = "없는 회원";
                message = "빚쟁이에 아직 등록되지 않은 회원입니다.";
            }
            else   // registered friend
            {
                if (!me.person_live_id.Equals(friend.person_live_id))
                {
                    if ((await isAlreadyFriend(friend)))   // He was already registed as my friend
                    {
                        title = "친구";
                        message = "우리는 이미 친구입니다.";
                    }
                    else   // New friend
                    {
                        await friendRelationTable.InsertAsync(new FriendRelation(me.person_live_id, friend.person_live_id));
                        await friendRelationTable.InsertAsync(new FriendRelation(friend.person_live_id, me.person_live_id));

                        title = "친구 등록";
                        message = friendName + "님을 친구로 등록하였습니다.";
                        result = true;
                    }
                }
                else
                {
                    title = "나 자신";
                    message = "자신은 친구로 등록하지 못합니다.";
                }
            }

            dialog = new MessageDialog(message, title);
            dialog.Commands.Add(new UICommand("확인"));
            await dialog.ShowAsync();

            return result;
        }


        // Check whether it exists in DB
        private async Task<Person> isExistedPerson(string friendName)
        {
            MobileServiceCollection<Person, Person> people = null;
            try
            {
                people = await personTable
                    .Where(p => p.person_name == friendName)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

            if (people.Count > 0)
                return people.First();
            else
                return null;
        }

        // Check whether it exists in DB
        private async Task<bool> isAlreadyFriend(Person friend)
        {
            MobileServiceCollection<FriendRelation, FriendRelation> friendRelation = null;
            try
            {
                friendRelation = await friendRelationTable
                    .Where(f => f.friend_person_live_id == friend.person_live_id)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

            if (friendRelation.Count > 0)
                return true;
            else
                return false;
        }

        // Go to debt
        private async Task borrow(Person me, Person friend, int amount)
        {
            MobileServiceCollection<Debt, Debt> debt = null;
            try
            {
                debt = await debtTable
                    .Where(d => d.host_person_name == me.person_name && d.friend_person_name == friend.person_name)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

            if (debt.Count > 0)
            {
                debt.First().addDebt(amount);
                await debtTable.UpdateAsync(debt.First());
            }
            else
                await debtTable.InsertAsync(new Debt(me.person_name, friend.person_name, amount));
        }

        #endregion
    }
}
