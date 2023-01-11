#pragma checksum "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ac7d693679aab294d2be2ed714d93464d1ff5558"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog_Detail), @"mvc.1.0.view", @"/Views/Catalog/Detail.cshtml")]
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
#line 1 "D:\Study\7sem\kursa4\kursach\project\Views\_ViewImports.cshtml"
using Kursach;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Study\7sem\kursa4\kursach\project\Views\_ViewImports.cshtml"
using Kursach.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ac7d693679aab294d2be2ed714d93464d1ff5558", @"/Views/Catalog/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f25e4126cf7b940ec7a958fe707d1fe1887186ff", @"/Views/_ViewImports.cshtml")]
    public class Views_Catalog_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Kursach.Models.Catalog.DetailViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetBook", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Profile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
   
    ViewData["Title"] = "Подробнее о " + Model.Description.Book.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .main-container {
        padding: 10px;
        box-shadow: 10px 4px 10px 3px #888;
        box-sizing: border-box;
        border: 1px solid dimgray;
        margin-left: 100px;
        margin-right: 100px;
    }

    .one-block {
        display: inline-block;
        padding-bottom: 10px;
        border-bottom: 2px solid black;
    }

    .img-size {
        width: 20%;
        border-color: white;
    }

    p {
        font-size: 20px;
    }
</style>
<div class=""main-container"">
    <div class=""one-block"">
    <h3>");
#nullable restore
#line 33 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
   Write(Model.Description.Book.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    <img class=\"img-size\"");
            BeginWriteAttribute("src", " src=\"", 753, "\"", 801, 1);
#nullable restore
#line 34 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
WriteAttributeValue("", 759, Url.Content(Model.Description.Book.Image), 759, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 802, "\"", 836, 1);
#nullable restore
#line 34 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
WriteAttributeValue("", 808, Model.Description.Book.Name, 808, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" align=\"left\" />\r\n\r\n    <p>Издание: ");
#nullable restore
#line 36 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
           Write(Model.Description.Publication.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 37 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
         foreach (var item in Model.Description.AboutBooks)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <b>Включает в себя:</b> ");
#nullable restore
#line 39 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                               Write(item.LiteratureName);

#line default
#line hidden
#nullable disable
            WriteLiteral("            <b>Автор(ы): </b> ");
#nullable restore
#line 40 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                         Write(item.AuthorName);

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                                              
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Есть в наличии:</p>\r\n\r\n");
#nullable restore
#line 44 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
     foreach (var item in Model.Locations)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>\r\n            <b>Название библиотеки</b> - ");
#nullable restore
#line 47 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                                    Write(item.LibraryName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n            <b>Адрес библиотеки</b> - ");
#nullable restore
#line 48 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                                 Write(item.LibraryAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ac7d693679aab294d2be2ed714d93464d1ff55587540", async() => {
                WriteLiteral("\r\n                <input type=\"button\" value=\"Получить\" class=\"btn btn-outline-dark\" style=\"border-radius: 0px;\" />\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-bookId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 50 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
                                                                   WriteLiteral(item.CopyId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["bookId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-bookId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["bookId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        </p>\r\n");
#nullable restore
#line 55 "D:\Study\7sem\kursa4\kursach\project\Views\Catalog\Detail.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Kursach.Models.Catalog.DetailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
