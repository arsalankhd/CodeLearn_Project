#pragma checksum "D:\AspNetCoreProjectAdvanced3\CodeLearn\TopLearn.Web\Views\Shared\_ActiveEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bafaa6c50c3e0986a3e5642cdcc3d2624adbd67f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ActiveEmail), @"mvc.1.0.view", @"/Views/Shared/_ActiveEmail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bafaa6c50c3e0986a3e5642cdcc3d2624adbd67f", @"/Views/Shared/_ActiveEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__ActiveEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CodeLearn.DataLayer.Entities.User.User>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div style=\"direction: rtl; padding: 20px\">\r\n    <h2>");
#nullable restore
#line 4 "D:\AspNetCoreProjectAdvanced3\CodeLearn\TopLearn.Web\Views\Shared\_ActiveEmail.cshtml"
   Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" عزیز !</h2>\r\n    <p>با نشکر از ثبت نام شما در کد لرن، جهت ادامه کار می‌بایست حساب کاربری خود را فعال کنید.</p>\r\n    <p>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 249, "\"", 319, 2);
            WriteAttributeValue("", 256, "https://localhost:44306/Account/ActiveAccount/", 256, 46, true);
#nullable restore
#line 7 "D:\AspNetCoreProjectAdvanced3\CodeLearn\TopLearn.Web\Views\Shared\_ActiveEmail.cshtml"
WriteAttributeValue("", 302, Model.ActiveCode, 302, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">فعالسازی حساب کاربری</a>\r\n    </p>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CodeLearn.DataLayer.Entities.User.User> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
