﻿

#pragma checksum "C:\Users\Seungmin\documents\visual studio 2013\Projects\Debtor\Debtor\DebtPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0C66809104C560DC7BB76602A63B4F55"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Debtor
{
    partial class DebtPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 70 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.debtButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 67 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.amountTextBox_TextChanged;
                 #line default
                 #line hidden
                #line 67 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.amountTextBox_KeyDown;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 62 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.friendLiveIdTextBox_TextChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 63 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.friendRegisterButton_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 59 "..\..\DebtPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.friendListView_SelectionChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


