#pragma checksum "..\..\..\Pages\Rejestracja.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4B8262E957A3FF3656DCE4C8547651E551F3DC2AAB98E65B885BD19A2FCD392F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using KatalogGier.Pages;
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


namespace KatalogGier.Pages {
    
    
    /// <summary>
    /// Rejestracja
    /// </summary>
    public partial class Rejestracja : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 76 "..\..\..\Pages\Rejestracja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MainIcon;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Pages\Rejestracja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginTextBox;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Pages\Rejestracja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hasloTextBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Pages\Rejestracja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox powtorHasloTextBox;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Pages\Rejestracja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nazwaTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/KatalogGier;component/pages/rejestracja.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Rejestracja.xaml"
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
            this.MainIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.loginTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.hasloTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.powtorHasloTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.nazwaTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 94 "..\..\..\Pages\Rejestracja.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Zarejestruj);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 95 "..\..\..\Pages\Rejestracja.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Logowanie);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

