﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogTagCloud.ascx.cs" Inherits="Sitecore.Modules.WeBlog.Layouts.BlogTagCloud" %>
<%@ Import Namespace="Sitecore.Modules.WeBlog.Model" %>

<asp:Panel ID="PanelTagCloud" runat="server" CssClass="wb-tagCloud wb-panel">
    <h3><%= Sitecore.Modules.WeBlog.Globalization.Translator.Render("TAGCLOUD") %></h3>
    <ul class="wb-tag-sorting">
        <asp:Repeater runat="server" ID="TagSortList">
            <ItemTemplate>
                <li><a data-tag-sort="<%#Container.DataItem%>"><%#Sitecore.Modules.WeBlog.Globalization.Translator.Render("TAGSORT_" + ((string)Container.DataItem).ToUpperInvariant())%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="wb-entries">
        <asp:Repeater runat="server" ID="TagList">
            <ItemTemplate>
                <a data-lastused="<%# ((Tag)Container.DataItem).LastUsed.ToString("s") %>"
                   data-alphabetic="<%# HttpUtility.HtmlAttributeEncode(((Tag)Container.DataItem).Name.ToLowerInvariant()) %>"
                   data-entrycount="<%# ((Tag)Container.DataItem).Count %>"
                   class="wb-weight<%# GetTagWeightClass(((Tag)Container.DataItem).Count) %>"
                   href="<%# GetTagUrl(((Tag)Container.DataItem).Name) %>">
                    <%# ((Tag)Container.DataItem).Name %>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>