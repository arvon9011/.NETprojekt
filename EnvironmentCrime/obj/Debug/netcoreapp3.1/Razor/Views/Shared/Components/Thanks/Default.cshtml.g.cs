#pragma checksum "C:\Users\arvid\source\repos\EnvironmentCrime\EnvironmentCrime\Views\Shared\Components\Thanks\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "db2701a20db6edf4fc594b05f45afec23f1a2ee3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Thanks_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Thanks/Default.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\arvid\source\repos\EnvironmentCrime\EnvironmentCrime\Views\_ViewImports.cshtml"
using EnvironmentCrime.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db2701a20db6edf4fc594b05f45afec23f1a2ee3", @"/Views/Shared/Components/Thanks/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f98cbdd90c9b58456a3608d28d2be4f42f93ded6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Thanks_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Errand>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n<p>\r\n    Anmälan har nu skickats in till kommunen och kommer att utredas. <br />\r\n    Vill du komplettera din anmälan kontakta oss via mail eller telefon. <br />\r\n    Ange då nummer: ");
#nullable restore
#line 8 "C:\Users\arvid\source\repos\EnvironmentCrime\EnvironmentCrime\Views\Shared\Components\Thanks\Default.cshtml"
               Write(Model.RefNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Errand> Html { get; private set; }
    }
}
#pragma warning restore 1591