﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace Debtor
{
    public sealed partial class DebtListViewItem : UserControl
    {
        public Debt debt { get; set; }

        public DebtListViewItem(Debt debt)
        {
            this.InitializeComponent();
            this.debt = debt;
            nameText.Text = debt.friend_person_name;
            amountText.Text = ""+debt.amount;
        }
    }
}
