﻿#pragma checksum "..\..\..\pageDraw.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0222F6F4F7031490F6AFB63D6EABA8C602777FC8"
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
using System.Windows.Controls.Ribbon;
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
using penA_v2;


namespace penA_v2 {
    
    
    /// <summary>
    /// pageDraw
    /// </summary>
    public partial class pageDraw : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.InkCanvas inkBoard;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stckTop;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUndo;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRedo;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSize;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnColor;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\pageDraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMode;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/penA_v2;V1.0.0.0;component/pagedraw.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pageDraw.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.inkBoard = ((System.Windows.Controls.InkCanvas)(target));
            
            #line 11 "..\..\..\pageDraw.xaml"
            this.inkBoard.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.inkBoard_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\pageDraw.xaml"
            this.inkBoard.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.inkBoard_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.stckTop = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.btnUndo = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\pageDraw.xaml"
            this.btnUndo.Click += new System.Windows.RoutedEventHandler(this.btnUndo_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\pageDraw.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.btnClear_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRedo = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\pageDraw.xaml"
            this.btnRedo.Click += new System.Windows.RoutedEventHandler(this.btnRedo_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSize = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\pageDraw.xaml"
            this.btnSize.Click += new System.Windows.RoutedEventHandler(this.btnSize_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnColor = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\pageDraw.xaml"
            this.btnColor.Click += new System.Windows.RoutedEventHandler(this.btnColor_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnMode = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\pageDraw.xaml"
            this.btnMode.Click += new System.Windows.RoutedEventHandler(this.btnMode_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

