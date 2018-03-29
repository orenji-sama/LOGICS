<%@ Page Title="Информационный ресурс" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      
  
 <div class="jumbotron" >
        <div class="text-center">
            <br />          
           
            
         

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />      
            <asp:Label ID="Label3" runat="server" Height="30px" Text="Введите номера логик через зарятую:"></asp:Label>
        <asp:Label ID="Label1" runat="server" Height="30px" Text="Номер логики:"></asp:Label> &nbsp;&nbsp;&nbsp; </span>
        <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="80px" style="font-size: small"></asp:TextBox>
            <span style="font-size: small">&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox1" runat="server" Text=" Поиск по нескольким логикам" Font-Bold="False" AutoPostBack="True"/>
            <br />
            <br /> </span><asp:Button ID="Button4" runat="server" Height="37px" Text="Добавить" style="font-size: small" Width="80px" OnClick="Button4_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            </span><asp:Button ID="Button1" runat="server" Height="37px" Text="Поиск" style="font-size: small" Width="80px" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Height="37px" Text="Сброс" style="font-size: small" Width="80px" OnClick="Button2_Click" />
            <br />
        <br />
        </div>
      <div class="popup" style="margin:auto; padding: 10px; width:800px; border-radius: 1px; border-style:solid; border-color:#006699; border-width:1px" >
            <asp:Panel ID="popupPanel" VerticalAlign="Center" runat="server">  

                <asp:Label ID="Label4" runat="server" Height="100px" Width="160px" TextAlign="center" Text="Название процесса в СПД:"></asp:Label>
                 <asp:TextBox ID="TextBox2" runat="server" style="resize:none" Font-Underline="False" Height="100" Width="400px" TextMode="MultiLine" Wrap="true"></asp:TextBox> <br/>

                 <asp:Label ID="Label2" runat="server" Height="100px" Text="Номер процесса в СПД:" Width="160px"></asp:Label>
                 <asp:TextBox ID="TextBox3" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label5" runat="server" Height="100px" Text="Рубрика в ЕСРД/ДК:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label6" runat="server" Height="100px" Text="ID DK:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox5" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label7" runat="server" Height="100px" Text="Дата получения логики:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label8" runat="server" Height="100px" Text="Номер служебной записки, по которой логика направлялась:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label9" runat="server" Height="100px" Text="Реакция Управления информатизации:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox8" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label10" runat="server" Height="100px" Text="Номер служебной записки по реакции:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox9" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label11" runat="server" Height="100px" Text="Дата реакции Управления информатизации:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox10" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label12" runat="server" Height="100px" Text="Превышение срока реализации (14 рабочих дней):" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox11" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label13" runat="server" Height="100px" Text="Номер GLPI:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox12" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label14" runat="server" Height="100px" Text="Дата передачи на тестирование:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox13" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label15" runat="server" Height="100px" Text="Номер АКТа по факту передачи на тестирование:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox14" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label16" runat="server" Height="100px" Text="Дата реализации (ввод в эксплуатацию):" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox15" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label17" runat="server" Height="100px" Text="Номер АКТа или служебной записки профильному подразделению по факту реализации (уведомление):" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox16" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label18" runat="server" Height="100px" Text="Комментарий:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox17" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label19" runat="server" Height="100px" Text="Наличие логики в БР:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox18" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label20" runat="server" Height="100px" Text="Управление-исполнитель по логике:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox19" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label21" runat="server" Height="100px" Text="Просрочено дней:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox20" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>

                <asp:Label ID="Label22" runat="server" Height="100px" Text="Плановая дата:" Width="160px"></asp:Label>
                <asp:TextBox ID="TextBox21" runat="server" Font-Underline="False" Height="100" Width="400" TextMode="MultiLine" Wrap="true" style="resize:none"></asp:TextBox><br/>               
                <asp:Button ID="Button3" runat="server"  Height="40" OnClick="Button3_Click" Text="Добавить" Width="76px" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button5" runat="server"  Height="40px" OnClick="Button5_Click" Text="Внести изменения" Width="117px" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button6" runat="server"  Height="40" OnClick="Button2_Click" Text="Отмена" Width="76px" />
            </asp:Panel></div>

     <asp:GridView ID="GridView2" runat="server" ItemStyle-CssClass="maxWidthGrid"  AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" CellPadding="3" BorderWidth="1px"  HorizontalAlign="Center" PageSize="50" AllowUserToResizeColumns="true" OnPageIndexChanging="GridView2_PageIndexChanging" GridLines="Vertical" OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>      
                <asp:BoundField DataField="LOGNAME" HeaderText="Название процесса в СПД" SortExpression="A"  ControlStyle-Width="193"> 
<ControlStyle Width="193px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="RUBRNAMESPD" HeaderText="Номер процесса в СПД" SortExpression="B"  ControlStyle-Width="54">
<ControlStyle Width="54px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="RUBRNAMEESRD" HeaderText="Рубрика в ЕСРД/ДК" SortExpression="C" ControlStyle-Width="74">
<ControlStyle Width="73px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IDDK" HeaderText="ID DK" SortExpression="D" ControlStyle-Width="36">
<ControlStyle Width="35px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DATEAR" HeaderText="Дата получения логики" SortExpression="E" ControlStyle-Width="64">
<ControlStyle Width="63px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DOCNUM" HeaderText="Номер служебной записки, по которой логика направлялась" SortExpression="F" ControlStyle-Width="86">
<ControlStyle Width="86px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="REACT" HeaderText="Реакция Управления информатизации" SortExpression="G" ControlStyle-Width="104">
<ControlStyle Width="103px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DOCNUMREACT" HeaderText="Номер служебной записки по реакции" SortExpression="H" ControlStyle-Width="102">
<ControlStyle Width="101px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="REACTDATE" HeaderText="Дата реакции Управления информатизации" SortExpression="I" ControlStyle-Width="103">
<ControlStyle Width="96px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TIMEPASS" HeaderText="Превышение срока реализации (14 рабочих дней)" SortExpression="J" ControlStyle-Width="81">
                
<ControlStyle Width="78px"></ControlStyle>
                </asp:BoundField>                
                 <asp:TemplateField HeaderText="Номер GLPI" SortExpression="K">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox22" runat="server"
                          Text='<%# Bind("GLPI") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("GLPI", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Text='<%# Eval("GLPI") %>'></asp:HyperLink>
                     </ItemTemplate>
                </asp:TemplateField>



               
                <asp:BoundField DataField="TESTDATE" HeaderText="Дата передачи на тестирование" SortExpression="L" ControlStyle-Width="85" >
<ControlStyle Width="83px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TESTDOC" HeaderText="Номер АКТа по факту передачи на тестирование" SortExpression="M" ControlStyle-Width="90">
<ControlStyle Width="85px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CREATEDATE" HeaderText="Дата реализации (ввод в эксплуатацию)" SortExpression="N" ControlStyle-Width="90">
<ControlStyle Width="90px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CREATEDOC" HeaderText="Номер АКТа или служебной записки профильному подразделению по факту реализации (уведомление)" SortExpression="O" ControlStyle-Width="98">
<ControlStyle Width="98px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="COMMENTR" HeaderText="Комментарий" SortExpression="P" ControlStyle-Width="95">           
<ControlStyle Width="94px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ISINBR" HeaderText="Наличие логики в БР" SortExpression="Q" ControlStyle-Width="52">
<ControlStyle Width="50px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ISPUPR" HeaderText="Управление-исполнитель по логике" SortExpression="R" ControlStyle-Width="107">
<ControlStyle Width="101px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DAYPASS" HeaderText="Просрочено дней" SortExpression="S" ControlStyle-Width="74">
<ControlStyle Width="68px"></ControlStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PLANDATE" HeaderText="Плановая дата" SortExpression="T" ControlStyle-Width="63">            
<ControlStyle Width="60px"></ControlStyle>
                </asp:BoundField>
                <asp:CommandField ButtonType="Button" HeaderText="Управление" ShowDeleteButton="True" ShowEditButton="True" ControlStyle-Width="73" CancelImageUrl="~/Images/cancel.png" DeleteImageUrl="~/Images/delete.png" EditImageUrl="~/Images/edit-icon.png" UpdateImageUrl="~/Images/rewrite.png">
                <ControlStyle Height="30px" Width="30px" />
                </asp:CommandField>
            </Columns>        
            <EmptyDataRowStyle BorderStyle="Solid" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />

<HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="center" Font-Size="Larger" CssClass="pagerStyle" />
            <RowStyle ForeColor="Black" BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC" BackColor="#EEEEEE" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>

       <div style="margin: 0px; padding: 0px; text-align: left"; vertical-align: "middle"; display: "table-cell"">         
          
         
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;A&quot;, &quot;B&quot;, &quot;C&quot;, &quot;D&quot;, &quot;E&quot;, &quot;F&quot;, &quot;G&quot;, &quot;H&quot;, &quot;I&quot;, &quot;J&quot;, &quot;K&quot;, &quot;L&quot;, &quot;M&quot;, &quot;N&quot;, &quot;O&quot;, &quot;P&quot;, &quot;Q&quot;, &quot;R&quot;, &quot;S&quot;, &quot;T&quot; FROM &quot;LOGICS3&quot;"></asp:SqlDataSource>
    </div>

    </asp:Content>
