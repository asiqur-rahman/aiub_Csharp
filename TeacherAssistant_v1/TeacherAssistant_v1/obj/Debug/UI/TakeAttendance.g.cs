﻿#pragma checksum "..\..\..\UI\TakeAttendance.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F431C0CF641CB86398A23F1889B4D30E0927CB54180A444B1FCD5A691BA5F86C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TeacherAssistant_v1.UI;


namespace TeacherAssistant_v1.UI {
    
    
    /// <summary>
    /// TakeAttendance
    /// </summary>
    public partial class TakeAttendance : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cameraComboBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pictureBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox richTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dayTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button detectButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UI\TakeAttendance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button scanButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TeacherAssistant_v1;component/ui/takeattendance.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\TakeAttendance.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\UI\TakeAttendance.xaml"
            ((TeacherAssistant_v1.UI.TakeAttendance)(target)).Loaded += new System.Windows.RoutedEventHandler(this.OnLoad);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cameraComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.pictureBox = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.richTextBox = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 5:
            this.dayTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.detectButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\UI\TakeAttendance.xaml"
            this.detectButton.Click += new System.Windows.RoutedEventHandler(this.detectButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.scanButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\UI\TakeAttendance.xaml"
            this.scanButton.Click += new System.Windows.RoutedEventHandler(this.scanButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
