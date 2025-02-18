#pragma checksum "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d35e4068b06f56f38c6909f6fb5e297354b2be3d055884f74fe7c02c290b8856"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Registration_Index), @"mvc.1.0.view", @"/Views/Registration/Index.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\CRUD_VscodeCore\EmployeeDetails\Views\_ViewImports.cshtml"
using EmployeeDetails

#nullable disable
    ;
#nullable restore
#line 2 "E:\CRUD_VscodeCore\EmployeeDetails\Views\_ViewImports.cshtml"
using EmployeeDetails.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d35e4068b06f56f38c6909f6fb5e297354b2be3d055884f74fe7c02c290b8856", @"/Views/Registration/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9f3b1e38fdd76f4e1aeba29fad7fa67f77437b19025040a7c06b58d1447262f7", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Registration_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EmployeeDetails.Models.AQIModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<h2>AQI Map for Andhra Pradesh</h2>
<div id=""map"" style=""width: 100%; height: 500px;""></div>

<script src=""https://maps.googleapis.com/maps/api/js?key=AIzaSyDE6c8ZwCcwJwRHPmw0q9jM93iyIZMYMR8""></script>
<script>
    function initMap() {
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 7,
            center: { lat: 16.5, lng: 80.6 } // Center of Andhra Pradesh
        });

");
#nullable restore
#line 14 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
         foreach (var item in Model)
        {
            

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n                var marker = new google.maps.Marker({\r\n                    position: { lat: ");
            Write(
#nullable restore
#line 18 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                                      item.Latitude

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(", lng: ");
            Write(
#nullable restore
#line 18 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                                                           item.Longitude

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" },\r\n                    map: map,\r\n                    title: \'");
            Write(
#nullable restore
#line 20 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                             item.Location

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" AQI: ");
            Write(
#nullable restore
#line 20 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                                                 item.AQIValue

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\'\r\n                });\r\n                var infowindow = new google.maps.InfoWindow({\r\n                    content: \'");
            Write(
#nullable restore
#line 23 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                               item.Location

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" AQI: ");
            Write(
#nullable restore
#line 23 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                                                   item.AQIValue

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\'\r\n                });\r\n                marker.addListener(\'click\', function () {\r\n                    infowindow.open(map, marker);\r\n                });\r\n            ");
#nullable restore
#line 28 "E:\CRUD_VscodeCore\EmployeeDetails\Views\Registration\Index.cshtml"
                   
        }

#line default
#line hidden
#nullable disable

            WriteLiteral("    }\r\n\r\n    initMap();\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EmployeeDetails.Models.AQIModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
