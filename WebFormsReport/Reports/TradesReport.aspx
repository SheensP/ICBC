<%@ Page Title="TradesReport" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TradesReport.aspx.cs" Inherits="WebFormsReport.Reports.TradesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%;">

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
    </div>
</asp:Content>
