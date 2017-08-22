<%@ Application Codebehind="Global.asax.cs" Inherits="SampleApi.WebApiApplication" Language="C#" %>
<script runat="server">
    void Application_BeginRequest(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        var response = context.Response;
		
        // enable CORS
        response.AddHeader("Access-Control-Allow-Origin", "*");
        response.AddHeader("X-Frame-Options", "ALLOW-FROM *");
		
        if (context.Request.HttpMethod == "OPTIONS")
        {
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            response.AddHeader("Access-Control-Max-Age", "1728000");
            response.End();
        }
    }
</script>